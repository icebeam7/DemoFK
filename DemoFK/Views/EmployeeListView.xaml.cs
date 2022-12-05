using DemoFK.Models.Views;
using DemoFK.ViewModels;

namespace DemoFK.Views;

public partial class EmployeeListView : ContentPage
{
    GenericListViewModel<EmployeeWithDepartment> vm;
    
	public EmployeeListView(GenericListViewModel<EmployeeWithDepartment> vm)
	{
		InitializeComponent();
        this.vm = vm;
        BindingContext = vm;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        var vm = BindingContext as GenericListViewModel<EmployeeWithDepartment>;
        await vm.GetItemsCommand.ExecuteAsync(null);
    }
}