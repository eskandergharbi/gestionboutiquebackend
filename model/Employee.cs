namespace GestionBoutiqueBack.model
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; } // Email address of the employee
        public string PhoneNumber { get; set; } // Contact number of the employee
        public string Position { get; set; } // Job title of the employee
        public decimal Salary { get; set; } // Salary of the employee
        public string Address { get; set; } // Address of the employee
        public int DepartmentId { get; set; }

        public Department Department { get; set; }


        // Many-to-one relationship with Department
    }
}