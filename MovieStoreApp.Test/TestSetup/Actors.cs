using MovieStorepApp.API.DbOperations;
using MovieStorepApp.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreApp.Test.TestSetup
{
    public static class Actors
    {
        public static void AddActors(this MovieStoreDbContext context)
        {
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
        }
    }
}
