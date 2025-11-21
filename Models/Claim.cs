using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractMonthlyClaimSystem.Models
{
    public class Claim
    {
        [Key]
        public int ClaimId { get; set; }

        [Required]
        public int LecturerId { get; set; }

        [Required]
        [Range(0, 200, ErrorMessage = "Hours must be between 0 and 200")]
        public decimal HoursWorked { get; set; }

        [Required]
        [Range(20, 500, ErrorMessage = "Hourly rate must be between $20 and $500")]
        public decimal HourlyRate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal FinalPayment { get; set; }

        [Required]
        public DateTime SubmissionDate { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected

        [MaxLength(500)]
        public string Notes { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public int? ApprovedBy { get; set; }

        // Navigation property
        [ForeignKey("LecturerId")]
        public virtual Lecturer Lecturer { get; set; }

        [ForeignKey("ApprovedBy")]
        public virtual User Approver { get; set; }
    }
}

