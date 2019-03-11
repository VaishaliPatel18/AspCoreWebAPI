using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoreWebAPI.Model;

namespace CoreWebAPI.Controller
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeModelsController : ControllerBase
    {
        private readonly EmployeeContext _context;

        public EmployeeModelsController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeModels
        [HttpGet]
        public IEnumerable<EmployeeModel> GetEmployees()
        {
            return _context.Employees;
        }

        // GET: api/EmployeeModels/101
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employeeModel = await _context.Employees.FindAsync(id);

            if (employeeModel == null)
            {
                return NotFound();
            }

            return Ok(employeeModel);
        }

        // PUT: api/EmployeeModels/101
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeModel([FromRoute] int id, [FromBody] EmployeeModel employeeModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employeeModel.EmpId)
            {
                return BadRequest();
            }

            _context.Entry(employeeModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeModelExists(id))
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

        // POST: api/EmployeeModels
        [HttpPost]
        public async Task<IActionResult> PostEmployeeModel([FromBody] EmployeeModel employeeModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Employees.Add(employeeModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployeeModel", new { id = employeeModel.EmpId }, employeeModel);
        }

        // DELETE: api/EmployeeModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employeeModel = await _context.Employees.FindAsync(id);
            if (employeeModel == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employeeModel);
            await _context.SaveChangesAsync();

            return Ok(employeeModel);
        }

        private bool EmployeeModelExists(int id)
        {
            return _context.Employees.Any(e => e.EmpId == id);
        }
    }
}