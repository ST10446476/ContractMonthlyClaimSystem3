using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractMonthlyClaimSystem.Models
{
    public class Lecturer
    {
        [Key]
        public int LecturerId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Phone]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        public decimal DefaultHourlyRate { get; set; }

        [MaxLength(200)]
        public string Department { get; set; }

        public DateTime DateJoined { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation property
        public virtual ICollection<Claim> Claims { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}"
    }
}

