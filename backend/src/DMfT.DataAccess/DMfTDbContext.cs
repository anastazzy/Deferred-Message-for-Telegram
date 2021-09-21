using DMfT.Domain;
using Microsoft.EntityFrameworkCore;

namespace DMfT.DataAccess
{
    public class DMfTDbContext : DbContext
    {
        public DMfTDbContext(DbContextOptions<DMfTDbContext> options) : base(options)
        {

        }
        public DbSet<Message> Messages { get; set; }
    }
}
