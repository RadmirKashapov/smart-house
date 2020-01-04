using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SmarHouse.DAL.Entities
{
    public class House
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "House name")]
        public string Name { get; set; }
    }
}
