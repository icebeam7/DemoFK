using DemoFK.ViewModels;

namespace DemoFK.Views;

public partial class EmployeeDetailsView : ContentPage
{
	EmployeeDetailsViewModel vm;
	public EmployeeDetailsView(EmployeeDetailsViewModel vm)
	{
		InitializeComponent();
		this.vm = vm;
        BindingContext = vm;
    }

	protected override async void OnAppearing()
	{
		base.OnAppearing();

		await vm.InitializeCommand.ExecuteAsync(null);
	}
}