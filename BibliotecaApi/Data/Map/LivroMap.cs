using BibliotecaApi.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibliotecaApi.Data.Map
{
    public class LivroMap : IEntityTypeConfiguration<LivroModel>
    {
        public void Configure(EntityTypeBuilder<LivroModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Titulo).IsRequired().HasMaxLength(50); 
        }
    }
}
