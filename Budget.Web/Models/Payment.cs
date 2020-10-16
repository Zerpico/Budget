using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.Web.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        [NotNull]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [NotNull]
        [Column(TypeName = "decimal (18,2)")]
        public double Money { get; set; }

        [NotNull]
        public int CategoryId { get; set; }
        public string UserId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
