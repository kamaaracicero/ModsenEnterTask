using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterTask.DataAccess.DbContexts
{
    internal class MainDbContextDesignFactory : IDesignTimeDbContextFactory<MainDbContext>
    {
        public MainDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MainDbContext>();
            optionsBuilder.UseSqlServer(
                "Server=localhost;Database=ModsenEnterTask.Database;User Id=sa;Password=ModsenEnterTask!1;TrustServerCertificate=True;"
            );

            return new MainDbContext(optionsBuilder.Options);
        }
    }
}
