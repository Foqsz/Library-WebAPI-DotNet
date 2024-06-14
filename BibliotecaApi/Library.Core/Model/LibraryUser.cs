using Microsoft.AspNetCore.Identity;

namespace WebApiCatalogo.Catalogo.Core.Model
{
    public class LibraryUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}