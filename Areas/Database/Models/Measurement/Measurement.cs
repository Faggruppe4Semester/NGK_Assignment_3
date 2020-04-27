using System;
using System.ComponentModel.DataAnnotations;

namespace NGK_Assignment_3.Areas.Database.Models
{
    public class Measurement
    {
        [Key]
        public DateTime Time { get; set; }

        public Place Place { get; set; }
        public float Temperature { get; set; }
        public int Humidity { get; set; }
        public float Pressure { get; set; }
    }

    public class Place
    {
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}