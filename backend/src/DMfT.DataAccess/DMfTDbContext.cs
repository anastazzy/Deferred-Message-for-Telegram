using DMfT.Domain;
using Microsoft.EntityFrameworkCore;

namespace DMfT.DataAccess
{
    public class DMfTDbContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
    }
}
