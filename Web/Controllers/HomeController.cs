using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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
using Entities.Migrations;

namespace Web.Controllers
{
    public class HomeController : Controller

        {
            private readonly GiftContext _context;
            private readonly HttpClient _httpClient;
            private Uri BaseEndPoint { get; set; }
            private Uri BaseEndPointGirls { get; set; }
            private Uri BaseEndPointBoys { get; set; }
          

            public HomeController(GiftContext context)
            {
                BaseEndPoint = new Uri("https://localhost:44340/api/Gift");
              
                BaseEndPointGirls = new Uri("https://localhost:44340/api/Gift/Girls");
                BaseEndPointBoys = new Uri("https://localhost:44340/api/Gift/Boys");
                _context = context;
                _httpClient = new HttpClient();
            }
            public async Task<IActionResult> Index()
            {
            var response = await _httpClient.GetAsync(BaseEndPoint, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return View(JsonConvert.DeserializeObject<List<Entities.Models.Gifts>>(data));
             }

            public async Task<IActionResult> GirlGifts()
            {

            var response = await _httpClient.GetAsync(BaseEndPointGirls, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return View(JsonConvert.DeserializeObject<List<Entities.Models.Gifts>>(data));
               }
            [HttpGet]
            public IActionResult Create()
            {
            

                return View();
            }
            [HttpPost]
            public ActionResult Create(Entities.Models.Gifts gift)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44340/api/Gift");

                    var postTask = client.PostAsJsonAsync<Entities.Models.Gifts>("Gift", gift);
                postTask.Wait();
                var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }

                ModelState.AddModelError(string.Empty, "Fejl.");

                return View(gift);
            }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    }

