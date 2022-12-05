using DemoFK.Models;
using DemoFK.ViewModels;

namespace DemoFK.Views;

public partial class DepartmentListView : ContentPage
{
	GenericListViewModel<Department> vm;

	public DepartmentListView(GenericListViewModel<Department> vm)
	{
		InitializeComponent();
		this.vm = vm;
		BindingContext = vm;
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        var vm = BindingContext as GenericListViewModel<Department>;
        await vm.GetItemsCommand.ExecuteAsync(null);
    }
}