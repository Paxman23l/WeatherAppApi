using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class WeatherModel
    {
        [Key]
        public int idweather { get; set; }
        public double temp { get; set; }
        public double pressure { get; set; }
        public double humidity { get; set; }
        public DateTime time { get; set; }
    }
}
