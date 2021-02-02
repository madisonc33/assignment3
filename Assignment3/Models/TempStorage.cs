using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Assignment3.Models
{
    public class TempStorage
    {
        private static List<AddMovie> movies = new List<AddMovie>();

        public static IEnumerable<AddMovie> Movies => movies;

        public static void AddMovie(AddMovie movie)
        {
            movies.Add(movie);
        }
    }
}
