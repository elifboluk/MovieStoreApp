using Microsoft.EntityFrameworkCore;
using MovieStorepApp.API.Entities;

namespace MovieStorepApp.API.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MovieStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<MovieStoreDbContext>>()))
            {
                if (context.Movies.Any())
                    return;

                context.Actors.AddRange(
                    new Actor
                    {
                        Name = "Morgan",
                        Surname = "Freeman",
                        Id = 1
                    },
                    new Actor
                    {
                        Name = "Tom",
                        Surname = "Hardy",
                        Id = 2
                    },
                    new Actor
                    {
                        Name = "Elijah",
                        Surname = "Wood",
                        Id = 3
                    }
                    );
                context.Directors.AddRange(
                    new Director
                    {
                        Name = "David",
                        Surname = "Fincher",
                        Id = 1
                    },
                    new Director
                    {
                        Name = "Christopher",
                        Surname = "Nolan",
                        Id = 2
                    },
                    new Director
                    {
                        Name = "Peter",
                        Surname = "Jackson",
                        Id = 3
                    }
                    );
                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Thriller"
                    },
                    new Genre
                    {
                        Name = "Science Fiction"
                    },
                    new Genre
                    {
                        Name = "Fantastic"
                    }
                    );
                context.SaveChanges();
                context.Movies.AddRange(
                    new Movie
                    {
                        Name = "Se7en",
                        Year = 1995,
                        Actors = context.Actors.Where(c => new[] { 1, 3 }.Contains(c.Id)).ToList(),
                        DirectorId = 1,
                        GenreId = 1,
                        Price = 20
                    },
                    new Movie
                    {
                        Name = "Inception",
                        Year = 2010,
                        Actors = context.Actors.Where(c => new[] { 2 }.Contains(c.Id)).ToList(),
                        DirectorId = 2,
                        GenreId = 2,
                        Price = 40
                    },
                    new Movie
                    {
                        Name = "The Lord of the Rings",
                        Year = 2001,
                        Actors = context.Actors.Where(c => new[] { 1, 2, 3 }.Contains(c.Id)).ToList(),
                        DirectorId = 3,
                        GenreId = 3,
                        Price = 50
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
