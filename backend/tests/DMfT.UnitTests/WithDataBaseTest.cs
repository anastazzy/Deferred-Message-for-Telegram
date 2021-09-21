using System;
using DMfT.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DMfT.UnitTests
{
    public class WithDataBaseTest
    {
        protected DMfTDbContext DbContext { get; }
        protected WithDataBaseTest()
        {
           var options = new DbContextOptionsBuilder<DMfTDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
           DbContext = new DMfTDbContext(options);
        }
    }
}
