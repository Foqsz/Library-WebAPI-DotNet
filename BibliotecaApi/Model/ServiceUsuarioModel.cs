using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.Models
{
    public class ServiceUsuarioModel 
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite seu nome.")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Digite seu email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite sua senha.")]
        public string Senha { get; set; }

        public ServiceUsuarioModel(int id, string name, string email, string senha)
        {
            Id = id;
            Name = name;
            Email = email;
            Senha = senha;
        }
    }
}
