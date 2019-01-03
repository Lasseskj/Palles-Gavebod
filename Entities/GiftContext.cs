using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class GiftContext : DbContext
    {
        public DbSet<Models.Gifts> Gifts { get; set; }
      

        public GiftContext(DbContextOptions<GiftContext> options) : base(options)
        {
            Database.EnsureCreated();
            if (Gifts.CountAsync().Result == 0)
            {
                var gift = new Models.Gifts()
                {
                    GiftNumber = 0,
                    Title = "Trøje",
                    Description = "dette er en trøje",
                    BoyGift = false,
                    GirlGift = true
                   
                };
                Gifts.Add(gift);
                SaveChanges();


            }
        }
       
    }
}
