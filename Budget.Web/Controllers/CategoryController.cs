using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Budget.Web.Data;
using Budget.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Budget.Web.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        public string Message { get; set; }
        private readonly BudgetDbContext _dbContext;

        public CategoryController(BudgetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            //тестовые данные
            if (_dbContext.Categories.Count() == 0)
            {
                var parent = new Category() { Name = "Продукты", ParentId = null };
                _dbContext.Categories.Add(parent);
                _dbContext.SaveChanges();

                var ovoshi = new Category() { Name = "Овощи", ParentId = parent.Id };
                _dbContext.Categories.Add(ovoshi);
                _dbContext.Categories.Add(new Category() { Name = "Макароны", ParentId = parent.Id });
                _dbContext.Categories.Add(new Category() { Name = "Фрукты", ParentId = parent.Id });
                _dbContext.SaveChanges();

                _dbContext.Categories.Add(new Category() { Name = "Картошка", ParentId = ovoshi.Id });
                _dbContext.Categories.Add(new Category() { Name = "Помидоры", ParentId = ovoshi.Id });
                _dbContext.Categories.Add(new Category() { Name = "Лук", ParentId = ovoshi.Id });
                _dbContext.SaveChanges();
            }
            //давай загрузим всё рекурсивно
            //https://www.codeproject.com/Articles/1077799/Creating-Dynamics-Tree-View-Menu-in-ASP-NET-MVC-in?msg=5220037
            //https://www.codeproject.com/Articles/337439/Tree-View-with-CRUD-operations-drag-and-drop-DnD-a
            var category = GetCategoryByIdRecursive(1);
          
            return View(category);
        }

                   

        public IEnumerable<Category> GetCategoryByIdRecursive(int id)
        {
            var parent = _dbContext.Categories.Where(t => t.ParentId == null)
            .Select(t => new Category
            {
                Id = t.Id,
                Name = t.Name,
                ParentId = t.ParentId ?? 0               
            }).ToList();

            foreach(var par in parent)
                par.Children = (ICollection<Category>)GetChildrenByParentId(par.Id);            
            return parent;
        }
        private IEnumerable<Category> GetChildrenByParentId(int parentId)
        {
            var children = new List<Category>();
            var threads = _dbContext.Categories.Where(x => x.ParentId == parentId);
            foreach (var t in threads)
            {
                var thread = new Category
                {
                    Id = t.Id,
                    ParentId = t.ParentId ?? 0,
                    Name = t.Name,                    
                    Children = (ICollection<Category>)GetChildrenByParentId(t.Id)
                };
                children.Add(thread);
            }
            return children;
        }


        [HttpPost]
        public async Task<IActionResult> AddCategory(string name)
        {
            if (string.IsNullOrEmpty(name))
                ModelState.AddModelError(string.Empty, "Имя не может быть пустым");
            else
            {
                var parent = _dbContext.Categories.Where(d => d.Id == 4).FirstOrDefault();
                await _dbContext.Categories.AddAsync(new Models.Category() { Name = name, Parent = parent });
                await _dbContext.SaveChangesAsync();
            }
            

            return View(name);
        }
    }
}