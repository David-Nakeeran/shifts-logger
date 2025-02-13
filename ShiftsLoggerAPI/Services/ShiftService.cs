using Microsoft.EntityFrameworkCore;
using ShiftsLoggerAPI.Data;
using ShiftsLoggerAPI.Interface;
using ShiftsLoggerAPI.Models;


namespace ShiftsLoggerAPI.Services;

public class ShiftService : IShiftService
{
    private readonly IShiftRepository _shiftRepo;

    public ShiftService(IShiftRepository shiftRepository)
    {
        _shiftRepo = shiftRepository;
    }

    public async Task<ServiceResponse<List<Shift>>> GetAllShiftsAsync()
    {
        ServiceResponse<List<Shift>> _response = new ServiceResponse<List<Shift>>();

        try
        {
            var shiftList = await _shiftRepo.GetAllWithEmployeeAsync();
            if (shiftList.Any())
            {
                _response.Success = true;
                _response.Message = "Ok";
                _response.Data = shiftList;
            }
            else
            {
                _response.Success = false;
                _response.Message = "No shifts found";
            }
        }
        catch (Exception ex)
        {
            // log error
            _response.Success = false;
            _response.Message = $"{ex.Message}";
        }
        return _response;
    }

    public async Task<ServiceResponse<Shift>> GetShiftByIdAsync(long id)
    {
        ServiceResponse<Shift> _response = new ServiceResponse<Shift>();

        try
        {
            var shift = await _shiftRepo.GetByIdAsync(id);

            if (shift == null)
            {
                _response.Success = false;
                _response.Message = $"Shift with ID {id} not found";
                return _response;
            }

            _response.Success = true;
            _response.Message = "Ok";
            _response.Data = shift;

        }
        catch (Exception ex)
        {
            _response.Success = false;
            _response.Message = $"{ex.Message}";
            // Add Ilogger

        }
        return _response;
    }

    public async Task<ServiceResponse<Shift>> UpdateShift(long id, ShiftDTO shiftDTO)
    {
        ServiceResponse<Shift> _response = new ServiceResponse<Shift>();
        // try
        // {
        //     var savedShift = await _context.Shifts
        //                 .Include(s => s.Employee)
        //                 .FirstOrDefaultAsync(x => x.ShiftId == id);

        //     if (savedShift == null)
        //     {
        //         _response.Success = false;
        //         _response.Message = "NotFound";
        //         _response.Data = null;
        //         return _response;
        //     }

        //     savedShift.StartTime = shiftDTO.StartTime;
        //     savedShift.EndTime = shiftDTO.EndTime;

        //     await _context.SaveChangesAsync();

        //     _response.Success = true;
        //     _response.Message = "Updated";
        //     _response.Data = savedShift;
        // }
        // catch (Exception ex)
        // {
        //     _response.Success = false;
        //     _response.Message = "Error";
        //     _response.Data = null;
        // }
        // return _response;
    }

    public async Task<ServiceResponse<Shift>> CreateShift(ShiftDTO shiftDTO)
    {
        ServiceResponse<Shift> _response = new ServiceResponse<Shift>();

        try
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
                _response.Success = false;
                _response.Message = "NotFound";
                _response.Data = null;
                return _response;
            }

            _response.Success = true;
            _response.Message = "Ok";
            _response.Data = shiftWithEmployee;
        }
        catch (Exception ex)
        {
            _response.Success = false;
            _response.Message = "Error";
            _response.Data = null;
        }

        return _response;
    }

    public async Task<ServiceResponse<Shift>> DeleteShift(long id)
    {
        ServiceResponse<Shift> _response = new ServiceResponse<Shift>();
        try
        {
            var shift = await _context.Shifts.FindAsync(id);

            if (shift == null)
            {
                _response.Success = false;
                _response.Message = "NotFound";
                _response.Data = null;
                return _response;
            }

            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync();

        }
        catch (Exception ex)
        {
            _response.Success = false;
            _response.Message = "Error";
            _response.Data = null;
        }
        return _response;
    }
}