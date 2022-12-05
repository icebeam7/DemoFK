using CommunityToolkit.Mvvm.ComponentModel;

namespace DemoFK.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        bool isBusy;

        [ObservableProperty]
        string title;
    }
}
