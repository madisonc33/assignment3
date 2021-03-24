using System;
using Microsoft.EntityFrameworkCore;
namespace Assignment3.Models
{
    public class MovieListContext : DbContext
    {
        //this class inherits from the DbContext base classs and allows us to access the database through it
        public MovieListContext (DbContextOptions<MovieListContext> options) : base (options)
        {

        }

        //this creates a list of all the items in the database that can be referenced in the program
        public DbSet<AddMovie> Movies { get; set; }

      
    }
}
