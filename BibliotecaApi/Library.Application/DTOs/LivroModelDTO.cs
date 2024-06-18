using BibliotecaApi.Library.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.Library.Application.DTOs
{
    public class LivroModelDTO
    { 
        public int Id { get; set; }
        [Required(ErrorMessage = "Titulo é obrigatório.")]
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Genero { get; set; }
        public string? Isbn { get; set; }
        public StatusLivro Status { get; set; }
        public int? AnoPublicacao { get; set; }
    }
}
