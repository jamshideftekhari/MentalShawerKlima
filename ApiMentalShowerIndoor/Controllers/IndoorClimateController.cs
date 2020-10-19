using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModelLib.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiMentalShowerIndoor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndoorClimateController : ControllerBase
    {
        public static List<SensorDataModel> data = new List<SensorDataModel>()
            {
                new SensorDataModel(1, "d-5", 25.5F, 35, 35, 35),
                new SensorDataModel(2, "d-6", 20.2F, 40, 40, 40),
                new SensorDataModel(3, "d-7", 15.4F, 45, 45, 45),
                new SensorDataModel(4, "d-8", 12.7F, 50, 50, 50),
                new SensorDataModel(5, "d-9", 29.9F, 55, 55, 55)
            };
        // GET: api/<IndoorClimateController>
        [HttpGet]
        public IEnumerable<SensorDataModel> Get()
        {
            return data;
        }

        // GET api/<IndoorClimateController>/5
        [HttpGet("{id}")]
        public SensorDataModel Get(int id)
        {
            return data.Find(i => i.SensorID == id);
        }

        // POST api/<IndoorClimateController>
        [HttpPost]
        public void Post([FromBody] SensorDataModel value)
        {
            data.Add(value);
        }

        // PUT api/<IndoorClimateController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] SensorDataModel value)
        {
            SensorDataModel item = Get(id);
            if(item != null) 
            {
                item.SensorID = value.SensorID;
                item.RoomID = value.RoomID;
                item.Temperature = value.Temperature;
                item.Humidity = value.Humidity;
                item.CO2 = value.CO2;
                item.Pressure = value.Pressure;
                    
            }
        }

        // DELETE api/<IndoorClimateController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            data.Remove(Get(id));
        }
    }
}
