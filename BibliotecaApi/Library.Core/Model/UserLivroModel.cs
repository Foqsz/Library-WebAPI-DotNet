using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.Library.Core.Model
{
    public class UserLivroModel
    {
        [Key]
        public int Id { get; set; }
        public string NomeLivro { get; set; }
        public string UserEmprestimo { get; set; }
        public string? Autor { get; set; }
        public string? Genero { get; set; }
        public DateTime DataEmprestimo { get; set; }

        public UserLivroModel(string? nomeLivro, string? autor, string? genero)
        {
            NomeLivro = nomeLivro;
            Autor = autor;
            Genero = genero;
        }
    }
}
