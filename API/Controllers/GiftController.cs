using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Entities.Migrations;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftController : ControllerBase
    {
        private readonly GiftContext _context;
        public GiftController(GiftContext context)
        {
            _context = context;
        }
       
        [HttpGet]
        public IEnumerable<Entities.Models.Gifts> Get()
        {
            return _context.Gifts;
        }

       
        [HttpGet("{GiftNumber}")]
        public async Task<IActionResult> Get(int GiftNumber)
        {
          

            var giftnum= await _context.Gifts.FindAsync(GiftNumber);

          
            return Ok(giftnum);
           
        }
        [HttpGet("Girls")]
        public async Task<IActionResult> GetGirlGifts()
        { if(!ModelState.IsValid)
            {
                return BadRequest();
                
            }
            var gift = await _context.Gifts.Where(c => c.GirlGift == true).ToListAsync();
            return Ok(gift);
        }
        [HttpGet("Boys")]
        public async Task<IActionResult> GetBoyGifts()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();

            }
            var gift = await _context.Gifts.Where(c => c.BoyGift == true).ToListAsync();
            return Ok(gift);
        }


       
        [HttpPost]
        public async Task<IActionResult> Post(Entities.Models.Gifts gifts, string title, string description, bool boyGift, bool girlGift)
        {
            var postedgift = gifts;
            postedgift.Title = title;
            postedgift.Description = description;
            postedgift.BoyGift = boyGift;
            postedgift.GirlGift = girlGift;
            postedgift.CreationTime = DateTime.UtcNow;
            _context.Add(postedgift);
            await _context.SaveChangesAsync();
            return Ok();

        }

       
    }
}
