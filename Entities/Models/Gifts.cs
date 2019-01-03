using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace Entities.Models
{
    [Table("Gifts")]
    public class Gifts
    {
        [Key]
        public int GiftNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime  { get; set; }
        public bool BoyGift { get; set; }
        public bool GirlGift { get; set; }

       
    }
}
