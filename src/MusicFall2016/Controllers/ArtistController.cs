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
                _context.Artists.Add(artist);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artist);
        }
    }
}
