using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity; // Required for DbContext
using YourProject.Models; // Namespace where Claim class lives

namespace ContractMonthlyClaimSystem.Data
{
    
    {
        public class ClaimDbContext : DbContext
        {
            public DbSet<Claim> Claims { get; set; }

            // You can add more DbSets here for other tables
            // public DbSet<Customer> Customers { get; set; }

            // Optional: specify the connection string
            public ClaimDbContext() : base("name=ClaimDbConnectionString")
            {
            }
        }
    }

