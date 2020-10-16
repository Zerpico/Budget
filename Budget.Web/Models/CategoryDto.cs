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
            children = new List<CategoryDto>();
        }

        public int Id { get; set; }       
        public string Name { get; set; }      
        public CategoryEnum Type { get; set; }
        public int ParentId { get; set; }
        public int LevelId { get; set; }

        public List<CategoryDto> children { get; set; }
    }
}
