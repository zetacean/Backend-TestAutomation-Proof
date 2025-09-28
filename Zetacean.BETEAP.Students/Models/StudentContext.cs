using Microsoft.EntityFrameworkCore;

namespace Zetacean.BETEAP.Students.Models
{
    public class StudentContext(DbContextOptions<StudentContext> options) : DbContext(options)
    {
        public DbSet<Student> Students { get; set; }
    }
}
