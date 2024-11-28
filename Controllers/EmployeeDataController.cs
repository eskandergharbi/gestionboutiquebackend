using GestionBoutiqueBack.model;
using GestionBoutiqueBack.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GestionBoutiqueBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeDataController : ControllerBase
    {
        private readonly EmployeeDataService _employeeService;

        public EmployeeDataController(EmployeeDataService employeeService)
        {
            _employeeService = employeeService;
        }

        // POST: api/Employee
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeData employeeData)
        {
            if (employeeData == null)
            {
                return BadRequest("Employee data is null");
            }

            // Call the service method to add employee and related entities
            var employee = await _employeeService.AddEmployeeAsync(employeeData);

            if (employee == null)
            {
                return StatusCode(500, "A problem occurred while adding the employee.");
            }

            // Return the created employee with a 201 status code
            return CreatedAtAction(nameof(AddEmployee), new { id = employee.Id }, employee);
        }
        // GET: api/employeeData - List all employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeData>>> GetEmployees()
        {
            var employees = await _employeeService.GetEmployeesAsync();
            return Ok(employees);
        }

        // GET: api/employeeData/{id} - Get employee details by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeData>> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null) return NotFound();

            return Ok(employee);
        }
        [Route("api/auth")]
        [ApiController]
        public class AuthController : ControllerBase
        {
            [HttpPost("login")]
            public IActionResult Login([FromBody] LoginRequest loginRequest)
            {
                // Validate user credentials (in a real application, query the database)
                if (loginRequest.Username == "admin" && loginRequest.Password == "password") // Example credentials
                {
                    // Generate JWT
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes("yourSecretKey"); // Replace with a secure key
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[]
                        {
                    new Claim(ClaimTypes.Name, loginRequest.Username)
                }),
                        Expires = DateTime.UtcNow.AddHours(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = tokenHandler.WriteToken(token);

                    return Ok(new { Token = tokenString });
                }

                return Unauthorized();
            }
        }

        public class LoginRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

    }
}
