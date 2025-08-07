using Microsoft.EntityFrameworkCore;
using PruebaQuala.Models;
using System;

namespace PruebaQuala.Data
{
    public class AppDbContext : DbContext
    {

        public DbSet<User> Users { get; set; } 

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

    }
}
