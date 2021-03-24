using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Assignment3.Models;

namespace Assignment3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //creates a context variable for both tables in the database so that we can access the data in the tables throughout the program
        private MovieListContext context { get; set; }

        //when we build, it gets the data from the DB and stores it in this list
        public HomeController(ILogger<HomeController> logger, MovieListContext con)
        {
            _logger = logger;
            context = con;
        }

        //method that will delete the movie with the matching id. When the deletemovie view is called, it calls this method and returns the updated movie list
        public void RemoveMovie(int movieid)
        {
            AddMovie movie = context.Movies
                .Where(x => x.MovieID == movieid)
                .FirstOrDefault();
            context.Remove(movie);
            context.SaveChanges();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MyPodcasts()
        {
            return View();
        }

        //post method that is called when the delete button is pressed on the movie list view. it calls the RemoveMovie method written above and then returns the updated movie list view
        [HttpPost]
        public IActionResult DeleteMovie(int movieid)
        {
            RemoveMovie(movieid);
            return View("MovieList", context.Movies);
        }

        //called when the user wants to add a new movie
        [HttpGet]
        public IActionResult AddMovie()
        {
            return View();
        }

        //gets users movie and adds it to the list, then redirects to the list page of all the movies entered so far
        [HttpPost]
        public IActionResult AddMovie(AddMovie addMovie)
        {
            if (ModelState.IsValid)
            {
                context.Movies.Add(addMovie);
                context.SaveChanges();
            }
            return View("MovieList", context.Movies);
        }

        //called when the Edit button is clicked on the movie list page, gets the id of the movie they want to edit and passes that movie into the view
        [HttpGet]
        public IActionResult EditMovie(int movieid)
        {
            AddMovie movie = context.Movies
                .Where(x => x.MovieID == movieid)
                .FirstOrDefault();
            return View(movie);
        }

        //gets users movie edits and updates the database
        [HttpPost]
        public IActionResult EditMovie(AddMovie movie)
        {
            if (ModelState.IsValid)
            {
                AddMovie editedMovie = context.Movies
                .Where(x => x.MovieID == movie.MovieID)
                .FirstOrDefault();
                editedMovie.Title = movie.Title;
                editedMovie.Category = movie.Category;
                editedMovie.Director = movie.Director;
                editedMovie.Year = movie.Year;
                editedMovie.Rating = movie.Rating;
                editedMovie.Edited = movie.Edited;
                editedMovie.LentTo = movie.LentTo;
                editedMovie.Notes = movie.Notes;
                context.SaveChanges();
            }
            return View("MovieList", context.Movies);
        }

        //pulls up the list page
        public IActionResult MovieList()
        {
            return View(context.Movies);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
