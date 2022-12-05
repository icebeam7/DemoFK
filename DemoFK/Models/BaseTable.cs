using SQLite;

namespace DemoFK.Models
{
    public class BaseTable
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
