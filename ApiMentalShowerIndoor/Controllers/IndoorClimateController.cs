using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiMentalShowerIndoor;
using ModelLib.Models;

namespace ApiMentalShowerIndoor.Controllers
{
    /// <summary>
    /// Rest api med inmemoryDB
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class IndoorClimateController : ControllerBase
    {
        private readonly FanContext _context;

        public IndoorClimateController(FanContext context)
        {
            _context = context;
        }

        // GET: api/SensorDataModels
        /// <summary>
        /// Get all data readings 
        /// </summary>
        /// <returns>A list of data readings</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SensorDataModel>>> Get()
        {
            return await _context.Fans.ToListAsync();
        }

        // GET: api/SensorDataModels/5
        /// <summary>
        /// Get a specific data readings from ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A SensorDateModel object</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<List<SensorDataModel>>> Get(int id)
        {
            var allData = Get().Result.Value.ToList().FindAll(a => a.SensorID == id);

            if (allData.Count == 0)
            {
                return NotFound();
            }

            return allData;
        }

        // GET: api/SensorDataModels/5
        /// <summary>
        /// Get latest data reading from SensorID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A SensorDateModel object</returns>
        [HttpGet("/latest/{id}")]
        public async Task<ActionResult<SensorDataModel>> GetLatest(int id)
        {
            var allData = Get().Result.Value.ToList().FindAll(a => a.SensorID == id);
            var sorted = allData.ToList().OrderBy(a => a.MeasurmentId);
            if (sorted.Count() == 0)
            {
                return NotFound();
            }

            return sorted.Last();
        }


        // PUT: api/SensorDataModels/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSensorDataModel(int id, SensorDataModel sensorDataModel)
        {
            if (id != sensorDataModel.SensorID)
            {
                return BadRequest();
            }

            _context.Entry(sensorDataModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SensorDataModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // PUT api/
        [HttpPut("Temp/{id}")]
        public async Task<bool> Put(int id, [FromBody] float value)
        {
            bool isTempTrue = false;

            List<SensorDataModel> mockList = new List<SensorDataModel>()
            {
                new SensorDataModel(1, "Stue", 25.12f, 30, 12, 1200),
                new SensorDataModel(2, "Stue", 25.12f, 30, 12, 1200),
                new SensorDataModel(3, "Stue", 25.12f, 30, 12, 1200)
            };

            SensorDataModel model = mockList.Find(m => m.SensorID == id);

            while (!isTempTrue)
            {
                try
                {

                    if (model.Temperature > value + 2) throw new Exception("More than two degrees");
                    if (model.Temperature < value - 2) throw new Exception("Less than two degrees");

                    isTempTrue = true;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    if (e.Message == "More than two degrees")
                    {
                        model.Temperature -= 0.1f;
                    }

                    if (e.Message == "Less thant two degrees")
                    {
                        model.Temperature += 0.1f;
                    }
                }
            }



            return isTempTrue;
        }


        // POST: api/SensorDataModels
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SensorDataModel>> PostSensorDataModel(SensorDataModel sensorDataModel)
        {
            _context.Fans.Add(sensorDataModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = sensorDataModel.SensorID }, sensorDataModel);
        }

        // DELETE: api/SensorDataModels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SensorDataModel>> DeleteSensorDataModel(int id)
        {
            var sensorDataModel = await _context.Fans.FindAsync(id);
            if (sensorDataModel == null)
            {
                return NotFound();
            }

            _context.Fans.Remove(sensorDataModel);
            await _context.SaveChangesAsync();

            return sensorDataModel;
        }

        private bool SensorDataModelExists(int id)
        {
            return _context.Fans.Any(e => e.SensorID == id);
        }
    }
}
    