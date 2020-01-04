using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.Designж
using System.ComponentModel.DataAnnotations;

namespace SmarHouse.DAL.Entities
{
    public class House
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [Display(Name = "Название модели")]
        public string Name { get; set; }
    }
}
