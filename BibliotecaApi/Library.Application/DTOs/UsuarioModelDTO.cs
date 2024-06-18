using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.Library.Application.DTOs
{
    public class UsuarioModelDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite seu nome.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Digite seu Email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite sua senha.")]
        public string Senha { get; set; }
    }
}
