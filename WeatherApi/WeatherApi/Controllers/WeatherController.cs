using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json;
using WeatherApi.Models;

namespace WeatherApi.Controllers
{
    public class WeatherController : ApiController
    {
        private readonly WeatherApiContext db = new WeatherApiContext();

        // GET: api/Weather
        public string GetWeatherModels()
        {
            //var weather = db.Weather.Select(x => x);

            var weather = (from w in db.Weather
                select w).ToList();
            //var weather = weatherlist.Select(s => new weather()
            //{
            //    idweather = s.idweather,
            //    temp = s.temp,
            //    humidity = s.humidity,
            //    pressure = s.pressure,
            //    time = s.time
            //});

            var weatherJson = JsonConvert.SerializeObject(weather.ToArray());

            return weatherJson;
        }

        // GET: api/Weather/5
        [ResponseType(typeof(weather))]
        public async Task<IHttpActionResult> GetByDay(DateTime day)
        {
            var previousWeek = day.AddDays(-14);

            var weatherModel = await (from w in db.Weather
                where w.time >= previousWeek
                select w).ToListAsync();

            if (weatherModel == null)
            {
                return NotFound();
            }

            var weatherJson = JsonConvert.SerializeObject(weatherModel.ToArray());
            return Ok(weatherJson);
        }

        // PUT: api/Weather/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutWeatherModel(int id, weather weatherModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != weatherModel.idweather)
            {
                return BadRequest();
            }

            db.Entry(weatherModel).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeatherModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Weather
        [ResponseType(typeof(weather))]
        public async Task<IHttpActionResult> PostWeatherModel(weather weatherModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Weather.Add(weatherModel);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = weatherModel.idweather }, weatherModel);
        }

        // DELETE: api/Weather/5
        [ResponseType(typeof(weather))]
        public async Task<IHttpActionResult> DeleteWeatherModel(int id)
        {
            weather weatherModel = await db.Weather.FindAsync(id);
            if (weatherModel == null)
            {
                return NotFound();
            }

            db.Weather.Remove(weatherModel);
            await db.SaveChangesAsync();

            return Ok(weatherModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WeatherModelExists(int id)
        {
            return db.Weather.Count(e => e.idweather == id) > 0;
        }
    }
}