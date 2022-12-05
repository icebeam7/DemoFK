using DemoFK.Models;

namespace DemoFK.Services
{
    public interface ILocalDatabaseService
    {
		Task<IEnumerable<T>> GetItemsWithQuery<T>(string query) where T : BaseTable, new();
        Task<IEnumerable<T>> GetItems<T>() where T : BaseTable, new();
		Task<bool> ExecuteQuery(string query);
        Task<int> CountItemsWithQuery(string query);
    }
}
