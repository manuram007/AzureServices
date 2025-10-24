
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AzureServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeRepository _repository;

        public EmployeesController(EmployeeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _repository.GetAllAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _repository.GetByIdAsync(id);
            if (employee == null)
                return NotFound();
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            var newId = await _repository.CreateAsync(employee);
            employee.Id = newId;
            return CreatedAtAction(nameof(GetById), new { id = newId }, employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Employee employee)
        {
            if (id != employee.Id)
                return BadRequest();

            var rows = await _repository.UpdateAsync(employee);
            if (rows == 0)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var rows = await _repository.DeleteAsync(id);
            if (rows == 0)
                return NotFound();

            return NoContent();

        }
    }
}
