using Final.Models;
using Final.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        public ActionResult Index()
        {
            return View(CreateMovieService().GetMovieList());
        }

        public ActionResult Create()
        {
            ViewBag.Title = "New Movie";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(MovieCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (CreateMovieService().CreateMovie(model))
            {
                TempData["SaveResult"] = "Movie established";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Someting went wrong.");
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var movie = CreateMovieService().GetMovieDetailsById(id);
            return View(movie);
        }
        public ActionResult Edit(int id)
        {
            var movie = CreateMovieService().GetMovieDetailsById(id);
            return View(new MovieEdit
            {
                MovieId = movie.MovieId,
                Description = movie.Description
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit( int id, MovieEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);

            
            if (model.MovieId ! = id)
            {
                ModelState.AddModelError("", "Id mismatch");
                return View(model);
            }

            if (CreateMovieService().UpdateMovie(model))
            {
                TempData["SaveResult"] = "Movie updated";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Someting went wrong.");
            return View(model);
        }

        private MovieService CreateMovieService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new MovieService(userId);
            return service;
        }

    }
}