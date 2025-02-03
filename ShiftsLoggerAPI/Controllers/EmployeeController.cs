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
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmployeeMapper _employeeMapper;

        public EmployeeController(ApplicationDbContext context, IEmployeeMapper employeeMapper)
        {
            _context = context;
            _employeeMapper = employeeMapper;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployees()
        {
            return await _context.Employees
                .Include(x => x.Shifts)
                .Select(x => _employeeMapper.EmployeeToDTO(x))
                .ToListAsync();
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployee(long id)
        {
            var employee = await _context.Employees
                .Include(x => x.Shifts)
                .Where(x => x.EmployeeId == id)
                .Select(x => _employeeMapper.EmployeeToDTO(x))
                .FirstOrDefaultAsync();

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employee/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(long id, EmployeeDTO employeeDTO)
        {
            if (id != employeeDTO.EmployeeId)
            {
                return BadRequest();
            }
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }
            employee.Name = employeeDTO.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!EmployeeExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Employee
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployeeDTO>> PostEmployee(EmployeeDTO employeeDTO)
        {
            var employee = new Employee
            {
                Name = employeeDTO.Name
            };
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            var employeeWithShifts = await _context.Employees
                .Include(employee => employee.Shifts)
                .FirstOrDefaultAsync(e => e.EmployeeId == e.EmployeeId);

            if (employeeWithShifts == null)
            {
                return NotFound();
            }

            return CreatedAtAction(nameof(GetEmployee),
                new { id = employee.EmployeeId },
                _employeeMapper.EmployeeToDTO(employeeWithShifts));
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(long id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(long id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
