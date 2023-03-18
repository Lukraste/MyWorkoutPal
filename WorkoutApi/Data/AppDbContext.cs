using Microsoft.EntityFrameworkCore;
using WorkoutApi.Models;

namespace WorkoutApi
{
    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Exercice> Exercices => Set<Exercice>();
    }  
}