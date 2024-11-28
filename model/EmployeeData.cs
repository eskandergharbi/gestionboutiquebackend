namespace GestionBoutiqueBack.model
{
    public class EmployeeData
    {
        public string Name { get; set; }
        public string Email { get; set; } // Email address of the employee
        public string PhoneNumber { get; set; } // Contact number of the employee
        public string Position { get; set; } // Job title of the employee
        public decimal Salary { get; set; } // Salary of the employee
        public string Address { get; set; } // Address of the employee
        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        // One-to-one relationship with PayrollHistory
        public ICollection<PayrollHistory> PayrollHistories { get; set; } = new List<PayrollHistory>();

        // One-to-many relationship with Absence
        public ICollection<Absence> Absences { get; set; } = new List<Absence>();

        // One-to-many relationship with Vacancy
        public ICollection<Vacation> Vacancies { get; set; } = new List<Vacation>();

        // Many-to-one relationship with Department
    }
}