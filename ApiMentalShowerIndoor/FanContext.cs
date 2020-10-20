using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModelLib.Models;

namespace ApiMentalShowerIndoor
{
    public class FanContext : DbContext
    {
        public FanContext(DbContextOptions<FanContext> options) : base(options) { }

        public DbSet<SensorDataModel> Fans { get; set; }
    }
}
