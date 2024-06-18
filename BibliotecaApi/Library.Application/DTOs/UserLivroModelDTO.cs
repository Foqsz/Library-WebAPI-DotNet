namespace BibliotecaApi.Library.Application.DTOs
{
    public class UserLivroModelDTO
    {
        public int? Id { get; set; }
        public string NomeLivro { get; set; }
        public string UserEmprestimo { get; set; }
        public string Autor { get; set; }
        public string Genero { get; set; } 
    }
}
