using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Policy;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicStore1.Data;
using MusicStore1.Models;


       
namespace MusicStore1.Controllers
{
    public class SongsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _env;


        public SongsController(ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

      
        // GET: Songs
        // Search method by Title, Artist, Album, or Genre
        // Adopted by Rick Anderson, Microsoft ASP.NET Core MVC app
        public async Task<IActionResult> Index(string searchString)
        {
            var songs = from m in _context.Song
                        select m;

            var customers = from mc in _context.Customers
                        select mc;

            if (!String.IsNullOrEmpty(searchString))
            {
                songs = songs.Where(s => (s.Title.Contains(searchString)
                || s.Album.Contains(searchString)
                || s.Artist.Contains(searchString)
                || s.Genre.Contains(searchString))
                && s.IsActive
                );
            }

            return View(await songs.ToListAsync());
        }


   
        // GET: Songs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Song
                .FirstOrDefaultAsync(m => m.ID == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }


        // GET: Songs/Create
        public IActionResult Create()
        {
            return View();
        }

   
        // POST: Songs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Artist,Album,ReleaseDate,Genre,ImagePath,Price,IsActive,IsFeatured")] Song song, IFormFile file)
        {
            if (file != null)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = _env.WebRootPath + "\\uploads\\albums\\" + fileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                song.ImagePath = "uploads/albums/" + fileName;
            }
            if (ModelState.IsValid)
            {
                _context.Add(song);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(song);
        }

        // GET: Songs/Edit/5

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Song.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }
            return View(song);
        }


       // [Authorize(Roles = "Administrator,Manager")]
        // POST: Songs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Artist,Album,ReleaseDate,Genre,ImagePath,Price,IsActive,IsFeatured")] Song song, IFormFile file)
        {
            if (id != song.ID)
            {
                return NotFound();
            }

            if (file != null)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = _env.WebRootPath + "\\uploads\\albums\\" + fileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                song.ImagePath = "uploads/albums/" + fileName;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(song);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(song.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(song);
        }




        // GET: Songs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Song
                .FirstOrDefaultAsync(m => m.ID == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // POST: Songs/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var song = await _context.Song.FindAsync(id);
            _context.Song.Remove(song);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongExists(int id)
        {
            return _context.Song.Any(e => e.ID == id);
        }


        public ActionResult Identity()
        {
            return Content("Testing we are using Identity");
        }
    }
}
