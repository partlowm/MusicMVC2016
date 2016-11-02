using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicFall2016.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicFall2016.Controllers
{
    public class ArtistController : Controller
    {
        private readonly MusicDbContext _context;

        public ArtistController(MusicDbContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var artists = _context.Artists.ToList();
            return View(artists);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Artist artist)
        {
            if (ModelState.IsValid)
            {
                if (_context.Artists.Any(ac => ac.Name.Equals(artist.Name)))
   {

                    return RedirectToAction("Index");
                }
                else
                {
                    _context.Artists.Add(artist);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
               
            }
            return View(artist);
        }
        public IActionResult Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            Artist artist = _context.Artists.SingleOrDefault(a => a.ArtistID == id);
            if (artist == null)
            {
                return NotFound();
            }
            ViewBag.AlbumsList = _context.Albums.Where(a => a.ArtistID == artist.ArtistID);
            return View(artist);

        }
        public IActionResult Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            Artist artist = _context.Artists.SingleOrDefault(a => a.ArtistID == id);
            if (artist == null)
            {
                return NotFound();
            }
            ViewBag.AlbumsList = _context.Albums.Where(a => a.ArtistID == artist.ArtistID);
            return View(artist);

        }

        [HttpPost]
        public IActionResult Edit(Artist artist)
        {
            if (ModelState.IsValid)
            {
                _context.Artists.Update(artist);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtistID = new SelectList(_context.Artists, "ArtistID", "Name");
            ViewBag.GenreID = new SelectList(_context.Genres, "GenreID", "Name");
            return View(artist);
        }
    }
}
