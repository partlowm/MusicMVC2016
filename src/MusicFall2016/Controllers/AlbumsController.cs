using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicFall2016.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicFall2016.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly MusicDbContext _context;

        public AlbumsController(MusicDbContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {

            var albums = _context.Albums.Include(a => a.Artist).Include(a => a.Genre);
            return View(albums.ToList());
        }
        public IActionResult Create()
        {
            ViewBag.ArtistID = new SelectList(_context.Artists, "ArtistID", "Name");
            ViewBag.GenreID = new SelectList(_context.Genres, "GenreID", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Album album)
        {
            if (ModelState.IsValid)
            {
                _context.Albums.Add(album);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtistID = new SelectList(_context.Artists, "ArtistID", "Name");
            ViewBag.GenreID = new SelectList(_context.Genres, "GenreID", "Name");
            return View(album);
        }
        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            Album album = _context.Albums.Include(a => a.Artist).Include(a => a.Genre).SingleOrDefault(a => a.AlbumID == id);
            if (album == null)
            {
                return NotFound();
            }
            return View(album);

        }

        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(Album album)
        {

                _context.Albums.Remove(album);
                _context.SaveChanges();
                return RedirectToAction("Index");

        }
        public IActionResult Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            Album album = _context.Albums.Include(a => a.Artist).Include(a => a.Genre).SingleOrDefault(a => a.AlbumID == id);
            if (album == null)
            {
                return NotFound();
            }
            ViewBag.AlbumsList = _context.Albums.Where(a => a.AlbumID == album.AlbumID);
            return View(album);

        }
        public IActionResult Edit(int? id)
        {
            ViewBag.ArtistID = new SelectList(_context.Artists, "ArtistID", "Name");
            ViewBag.GenreID = new SelectList(_context.Genres, "GenreID", "Name");
            if (id == null)
            {
                return NotFound();
            }
            Album album = _context.Albums.SingleOrDefault(a => a.AlbumID == id);
            if (album == null)
            {
                return NotFound();
            }

            ViewBag.AlbumsList = _context.Albums.Where(a => a.AlbumID == album.AlbumID);
            return View(album);

        }
        [HttpPost]
        public IActionResult Edit(Album album)
        {
            
            if (ModelState.IsValid)
            {
                _context.Albums.Update(album);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtistID = new SelectList(_context.Artists, "ArtistID", "Name");
            ViewBag.GenreID = new SelectList(_context.Genres, "GenreID", "Name");
            return View(album);
        }
    }
}