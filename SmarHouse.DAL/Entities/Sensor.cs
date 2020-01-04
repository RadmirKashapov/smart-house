using System;
using System.ComponentModel.DataAnnotations;

namespace SmartHouse.DAL.Entities
{
    public class Sensor
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Sensor name")]
        public string Name { get; set; }

        [Display(Name = "Id")]
        public int HouseId { get; set; }

        [Display(Name = "House object")]
        public House House { get; set; }

        [Display(Name = "Id")]
        public int RoomId { get; set; }

        [Display(Name = "Room object")]
        public Room Room { get; set; }

        [Display(Name = "Date")]
        public DateTime Date { get; set; }

    }
}
