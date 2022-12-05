using SQLite;
using System.Diagnostics;

using DemoFK.Models;
using DemoFK.Helpers;

namespace DemoFK.Services
{
    public class LocalDatabaseService : ILocalDatabaseService
    {
        private string dbPath;
        private SQLiteAsyncConnection connection;

        public LocalDatabaseService(string dbPath)
        {
            this.dbPath = dbPath;
        }

        private async Task Init()
        {
            if (connection != null)
                return;

            try
            {
                connection = new SQLiteAsyncConnection(dbPath);

                connection.Tracer = new Action<string>(q => Debug.WriteLine(q));
                connection.Trace = true;

                await CreateTables();
                var count = await CountItems();

                if (count == 0)
                {
                    await AddInitialData();
                    await CheckData();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async Task CreateTables()
        {
            var createTableStatements = new List<string>()
            {
                Constants.CreateDepartmentTableStatement,
                Constants.CreateEmployeeTableStatement
            };

            foreach (var statement in createTableStatements)
                await ExecuteQuery(statement);
        }

        private async Task<int> CountItems()
        {
            var tables = new string[] { Constants.DepartmentTable, Constants.EmployeeTable };
            var count = 0;

            foreach (var table in tables)
            {
                var countQuery = $"SELECT COUNT(*) FROM {table}";
                var tableCount = await CountItemsWithQuery(countQuery);
                Debug.WriteLine($"{table}: {tableCount}");
                count += tableCount;
            }

            return count;
        }

        private async Task AddInitialData()
        {
            var commands = new List<string>()
            {
                "INSERT INTO departments VALUES (1, 'Kitchen'), (2, 'Sales'), (3, 'Accounting');",
                "INSERT INTO employees VALUES (1, 'SpongeBob SquarePants', 1), (2, 'Squidward Tentacles', 2), (3, 'Mr. Krabs', 3), (4, 'Pearl Krabs', 3);",
            };

            foreach (var command in commands)
            {
                var op = await ExecuteQuery(command);
                Debug.WriteLine(op);
            }
        }

        private async Task CheckData()
        {
            var item = await connection.Table<Department>().Where(v => v.Id == 1).FirstOrDefaultAsync();
            Debug.WriteLine(item?.Name);
        }
		
        public async Task<IEnumerable<T>> GetItems<T>() where T : BaseTable, new()
        {
            await Init();

            return await connection.Table<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetItemsWithQuery<T>(string query) where T : BaseTable, new()
        {
            await Init();

            return await connection.QueryAsync<T>(query);
        }

        public async Task<int> CountItemsWithQuery(string query)
        {
            await Init();

            return await connection.ExecuteScalarAsync<int>(query);
        }

        public async Task<bool> ExecuteQuery(string query)
        {
            await Init();

            var op = await connection.ExecuteAsync(query);
            return op > 0;
        }
    }
}