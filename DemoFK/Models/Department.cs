using SQLite;
using DemoFK.Helpers;

namespace DemoFK.Models
{
    [Table(Constants.DepartmentTable)]
    public class Department : BaseTable
    {
        public string Name { get; set; }
    }
}
