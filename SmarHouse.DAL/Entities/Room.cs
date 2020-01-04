using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SmarHouse.DAL.Entities
{
    public class Room
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Room name")]
        public string Name { get; set; }

        [Display(Name = "Id")]
        public int HouseId { get; set; }

        [Display(Name = "House object")]
        public House House { get; set; }
    }
}
