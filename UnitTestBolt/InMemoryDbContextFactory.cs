using Bolt;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestBolt
{
    class InMemoryDbContextFactory
    {
        public DbSearchContext GetMockSearchDbContext()
        {
            var options = new DbContextOptionsBuilder<DbSearchContext>()
                            .UseInMemoryDatabase(databaseName: "InMemoryMockSearchDatabase")
                            .Options;
            var dbContext = new DbSearchContext(options);

            return dbContext;
        }
    }
}
