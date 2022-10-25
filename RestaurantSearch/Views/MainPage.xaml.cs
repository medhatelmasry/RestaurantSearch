namespace RestaurantSearch.Views;

public partial class MainPage : ContentPage
{
	public MainPage(RestaurantsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

}

