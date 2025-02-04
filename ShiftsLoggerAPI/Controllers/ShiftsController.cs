using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftsLoggerAPI.Data;
using ShiftsLoggerAPI.Interface;
using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IShiftService _shiftService;
        private readonly IShiftMapper _shiftMapper;

        public ShiftsController(ApplicationDbContext context, IShiftService shiftService, IShiftMapper shiftMapper)
        {
            _context = context;
            _shiftService = shiftService;
            _shiftMapper = shiftMapper;

        }

        // GET: api/Shifts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShiftDTO>>> GetShifts()
        {
            var shifts = await _shiftService.GetAllShiftsAsync();
            var shiftDTOs = shifts.Select(s => _shiftMapper.ShiftToDTO(s)).ToList();
            return Ok(shiftDTOs);
        }

        // GET: api/Shifts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShiftDTO>> GetShift(long id)
        {
            var shift = await _shiftService.GetShiftByIdAsync(id);

            if (shift == null)
            {
                return NotFound();
            }
            var shiftDTO = _shiftMapper.ShiftToDTO(shift);

            return Ok(shiftDTO);
        }

        // PUT: api/Shifts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutShift(long id, ShiftDTO shiftDTO)
        {
            if (id != shiftDTO.ShiftId)
            {
                return BadRequest();
            }
            var shift = await _shiftService.UpdateShift(id, shiftDTO);
            if (shift == null)
            {
                return NotFound();
            }

            var updatedShiftDTO = _shiftMapper.ShiftToDTO(shift);

            return Ok(updatedShiftDTO);
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

    }
}
