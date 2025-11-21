using ContractMonthlyClaimSystem.Models;
using System.Data.Entity;

namespace ContractMonthlyClaimSystem.Data
{
    public class ClaimDbContextInitialzer : DropCreateDatabaseIfModelChanges<ClaimDbContext>
    {
        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override void InitializeDatabase(ClaimDbContext context)
        {
            base.InitializeDatabase(context);
        }

        public override string? ToString()
        {
            return base.ToString();
        }

        protected override void Seed(ClaimDbContext context)
        {
            // Users
            var admin = new User
            {
                UserId = 1,
                Username = "admin",
                PasswordHash = HashPassword("admin123"),
                Role = "HR",
                Email = "admin@university.com",
                CreatedDate = DateTime.Now,
                IsActive = true
            };

            var coordinator = new User
            {
                UserId = 2,
                Username = "coordinator",
                PasswordHash = HashPassword("coord123"),
                Role = "Coordinator",
                Email = "coordinator@university.com",
                CreatedDate = DateTime.Now,
                IsActive = true
            };

            context.Users.Add(admin);
            context.Users.Add(coordinator);

            // Lecturers
            var lecturer1 = new Lecturer
            {
                LecturerId = 1,
                FirstName = "John",
                LastName = "Smith",
                Email = "john.smith@university.com",
                PhoneNumber = "+27 11 123 4567",
                DefaultHourlyRate = 150m,
                Department = "Computer Science",
                DateJoined = DateTime.Now.AddYears(-2),
                IsActive = true
            };

            var lecturer2 = new Lecturer
            {
                LecturerId = 2,
                FirstName = "Sarah",
                LastName = "Johnson",
                Email = "sarah.johnson@university.com",
                PhoneNumber = "+27 11 234 5678",
                DefaultHourlyRate = 175m,
                Department = "Mathematics",
                DateJoined = DateTime.Now.AddYears(-3),
                IsActive = true
            };

            context.Lecturers.Add(lecturer1);
            context.Lecturers.Add(lecturer2);

            // Lecturer user accounts
            context.Users.Add(new User
            {
                UserId = 3,
                Username = "john.smith",
                PasswordHash = HashPassword("lecturer123"),
                Role = "Lecturer",
                Email = "john.smith@university.com",
                LecturerId = 1,
                CreatedDate = DateTime.Now,
                IsActive = true
            });

            context.Users.Add(new User
            {
                UserId = 4,
                Username = "sarah.johnson",
                PasswordHash = HashPassword("lecturer123"),
                Role = "Lecturer",
                Email = "sarah.johnson@university.com",
                LecturerId = 2,
                CreatedDate = DateTime.Now,
                IsActive = true
            });

            context.SaveChanges();
        }

        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}

{

}
}