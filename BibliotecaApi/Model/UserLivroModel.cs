using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.Model
{
    public class UserLivroModel
    {
        [Key]
        public int Id { get; set; } 
        public string NomeLivro { get; set; }
        public string UserEmprestimo { get; set; }
        public DateTime DataEmprestimo { get; set; }

    }
}
