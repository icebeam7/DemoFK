using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;

using DemoFK.Models;

namespace DemoFK.ViewModels
{
    [QueryProperty("Item", "Item")]
    public partial class DepartmentDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        Department item;

        public ObservableCollection<Employee> EmployeesDepartment { get; } = new();

		[RelayCommand]
		private async Task Initialize()
		{
			var emp = (await App.LocalDb.GetItems<Employee>()).Where(x => x.DepartmentId == item.Id);

			EmployeesDepartment.Clear();

			foreach (var e in emp)
				EmployeesDepartment.Add(e);
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
                    ? $"UPDATE departments SET Name = '{item.Name}' WHERE Id = {item.Id};"
                    : $"INSERT INTO departments (Name) VALUES('{item.Name}');";

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
                var op = await App.LocalDb.ExecuteQuery($"DELETE FROM departments WHERE Id = {item.Id}");

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