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
using DeviceAPI.Services;
//using UserAPI.Data;

namespace DeviceAPI.Controllers
{
    [Route("/deviceapi/api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly DeviceContext _context;
        //private readonly IQueueService _queueService;
        private readonly HttpClient _httpClient;

        public DevicesController(DeviceContext context,
            //IQueueService queueService,
            HttpClient httpClient)
        {
            _context = context;
            //_queueService = queueService;
            _httpClient = httpClient;
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

            //_queueService.SendToQueue(Device, "update");

            return Ok(Device);
        }

        // POST: api/Devices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Device>> PostDevice(Device Device)
        {
            _context.Device.Add(Device);
            await _context.SaveChangesAsync();

            //_queueService.SendToQueue(Device, "create");

            return CreatedAtAction(nameof(PostDevice), new { id = Device.Id }, Device);
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
            else
            {

                //_queueService.SendToQueue(Device, "delete");
                _context.Device.Remove(Device);
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }


        private bool DeviceExists(Guid id)
        {
            return _context.Device.Any(e => e.Id.Equals(id));
        }
    }
}
