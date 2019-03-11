using Microsoft.EntityFrameworkCore;


namespace CoreWebAPI.Model
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base (options)
        {

        }

        // Create a dataset with table
        public DbSet<EmployeeModel> Employees { get; set; }
    }
}
