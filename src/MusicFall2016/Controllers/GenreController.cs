using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicFall2016.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicFall2016.Controllers
{
    public class GenreController : Controller
    {
        private readonly MusicDbContext _context;

        public GenreController(MusicDbContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var genres = _context.Genres.ToList();
            return View(genres);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Genre genre)
        {
            if (_context.Genres.Any(ac => ac.Name.Equals(genre.Name)))
            {

                return RedirectToAction("Index");
            }
            else
            {
                _context.Genres.Add(genre);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(genre);
        }
       

        public IActionResult Details(int? id)
        {

            if(id == null)
            {
                return NotFound();
            }
            Genre genre = _context.Genres.SingleOrDefault(a => a.GenreID == id);
            if(genre == null)
            {
                return NotFound();
            }
            ViewBag.AlbumsList = _context.Albums.Where(a => a.GenreID == genre.GenreID);
            return View(genre);

        }
        public IActionResult Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            Genre genre = _context.Genres.SingleOrDefault(a => a.GenreID == id);
            if (genre == null)
            {
                return NotFound();
            }
            ViewBag.AlbumsList = _context.Albums.Where(a => a.GenreID == genre.GenreID);
            return View(genre);

        }

        [HttpPost]
        public IActionResult Edit(Genre genre)
        {
            if (ModelState.IsValid)
            {
                _context.Genres.Update(genre);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtistID = new SelectList(_context.Artists, "ArtistID", "Name");
            ViewBag.GenreID = new SelectList(_context.Genres, "GenreID", "Name");
            return View(genre);
        }

    }
}
