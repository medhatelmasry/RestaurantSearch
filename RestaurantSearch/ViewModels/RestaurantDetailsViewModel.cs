using System;
namespace RestaurantSearch.ViewModels;

[QueryProperty("Restaurant", "Restaurant")]
public partial class RestaurantDetailsViewModel : BaseViewModel
{
	IMap _map;
	public RestaurantDetailsViewModel(IMap map)
	{
		_map = map;
	}

	[ObservableProperty]
	Restaurant restaurant;

	// this method is not used.
	// it could, however, be used to return from the details  to the main page
	[RelayCommand]
	async Task GoBackAsync()
	{
		await Shell.Current.GoToAsync("..");
	}

	[RelayCommand]
	async Task OpenMapAsync()
	{
		try
		{
			await _map.OpenAsync(Restaurant.Latitude, Restaurant.Longitude,
				new MapLaunchOptions
				{
					Name = Restaurant.RestaurantName,
					NavigationMode = NavigationMode.None
				});
		}
		catch (Exception ex)
		{
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error!", $"Unable to open map: {ex.Message}", "OK");
        }
    }
}

