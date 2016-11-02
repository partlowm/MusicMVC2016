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
        public IActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.AlbumNameSortParam = string.IsNullOrEmpty(sortOrder) ? "album_name" : "";
            ViewBag.ArtistNameSortParam = sortOrder == "artist_name" ? "name_artist" : "artist_name";
            ViewBag.GenreNameSortParam = sortOrder == "genre_name" ? "name_genre" : "genre_name";
            ViewBag.PriceSortParam = sortOrder == "price_amnt" ? "amnt_price" : "price_amnt";
            var albums = from a in _context.Albums.Include(a => a.Artist).Include(a => a.Genre) select a;
            if (!string.IsNullOrEmpty(searchString))
            { albums = albums.Where(a => a.Title.ToUpper().Contains(searchString.ToUpper()) ||  a.Genre.Name.ToUpper().Contains(searchString.ToUpper()) || a.Artist.Name.ToUpper().Contains(searchString.ToUpper()));
           
            }
            switch (sortOrder)
            {
                case "album_name":
                    albums = albums.OrderByDescending(a => a.Title);
                    break;
                case "artist_name":
                    albums = albums.OrderBy(a => a.Artist.Name);
                    break;
                case "name_artist":
                    albums = albums.OrderByDescending(a => a.Artist.Name);
                    break;
                case "genre_name":
                    albums = albums.OrderBy(a => a.Genre.Name);
                    break;
                case "name_genre":
                    albums = albums.OrderByDescending(a => a.Genre.Name);
                    break;
                case "price_amnt":
                    albums = albums.OrderBy(a => a.Price);
                    break;
                case "amnt_price":
                    albums = albums.OrderByDescending(a => a.Price);
                    break;
                default:
                    albums = albums.OrderBy(a => a.Title);
                    break;
            }
           
            
            return View(albums.ToList());
        }
        [HttpPost]
        public IActionResult Likes(int id)
        {
            
                var album = _context.Albums.SingleOrDefault(a => a.AlbumID == id);
                album.Likes++;
                _context.Albums.Update(album);
                _context.SaveChanges();
                return RedirectToAction("Index");
            
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
            if (album.ArtistID != null && album.ArtistID != 0)
            {
                ModelState.Remove("Artist.Name");
                album.Artist = null;
            }

            if (album.GenreID != null && album.GenreID != 0)
            {
                ModelState.Remove("Genre.Name");
                album.Genre = null;
            }
            if (ModelState.IsValid)
            {
                var albumDuplicate = _context.Albums.FirstOrDefault(a => a.Title.ToLower() == album.Title.ToLower() && a.ArtistID == album.ArtistID);
                if (albumDuplicate == null)
                {
                    _context.Albums.Add(album);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.DuplicationValidation = "Album already exists";
                    ViewBag.ArtistList = new SelectList(_context.Artists, "ArtistID", "Name");
                    ViewBag.GenreList = new SelectList(_context.Genres, "GenreID", "Name");
                    return View(album);
                }
            }
            ViewBag.ArtistList = new SelectList(_context.Artists, "ArtistID", "Name");
            ViewBag.GenreList = new SelectList(_context.Genres, "GenreID", "Name");
            return View(album);
        }
        public IActionResult Delete(int? id)
        {
            ViewBag.ArtistID = new SelectList(_context.Artists, "ArtistID", "Name");
            ViewBag.GenreID = new SelectList(_context.Genres, "GenreID", "Name");
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

        [HttpPost]
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