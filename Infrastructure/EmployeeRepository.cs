using AzureServices.Data;
using Dapper; 

namespace AzureServices
{
    public class EmployeeRepository
    {
        private readonly DapperContext _context;

        public EmployeeRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            var query = "SELECT * FROM Employees";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Employee>(query);
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Employees WHERE Id = @Id";
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Employee>(query, new { Id = id });
        }

        public async Task<int> CreateAsync(Employee employee)
        {
            var query = @"INSERT INTO Employees (Name, Email, Department, Salary)
                          VALUES (@Name, @Email, @Department, @Salary);
                          SELECT CAST(SCOPE_IDENTITY() as int)";
            using var connection = _context.CreateConnection();
            return await connection.ExecuteScalarAsync<int>(query, employee);
        }

        public async Task<int> UpdateAsync(Employee employee)
        {
            var query = @"UPDATE Employees 
                          SET Name = @Name, Email = @Email, Department = @Department, Salary = @Salary 
                          WHERE Id = @Id";
            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync(query, employee);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var query = "DELETE FROM Employees WHERE Id = @Id";
            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync(query, new { Id = id });
        }
    }
}
