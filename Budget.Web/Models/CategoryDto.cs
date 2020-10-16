using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.Web.Models
{
    public class CategoryDto
    {
        public CategoryDto()
        {
            Subs = new List<CategoryDto>();
        }

        public int Id { get; set; }       
        public string Title { get; set; }
        public int ParentId { get; set; }
        public CategoryEnum Type { get; set; }
        
        public int LevelId { get; set; }

        public List<CategoryDto> Subs { get; set; }
    }
}
