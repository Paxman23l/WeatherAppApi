using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeatherApp.Controllers
{
    [Route("api/[controller]")]
    public class WeatherController : Controller
    {
        private readonly WeatherAppContext _context;

        public WeatherController(WeatherAppContext context)
        {
            _context = context;

            //if (_context.Weather.Any()) return;
            //_context.Weather.Add(new WeatherModel{Id = 1, Temp = 50, Humidity = 50, Pressure = 50, Time = DateTime.Now});
            //_context.SaveChanges();
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<WeatherModel> Get()
        {
            var weatherlist = _context.Weather.Select(x => x);

            var weather = weatherlist.Select(s => new WeatherModel()
            {
                idweather = s.idweather,
                temp = s.temp,
                humidity = s.humidity,
                pressure = s.pressure,
                time = s.time
            });

            return weather;

        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IEnumerable<WeatherModel> GetbyDay(DateTime day)
        {
            var weatherList = _context.Weather.Where(x => x.time.DayOfYear == day.DayOfYear).Select(x => x);

            var weather = weatherList.Select(s => new WeatherModel()
            {
                idweather = s.idweather,
                temp = s.temp,
                humidity = s.humidity,
                pressure = s.pressure,
                time = s.time
            });

            return weather;
        }

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
