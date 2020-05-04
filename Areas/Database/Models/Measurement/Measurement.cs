using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace NGK_Assignment_3.Areas.Database.Models
{
    public class Measurement
    {
        [Key]
        public DateTime Time { get; set; }
        public float Temperature { get; set; }
        public int Humidity { get; set; }
        public float Pressure { get; set; }


        public Place Place { get; set; }
        public double PlaceLat { get; set; }
        public double PlaceLon { get; set; }

    }

    public class Place
    {
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }

        public List<Measurement> Measurements { get; set; }
    }
}