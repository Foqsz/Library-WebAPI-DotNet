﻿using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Models
{
    public class LibraryContext : DbContext
    { 
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {

        } 
        public DbSet<LibraryRegistration> Registration { get; set; }

    }
}
