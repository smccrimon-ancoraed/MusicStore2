using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicStore1.Data;
using MusicStore1.Models;

namespace MusicStore1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // Add user manager support
        private readonly UserManager<IdentityUser> userManager;

        private readonly ApplicationDbContext _context;

        //
        public HomeController(ApplicationDbContext context)
        {
            _context = context;

        }


        // SEM 2020-01-11 Modified query to match IsActive && IsFeatured
        // Bug fix, IsActive = False, and IsFeatured = displayed
        public IActionResult Index()
        {
            return View(_context.Song.Where(m => m.IsFeatured
             && m.IsActive).ToList());
        }

        //
        public IActionResult About()
        {
            ViewData["Message"] = "My application description page.";

            return View();
        }

        //
        public IActionResult Contact()
        {
            ViewData["Message"] = "My contact page.";

            return View();
        }

        //
        public IActionResult Privacy()
        {
            return View();
        }

        //

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
