using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.Library.Core.Model
{
    public record jwtAuthModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }

        public jwtAuthModel(int id, string name, string? email, string password)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
        }
    }
}
