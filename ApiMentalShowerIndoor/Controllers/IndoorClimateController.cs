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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SensorDataModel>>> Get()
        {
            return await _context.Fans.ToListAsync();
        }

        // GET: api/SensorDataModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SensorDataModel>> Get(int id)
        {
            var sensorDataModel = await _context.Fans.FindAsync(id);

            if (sensorDataModel == null)
            {
                return NotFound();
            }

            return sensorDataModel;
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
