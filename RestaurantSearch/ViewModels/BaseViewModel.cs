namespace RestaurantSearch.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    //[AlsoNotifyChangeFor(nameof(IsNotBusy))]
    private bool isBusy;

    [ObservableProperty]
    private string title;

    public bool IsNotBusy => !IsBusy;

    public BaseViewModel()
    {
    }
}

