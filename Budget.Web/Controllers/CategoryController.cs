using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Budget.Web.Data;
using Budget.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Budget.Web.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        public string Message { get; set; }
        private readonly ApplicationDbContext _dbContext;

        public CategoryController(ApplicationDbContext dbContext)
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
            //var category = GetCategoryByIdRecursive(1);

            
            return View(GetTreeCatalog());
        }


        IEnumerable<CategoryDto> GetTreeCatalog()
        {
            List<CategoryDto> result = new List<CategoryDto>();
            var catalog = _dbContext.Categories.ToList();

            result = catalog.Where(d => d.ParentId == null).Select(c=> new CategoryDto() { Id = c.Id, Name=c.Name, ParentId = c.ParentId.HasValue ? c.ParentId.Value : 0, Type =c.Type, LevelId =1 }).ToList();

            for(int i=0;i<result.Count;i++)
            {
                BuildTree(result[i], catalog, 1);
            }
            return result;
        }

        static void BuildTree(CategoryDto con, List<Category> connectionList, int level)
        {
            int copyLevel = level + 1;
            foreach(var c in connectionList.Where(c => (c.ParentId.HasValue ? c.ParentId.Value : -1) == con.Id))
                con.children.Add(new CategoryDto() { Id = c.Id, Name = c.Name, ParentId = c.ParentId.HasValue ? c.ParentId.Value : 0, Type = c.Type, LevelId = copyLevel });
            foreach (CategoryDto c in con.children) BuildTree(c, connectionList, copyLevel);
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

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _dbContext.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
            return RedirectPermanent("~/Category/Index");
        }

        // GET: Payment/Create
        public IActionResult Create()
        {
            var categoryTypes = Enum.GetValues(typeof(Models.CategoryEnum)).Cast<Models.CategoryEnum>().Select(t => new Tuple<int,string>(((int)t), t.ToString() ));

            var categories = _dbContext.Categories.ToList();
            categories.Add(new Category() { Id = -1, Name = "<Нет>" });

            ViewData["ParentId"] = new SelectList(categories, nameof(Category.Id), nameof(Category.Name), -1);
            ViewData["Type"] = new SelectList(categoryTypes, "Item1", "Item2", 2);
            return View();
        }

        // POST: Payment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Type,ParentId")] Category category)
        {           
            if (ModelState.IsValid)
            {
                _dbContext.Add(category);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(_dbContext.Categories, nameof(Category.Id), nameof(Category.Name), category.ParentId);
            return View(category);
        }
    }
}