using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.Web.Models
{
   
    public class Category
    {
        public Category()
        {
            Children = new HashSet<Category>();
        }

        [Key]
        public int Id { get; set; }
        [NotNull]
        public string Name { get; set; }

        [NotNull]
        public CategoryEnum Type { get; set; }

        public int? ParentId { get; set; }
        public virtual Category Parent { get; set; }
        public virtual ICollection<Category> Children { get; set; }
    }

    public enum CategoryEnum
    {
        income = 1,
        outcome = 2
    }
}
