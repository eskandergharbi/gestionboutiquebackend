using Microsoft.EntityFrameworkCore;

namespace GestionBoutiqueBack.services
{
    using GestionBoutiqueBack.Entitiess;
    using GestionBoutiqueBack.model;
    // Data/HRManagementContext.cs
    using Microsoft.EntityFrameworkCore;

    public class HRManagementContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Absence> Absences { get; set; }
        public DbSet<PayrollHistory> PayrollHistories { get; set; }
        public DbSet<Vacation> Vacancies { get; set; }
        public DbSet<EmployeeData> EmployeeDatas { get; set; }
        public DbSet<User> Users { get; set; }  // Represents the User table in the database

        public HRManagementContext(DbContextOptions<HRManagementContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PayrollHistory>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2); // Precision of 18 and scale of 2 (e.g., 1234567890123456.78)
            modelBuilder.Entity<EmployeeData>()
          .HasNoKey();  // Marks EmployeeData as a keyless entity

            base.OnModelCreating(modelBuilder);
        }
    }
   


}
