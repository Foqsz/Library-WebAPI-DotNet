using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BibliotecaApi.Library.Core.Model;
using BibliotecaApi.Library.Infrastructure.Data.Map;
using WebApiCatalogo.Catalogo.Core.Model;

namespace BibliotecaApi.Library.Infrastructure.Data
{
    public class LibraryContext : IdentityDbContext<LibraryUser>
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        public DbSet<UsuarioModel> Registration { get; set; }
        public DbSet<LivroModel> livroModels { get; set; }
        public DbSet<UserLivroModel> userLivroEmprestimo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new LivroMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
