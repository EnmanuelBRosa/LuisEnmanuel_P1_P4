using Microsoft.EntityFrameworkCore;
using LuisEnmanuel_P1_P4.Models;

namespace LuisEnmanuel_P1_P4.Data;
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Autor> Autores { get; set; }
    }

