namespace RestaurantSearch.Services;

public class RestaurantService
{
	List<Restaurant> restaurantList = new ();
	HttpClient httpClient;

	public RestaurantService()
	{
		httpClient = new ();
	}

	public async Task<List<Restaurant>> GetRestaurantsAsync()
	{
		if (restaurantList?.Count > 0)
			return restaurantList;

		var url = "https://apipool.azurewebsites.net/api/restaurants";

		var response = await httpClient.GetAsync(url);

		if (response.IsSuccessStatusCode)
		{
			restaurantList = await response.Content.ReadFromJsonAsync<List<Restaurant>>();
		}

		return restaurantList;
	}
}

