using ContractMonthlyClaimSystem.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ContractMonthlyClaimSystem.Data
{
    public class ClaimDbContext : DbContext
    {
        // Constructor uses connection string name from App.config / Web.config
        public ClaimDbContext()
            : base("name=ClaimSystemDB")
        {
        }

        public DbSet<Claim> Claims { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)

        {
            // Remove EF6 pluralizing conventions
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            ConfigureClaim(modelBuilder);
            ConfigureLecturer(modelBuilder);
            ConfigureUser(modelBuilder);
            ConfigureAuditLog(modelBuilder);
        }

        private void ConfigureClaim(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Claim>()
                .HasKey(c => c.ClaimId);

            modelBuilder.Entity<Claim>()
                .Property(c => c.HoursWorked)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Claim>()
                .Property(c => c.HourlyRate)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Claim>()
                .Property(c => c.FinalPayment)
                .HasPrecision(18, 2);

            // Relationships
            modelBuilder.Entity<Claim>()
                .HasRequired(c => c.Lecturer)
                .WithMany(l => l.Claims)
                .HasForeignKey(c => c.LecturerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Claim>()
                .HasOptional(c => c.Approver)
                .WithMany()
                .HasForeignKey(c => c.ApprovedBy)
                .WillCascadeOnDelete(false);
        }

        private void ConfigureLecturer(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lecturer>()
                .HasKey(l => l.LecturerId);

            modelBuilder.Entity<Lecturer>()
                .Property(l => l.DefaultHourlyRate)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Lecturer>()
                .HasIndex(l => l.Email)
                .IsUnique();
        }

        private void ConfigureUser(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasOptional(u => u.Lecturer)
                .WithMany()
                .HasForeignKey(u => u.LecturerId)
                .WillCascadeOnDelete(false);
        }

        private void ConfigureAuditLog(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditLog>()
                .HasKey(a => a.AuditId);

            modelBuilder.Entity<AuditLog>()
                .HasRequired(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .WillCascadeOnDelete(false);
        }

        internal void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}

