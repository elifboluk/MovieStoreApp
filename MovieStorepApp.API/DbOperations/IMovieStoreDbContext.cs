using Microsoft.EntityFrameworkCore;
using MovieStorepApp.API.Entities;

namespace MovieStorepApp.API.DbOperations
{
    public interface IMovieStoreDbContext
    {
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Order> Orders { get; set; }
        int SaveChanges();
    }
}
