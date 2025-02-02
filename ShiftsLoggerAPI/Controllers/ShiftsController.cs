using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftsLoggerAPI.Data;
using ShiftsLoggerAPI.Interface;
using ShiftsLoggerAPI.Models;
using ShiftsLoggerAPI.Services;

namespace ShiftsLoggerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IShiftMapper _shiftMapper;

        public ShiftsController(ApplicationDbContext context, IShiftMapper shiftMapper)
        {
            _context = context;
            _shiftMapper = shiftMapper;

        }

        // GET: api/Shifts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShiftDTO>>> GetShifts()
        {
            return await _context.Shifts
                .Include(x => x.Employee)
                .Select(x => _shiftMapper.ShiftToDTO(x))
                .ToListAsync();
        }

        // GET: api/Shifts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShiftDTO>> GetShift(long id)
        {
            var shift = await _context.Shifts
                .Include(x => x.Employee)
                .Where(x => x.ShiftId == id)
                .Select(x => _shiftMapper.ShiftToDTO(x))
                .FirstOrDefaultAsync();

            if (shift == null)
            {
                return NotFound();
            }

            return shift;
        }

        // PUT: api/Shifts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShift(long id, ShiftDTO shiftDTO)
        {
            if (id != shiftDTO.ShiftId)
            {
                return BadRequest();
            }
            var shift = await _context.Shifts.FindAsync(id);
            if (shift == null)
            {
                return NotFound();
            }
            shift.StartTime = shiftDTO.StartTime;
            shift.EndTime = shiftDTO.EndTime;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ShiftExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Shifts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShiftDTO>> PostShift(ShiftDTO shiftDTO)
        {
            var shift = new Shift
            {
                EmployeeId = shiftDTO.EmployeeId,
                StartTime = shiftDTO.StartTime,
                EndTime = shiftDTO.EndTime,
            };
            _context.Shifts.Add(shift);
            await _context.SaveChangesAsync();

            var shiftWithEmployee = await _context.Shifts
                .Include(shift => shift.Employee)
                .FirstOrDefaultAsync(s => s.ShiftId == shift.ShiftId);

            if (shiftWithEmployee == null)
            {
                return NotFound();
            }

            return CreatedAtAction(
                nameof(GetShift),
                new { id = shift.ShiftId },
                _shiftMapper.ShiftToDTO(shiftWithEmployee));
        }

        // DELETE: api/Shifts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShift(long id)
        {
            var shift = await _context.Shifts.FindAsync(id);
            if (shift == null)
            {
                return NotFound();
            }

            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShiftExists(long id)
        {
            return _context.Shifts.Any(e => e.ShiftId == id);
        }
    }
}
