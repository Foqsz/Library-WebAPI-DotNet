using BibliotecaApi.Data.Map;
using BibliotecaApi.Model;
using BibliotecaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Models
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        public DbSet<UsuarioModel> Registration { get; set; }
        public DbSet<LivroModel> livroModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new LivroMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
