using System;
using RestaurantSearch.Models;
using RestaurantSearch.Services;

namespace RestaurantSearch.ViewModels;

public partial class RestaurantsViewModel : BaseViewModel
{
	RestaurantService _restaurantService;

	public ObservableCollection<Restaurant> Restaurants { get; } = new ();

	IConnectivity _connectivity;

	IGeolocation _geoLocation;

	public RestaurantsViewModel(RestaurantService restaurantService, 
		IConnectivity connectivity,
		IGeolocation geoLocation)
	{
		_restaurantService = restaurantService;
		_connectivity = connectivity;
		_geoLocation = geoLocation;

        Title = "Restaurant Search";
	}

	[RelayCommand]
	async Task GetClosestRestaurantAsync()
	{
		if (IsBusy || Restaurants.Count == 0) return;

		try
		{
			var location = await _geoLocation.GetLastKnownLocationAsync();
			if (location is null)
			{
				location = await _geoLocation.GetLocationAsync(
					new GeolocationRequest
					{
						DesiredAccuracy = GeolocationAccuracy.Medium,
						Timeout = TimeSpan.FromSeconds(30),
					});
			}

			if (location is null) return;

			var first = Restaurants.OrderBy(_ =>
				location.CalculateDistance(_.Latitude, _.Longitude, DistanceUnits.Kilometers))
				.FirstOrDefault();

			if (first is null) return;

			await Shell.Current.DisplayAlert("Closest Restaurant",
				$"{first.RestaurantName} at {first.Street}, {first.City}, {first.PostalCode}", "OK");
		}
		catch (Exception ex)
		{
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error!", $"Unable to get closest restaurant: {ex.Message}", "OK");
        }
    }

    [RelayCommand]
    async Task GoToDetailsAsync(Restaurant restaurant)
	{
		if (restaurant is null) return;

		await Shell.Current.GoToAsync($"{nameof(DetailsPage)}", true, 
			new Dictionary<string, object>
			{
				{"Restaurant", restaurant }
			});
	}

	[RelayCommand]
	async Task GetRestaurantsAsync()
	{
		if (IsBusy) return;

		try
		{
			if (_connectivity.NetworkAccess != NetworkAccess.Internet)
			{
                await Shell.Current.DisplayAlert("Internet Issue", "Check your internet and try again!", "OK");
                return;
			}

            IsBusy = true;
            List<Restaurant> restaurants = await _restaurantService.GetRestaurantsAsync();

			if (Restaurants.Count != 0)
			{
                Restaurants.Clear();
			}

			foreach (var item in restaurants)
			{
				Restaurants.Add(item);
			}

		}
		catch (Exception ex)
		{
			Debug.WriteLine(ex);
			await Shell.Current.DisplayAlert("Error!", $"Unable to retrieve restaurants: {ex.Message}", "OK");
		} finally
		{
            IsBusy = false;
		}
	}
}


