using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WeatherApp.Models
{
    public class WeatherAppContext : DbContext
    {
        public WeatherAppContext(DbContextOptions<WeatherAppContext> options)
            :base(options)
        {
        }

        public DbSet<WeatherModel> Weather { get; set; }
    }
}
