using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;

using DemoFK.Models;
using DemoFK.Models.Views;

namespace DemoFK.ViewModels
{
    [QueryProperty("employeeWithDepartment", "Item")]
    public partial class EmployeeDetailsViewModel : BaseViewModel
    {
		[ObservableProperty]
		Employee item;
		
        public EmployeeWithDepartment employeeWithDepartment 
        { 
            set { item = new(value); OnPropertyChanged("Item"); } 
        }

		public ObservableCollection<Department> Departments { get; } = new();

        [ObservableProperty]
        Department employeeDepartment;

        [RelayCommand]
        private async Task Initialize()
        {
            var dep = await App.LocalDb.GetItems<Department>();
            
            Departments.Clear(); 

            foreach (var d in dep) 
                Departments.Add(d);

            EmployeeDepartment = Departments
                .Where(d => d.Id == Item.DepartmentId)
                .FirstOrDefault();
		}

		[RelayCommand]
        private async Task SaveItem()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                var query = item.Id != 0
                    ? $"UPDATE employees SET Name = '{item.Name}', DepartmentId = {EmployeeDepartment.Id} WHERE Id = {item.Id};"
                    : $"INSERT INTO employees (Name, DepartmentId) VALUES('{item.Name}', {EmployeeDepartment.Id});";

                var op = await App.LocalDb.ExecuteQuery(query);

                await Shell.Current.DisplayAlert(
                    "Result",
                    op ? "Data successfully saved!" : "Error. Try again.",
                    "OK");

                if (op)
                    await Shell.Current.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task DeleteItem()
        {
            var confirm = await Shell.Current.DisplayAlert("Confirm", "Do you want to DELETE this item?", "Yes", "No");

            if (confirm)
            {
                var op = await App.LocalDb.ExecuteQuery($"DELETE FROM employees WHERE Id = {item.Id}");

                await Shell.Current.DisplayAlert(
                    "Result",
                    op ? "Data successfully deleted!" : "Try again later",
                    "OK");

                if (op)
                    await Shell.Current.Navigation.PopAsync();
            }
        }
    }
}