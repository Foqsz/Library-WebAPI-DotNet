using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BibliotecaApi.Library.Infrastructure.Data
{
    public class LibraryContextFactory : IDesignTimeDbContextFactory<LibraryContext>
    {
        public LibraryContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<LibraryContext>();
            var connectionString = configuration.GetConnectionString("ConexaoPadrao");

            builder.UseSqlServer(connectionString);

            return new LibraryContext(builder.Options);
        }
    }
}
