using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> User { get; set; }
        public DbSet<food_chart> food_Chart { get; set; }
        public DbSet<Nutrients> nutrients { get; set; }
        public DbSet<HealthData> health_data { get; set; }
        public DbSet<Amount> amount { get; set; }
    }
  
    
}