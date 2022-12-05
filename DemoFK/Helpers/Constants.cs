namespace DemoFK.Helpers
{
    public static class Constants
    {
        public const string LocalDbFile = "company_v12.db";
        public const string DepartmentTable = "departments";
        public const string EmployeeTable = "employees";
        
        public const string CreateDepartmentTableStatement = $"CREATE TABLE IF NOT EXISTS {DepartmentTable} (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name VARCHAR(255));";
        public const string CreateEmployeeTableStatement = $"CREATE TABLE IF NOT EXISTS {EmployeeTable} (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name VARCHAR(255), DepartmentId INT, FOREIGN KEY(DepartmentId) REFERENCES departments(Id));";

		public const string AllDepartmentsQuery = $"SELECT * FROM {DepartmentTable}";
		public const string EmployeesAndDepartmentQuery = $"SELECT e.*, d.Name as DepartmentName FROM {EmployeeTable} e JOIN {DepartmentTable} d ON e.DepartmentId = d.Id";

		public const string DepartmentDetailsRoute = "DepartmentDetailsPage";
		public const string EmployeeDetailsRoute = "EmployeeDetailsPage";
	}
}
