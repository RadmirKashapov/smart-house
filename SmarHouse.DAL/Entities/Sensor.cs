﻿using System;
using System.ComponentModel.DataAnnotations;

namespace SmartHouse.DAL.Entities
{
    public class Sensor
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Id")]
        public int HouseId { get; set; }

        [Display(Name = "Id")]
        public int RoomId { get; set; }

    }
}
