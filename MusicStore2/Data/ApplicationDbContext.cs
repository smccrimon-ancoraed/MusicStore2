using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicStore1.Models;

namespace MusicStore1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MusicStore1.Models.Music> Music { get; set; }
        public DbSet<Song> Song { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
