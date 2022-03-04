using Final.Data;
using Final.Models;
using FinalProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Services
{
    public class MovieService
    {
        private readonly Guid _userId;

        public MovieService(Guid userId)
        {
            _userId = userId;
        }

        public MovieDetail GetMovieDetailsById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var movie = ctx.movies.Single(m => m.Id == id);
                return new MovieDetail
                {
                    MovieId = movie.Id,
                    Description = movie.Description,
                };
            }
        }

        public bool CreateMovie(MovieCreate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var newMovie = new Movie()
                {
                    Description = model.Description
                };

                ctx.movies.Add(newMovie);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<MovieListItem> GetMovieList()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.movies.Select(m => new MovieListItem
                {
                    //MovieId = m.Id,
                    Description = m.Description
                });

                return query.ToArray();
            }
        }

        public bool UpdateMovie(MovieEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var movie = ctx.movies.Single(m => m.Id == model.MovieId);
                movie.Description = model.Description;

                return ctx.SaveChanges() == 1;

            }
        }

       
    }
}
