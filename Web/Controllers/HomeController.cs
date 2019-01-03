using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Web;
using Entities;
using Entities.Models;
using Web.Models;
using System.Diagnostics;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly GiftContext _context;
        public HomeController(GiftContext context)
        {
          
            _context = context;
            
        }
        public IActionResult Index()
        {
            var GiftsView = _context.Gifts;
            return View(GiftsView);
        }

        public IActionResult GirlGifts()
        {

            var GiftsView = _context.Gifts;
            return View(GiftsView);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Gifts gifts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            gifts.CreationTime = DateTime.UtcNow;
            _context.Add(gifts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
