using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SafHackathon.Domain.Models
{
    public class Card
    {
        public int VoucherNumber { get; set; }
        public string SerialNumber { get; set; }

        public DateTime ExpiryDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal VoucherAmount { get; set; }

        [RegularExpression("^[A-Z]$", ErrorMessage = "Invalid status")]
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

    }
}
