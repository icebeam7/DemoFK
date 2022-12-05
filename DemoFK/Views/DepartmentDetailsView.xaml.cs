using DemoFK.ViewModels;

namespace DemoFK.Views;

public partial class DepartmentDetailsView : ContentPage
{
	DepartmentDetailsViewModel vm;

	public DepartmentDetailsView(DepartmentDetailsViewModel vm)
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