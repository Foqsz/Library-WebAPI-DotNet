using BibliotecaApi.Enums;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using System.Web.Http;

namespace BibliotecaApi.Model
{
    public class LivroModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Titulo é obrigatório.")]
        public string Titulo { get; set; }
        public string? Autor { get; set; }
        public string? Genero { get; set; }
        public string? Isbn { get; set; }
        public StatusLivro Status { get; set; }
        public int? AnoPublicacao { get; set; }
         
         
    }
}
