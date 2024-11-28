using GestionBoutiqueBack.model;
using GestionBoutiqueBack.services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GestionBoutiqueBack.Services
{
    public class EmployeeDataService
    {
        private readonly HRManagementContext _context;

        public EmployeeDataService(HRManagementContext context)
        {
            _context = context;
        }

        // Add an employee along with related entities
        public async Task<Employee> AddEmployeeAsync(EmployeeData employeeData)
        {
            // Create and add employee
            var employee = new Employee
            {
                Name = employeeData.Name,
                Email = employeeData.Email,
                PhoneNumber = employeeData.PhoneNumber,
                Position = employeeData.Position,
                Salary = employeeData.Salary,
                Address = employeeData.Address,
                DepartmentId = employeeData.DepartmentId,
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync(); // Save to generate Id

            // Associate and add PayrollHistories
            if (employeeData.PayrollHistories?.Any() == true)
            {
                foreach (var payroll in employeeData.PayrollHistories)
                {
                    payroll.EmployeeId = employee.Id;
                    _context.PayrollHistories.Add(payroll);
                }
                await _context.SaveChangesAsync();
            }

            // Associate and add Absences
            if (employeeData.Absences?.Any() == true)
            {
                foreach (var absence in employeeData.Absences)
                {
                    absence.EmployeeId = employee.Id;
                    _context.Absences.Add(absence);
                }
                await _context.SaveChangesAsync();
            }

            // Associate and add Vacancies
            if (employeeData.Vacancies?.Any() == true)
            {
                foreach (var vacation in employeeData.Vacancies)
                {
                    vacation.EmployeeId = employee.Id;
                    _context.Vacancies.Add(vacation);
                }
                await _context.SaveChangesAsync();
            }

            return employee;
        }

        public async Task<EmployeeData> GetEmployeeByIdAsync(int employeeId)
        {
            // Fetch the employee entity (basic details) by ID
            var employee = await _context.Employees
                .Include(e => e.Department) // Include the department if needed
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee == null) return null;

            // Manually populate the EmployeeData DTO with related data by querying each table
            var employeeData = new EmployeeData
            {
                Name = employee.Name,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Position = employee.Position,
                Salary = employee.Salary,
                Address = employee.Address,
                DepartmentId = employee.DepartmentId,
                Department = employee.Department != null ? new Department { Id = employee.Department.Id, Name = employee.Department.Name } : null
            };

            // Query PayrollHistory, Absence, and Vacation tables by EmployeeId and map to the DTO
            employeeData.PayrollHistories = await _context.PayrollHistories
                .Where(ph => ph.EmployeeId == employeeId)
                .Select(ph => new PayrollHistory
                {
                    Date = ph.Date,
                    Amount = ph.Amount,
                    EmployeeId = ph.EmployeeId
                })
                .ToListAsync();

            employeeData.Absences = await _context.Absences
                .Where(a => a.EmployeeId == employeeId)
                .Select(a => new Absence
                {
                    StartDate = a.StartDate,
                    EndDate = a.EndDate,
                    Reason = a.Reason,
                    EmployeeId = a.EmployeeId
                })
                .ToListAsync();

            employeeData.Vacancies = await _context.Vacancies
                .Where(v => v.EmployeeId == employeeId)
                .Select(v => new Vacation
                {
                    StartDate = v.StartDate,
                    EndDate = v.EndDate,
                    Reason = v.Reason,
                    IsApproved = v.IsApproved,
                    EmployeeId = v.EmployeeId
                })
                .ToListAsync();

            return employeeData;
        }
        public async Task<IEnumerable<EmployeeData>> GetEmployeesAsync()
        {
            // Fetch basic details for all employees
            var employees = await _context.Employees
                .Include(e => e.Department) // Include department if necessary
                .ToListAsync();

            // Populate the EmployeeData DTO list
            var employeeDataList = new List<EmployeeData>();
            foreach (var employee in employees)
            {
                var employeeData = new EmployeeData
                {
                    Name = employee.Name,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    Position = employee.Position,
                    Salary = employee.Salary,
                    Address = employee.Address,
                    DepartmentId = employee.DepartmentId,
                    Department = employee.Department != null ? new Department { Id = employee.Department.Id, Name = employee.Department.Name } : null
                };

                // Fetch related data for each employee using EmployeeId
                employeeData.PayrollHistories = await _context.PayrollHistories
                    .Where(ph => ph.EmployeeId == employee.Id)
                    .Select(ph => new PayrollHistory
                    {
                        Date = ph.Date,
                        Amount = ph.Amount,
                        EmployeeId = ph.EmployeeId
                    })
                    .ToListAsync();

                employeeData.Absences = await _context.Absences
                    .Where(a => a.EmployeeId == employee.Id)
                    .Select(a => new Absence
                    {
                        StartDate = a.StartDate,
                        EndDate = a.EndDate,
                        Reason = a.Reason,
                        EmployeeId = a.EmployeeId
                    })
                    .ToListAsync();

                employeeData.Vacancies = await _context.Vacancies
                    .Where(v => v.EmployeeId == employee.Id)
                    .Select(v => new Vacation
                    {
                        StartDate = v.StartDate,
                        EndDate = v.EndDate,
                        Reason = v.Reason,
                        IsApproved = v.IsApproved,
                        EmployeeId = v.EmployeeId
                    })
                    .ToListAsync();

                employeeDataList.Add(employeeData);
            }

            return employeeDataList;
        }

    }
}
