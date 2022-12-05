using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;

using DemoFK.Models;

namespace DemoFK.ViewModels
{
    public partial class GenericListViewModel<T> : BaseViewModel where T : BaseTable, new()
    {
        public ObservableCollection<T> Items { get; } = new();

        private string detailsRoute;
        private string query;

        public GenericListViewModel(string query, string detailsRoute)
        {
			this.query = query;
			this.detailsRoute = detailsRoute;
		}

		[ObservableProperty]
        T selectedItem;

        [RelayCommand]
        private async Task GetItemsAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

				var items = await App.LocalDb.GetItemsWithQuery<T>(query);

				// if you want to get all items from specific table (no join involved) you can also use
                // items = await App.LocalDb.GetItems<T>();

				if (Items.Count != 0)
                    Items.Clear();

                foreach (var item in items)
                    Items.Add(item);
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

        private async Task NavigateToDetails(T item)
        {
            var data = new Dictionary<string, object>
            {
                { "Item", item }
            };

            await Shell.Current.GoToAsync(detailsRoute, true, data);
        }

        [RelayCommand]
        private async Task GoToNewDetails()
        {
            await NavigateToDetails(new T());
        }

        [RelayCommand]
        private async Task GoToDetails()
        {
            if (selectedItem == null)
                return;

            await NavigateToDetails(selectedItem);
        }
    }
}