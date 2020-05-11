using System.Collections.Generic;

namespace NGK_Assignment_3.Areas.Database.Models
{
    public class Place
    {
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }

        public List<Measurement> Measurements { get; set; }
    }
}