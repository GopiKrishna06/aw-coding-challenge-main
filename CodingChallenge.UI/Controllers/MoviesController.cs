using CodingChallenge.DataAccess;
using CodingChallenge.DataAccess.Interfaces;
using System.Linq;
using System.Web.Http;

namespace CodingChallenge.UI.Controllers
{
    [RoutePrefix("api/movies")]
    public class MoviesController : ApiController
    {
        public ILibraryService LibraryService { get; set; }

        [HttpGet]
        [Route("getMovies")]
        public IHttpActionResult getMovies()
        {
            LibraryService = new LibraryService();
            var movies = LibraryService.SearchMovies("").ToList();
            return Ok(movies);
        }

        [HttpGet]
        [Route("searchByTitle/{title}")]
        public IHttpActionResult searchByTitle(string title = "")
        {
            LibraryService = new LibraryService();
            var movies = LibraryService.SearchMovies(title).ToList();
            return Ok(movies);
        }
    }

}
