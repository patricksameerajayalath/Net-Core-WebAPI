using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatSam.Apps.Data.Contexts;
using PatSam.Apps.Data.Entities;
using System.Threading.Tasks;

namespace PatSam.Apps.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly PatSamDbContext _context;

        public EmployeeController(PatSamDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var employee = await _context.Employee
                .AsNoTracking()
                .ToListAsync();

            return await Task.FromResult(new JsonResult(employee));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var employee = await _context.Employee.FindAsync(id);

            if (employee == null)
            {
                return new NotFoundObjectResult($"Employee with Id {id} not found.");
            }

            return await Task.FromResult(new JsonResult(employee));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _context.Employee.FindAsync(id);

            if (employee == null)
            {
                return new NotFoundObjectResult($"Employee with Id {id} not found.");
            }

            _context.Employee.Remove(employee);
            var result = await _context.SaveChangesAsync();
            var statusCode = result > 0 ? 200 : 500;
            return await Task.FromResult(new StatusCodeResult(statusCode));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            var newEmployee = _context.Employee.Add(employee);
            await _context.SaveChangesAsync();
            return await Task.FromResult(new OkObjectResult($"Successfully created employee {employee.Id}."));
        }
    }
}