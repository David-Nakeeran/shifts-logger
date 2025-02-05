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

            if (!shifts.Success)
            {
                return BadRequest(shifts.Message);
            }

            if (shifts.Data == null)
            {
                return NotFound("No shifts found");
            }

            var shiftDTOs = shifts.Data.Select(s => _shiftMapper.ShiftToDTO(s)).ToList();
            return Ok(shiftDTOs);
        }

        // GET: api/Shifts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShiftDTO>> GetShift(long id)
        {
            var shift = await _shiftService.GetShiftByIdAsync(id);

            if (shift.Data == null)
            {
                return NotFound("No shift found");
            }
            var shiftDTO = _shiftMapper.ShiftToDTO(shift.Data);

            return Ok(shiftDTO);
        }

        // PUT: api/Shifts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutShift(long id, ShiftDTO shiftDTO)
        {
            if (id != shiftDTO.ShiftId)
            {
                return BadRequest("Shift ID in URL does not match the request body");
            }
            var shift = await _shiftService.UpdateShift(id, shiftDTO);

            if (!shift.Success)
            {
                return BadRequest(shift.Message);
            }

            if (shift.Data == null)
            {
                return NotFound("No shift found");
            }

            var updatedShiftDTO = _shiftMapper.ShiftToDTO(shift.Data);

            return Ok(updatedShiftDTO);
        }

        // POST: api/Shifts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShiftDTO>> PostShift(ShiftDTO shiftDTO)
        {
            var createdShift = await _shiftService.CreateShift(shiftDTO);

            if (!createdShift.Success)
            {
                return BadRequest(createdShift.Message);
            }

            if (createdShift.Data == null)
            {
                return NotFound(createdShift.Message);
            }

            return CreatedAtAction(
                nameof(GetShift),
                new { id = createdShift.Data.ShiftId },
                _shiftMapper.ShiftToDTO(createdShift.Data));
        }

        // DELETE: api/Shifts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteShift(long id)
        {
            var shift = await _shiftService.DeleteShift(id);

            if (!shift.Success)
            {
                return BadRequest(shift.Message);
            }

            if (shift.Data == null)
            {
                return NotFound(shift.Message);
            }
            var deletedShiftDTO = _shiftMapper.ShiftToDTO(shift.Data);

            return Ok(deletedShiftDTO);
        }

    }
}
