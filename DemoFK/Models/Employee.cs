using SQLite;
using DemoFK.Helpers;
using DemoFK.Models.Views;

namespace DemoFK.Models
{
    [Table(Constants.EmployeeTable)]
    public class Employee : BaseTable
    {
        public string Name { get; set; }
        public int DepartmentId { get; set; }

        public Employee()
        {

        }

        public Employee(EmployeeWithDepartment empDep)
        {
            Id = empDep.Id;
            Name = empDep.Name;
            DepartmentId = empDep.DepartmentId;
        }
    }
}