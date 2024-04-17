﻿using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.Models
{
    public class LibraryRegistrationDTO
    {
        [Key]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; } 

    }
}
