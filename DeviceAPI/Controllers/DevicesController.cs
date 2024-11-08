using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DeviceAPI.Models;
using DeviceAPI.Context;
using DeviceAPI.Migrations;
//using UserAPI.Data;

namespace DeviceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly DeviceContext _context;
        //private readonly UserContext _userContext;

        public DevicesController(DeviceContext context)
        {
            _context = context;
            //_userContext = userContext;
        }

        // GET: api/Devices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Device>>> GetDevices(string description = "",
            string address = "", double maxEnCons = 0.0, string ownerId = "")
        {

            var query = _context.Device.AsQueryable();

            if (!string.IsNullOrEmpty(description))
            {
                query = query.Where(d => d.Description.Contains(description));
            }
            if (!string.IsNullOrEmpty(address))
            {
                query = query.Where(d => d.Address.Contains(address));
            }
            if (maxEnCons > 0)
            {
                query = query.Where(d => d.MaxEnergyConsumption == maxEnCons);
            }
            if (!ownerId.Equals(""))
            {
                query = query.Where(d => d.UserId.Equals(ownerId));
            }

            var result = await query.ToListAsync();
            return Ok(result);

        }


        // GET: api/Devices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Device>> GetDevice(Guid id)
        {
            var Device = await _context.Device.FindAsync(id);

            if (Device == null)
            {
                return NotFound();
            }

            return Device;
        }

        // GET: api/devices/user/5
        [HttpGet("/user/{id}")]
        public async Task<ActionResult<IEnumerable<Device>>> GetDevicesByUser(Guid userId)
        {
            var Devices = await _context.Device.Where(d => d.UserId.Equals(userId)).ToListAsync();

            if (Devices == null)
            {
                return NotFound();
            }

            return Devices;
        }

        // PUT: api/Devices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDevice(Guid id, Device Device)
        {
            if (!Device.Id.Equals(id))
            {
                return BadRequest();
            }

            _context.Entry(Device).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(Device);
        }

        // POST: api/Devices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Device>> PostDevice(Device Device)
        {
            _context.Device.Add(Device);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDevice), new { id = Device.Id }, Device);
        }

        // DELETE: api/Devices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice(Guid id)
        {
            var Device = await _context.Device.FindAsync(id);
            if (Device == null)
            {
                return NotFound();
            }

            _context.Device.Remove(Device);
            await _context.SaveChangesAsync();

            //_userContext.Remove(_userContext.Devices.Find(Device.Id));
            //_userContext.SaveChanges();

            return NoContent();
        }


        private bool DeviceExists(Guid id)
        {
            return _context.Device.Any(e => e.Id.Equals(id));
        }
    }
}
