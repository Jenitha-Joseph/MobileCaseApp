using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MobileCaseApp.Data;
using MobileCaseApp.Models;
using MobileCaseApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileCaseApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubCategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        [TempData]
        public string StatusMessage { get; set; }
        
        public SubCategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: SubCategoryController
        public async Task<IActionResult> Index()
        {
            return View(await _db.SubCategory.Include(s=>s.category).ToListAsync());
        }

        
        // GET: SubCategoryController/Create
        public async Task<IActionResult> Create()
        {
            SubCategory_And_Category_ViewModel model = new SubCategory_And_Category_ViewModel() { 
                CategoryList = await _db.Category.ToListAsync(),
                SubCategory = new Models.SubCategory(),
                SubCategoryList = await _db.SubCategory.OrderBy(p=>p.Name).Select(p=>p.Name).Distinct().ToListAsync()
            };
            return View(model);
        }

        // POST: SubCategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubCategory_And_Category_ViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doesSubCategoryExist = _db.SubCategory.Include(s => s.category)
                    .Where(s => s.Name == model.SubCategory.Name && s.CategoryId == model.SubCategory.CategoryId);
                if (doesSubCategoryExist.Count() > 0)
                {
                    StatusMessage = "Error: SubCategory Exists";
                }
                else
                {
                    _db.SubCategory.Add(model.SubCategory);
                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            SubCategory_And_Category_ViewModel modelVM = new SubCategory_And_Category_ViewModel()
            {
                CategoryList = await _db.Category.ToListAsync(),
                SubCategory = new Models.SubCategory(),
                SubCategoryList = await _db.SubCategory.OrderBy(p => p.Name).Select(p => p.Name).Distinct().ToListAsync(),
                StatusMessage = StatusMessage
            };
            return View(modelVM);
        }

        public async Task<IActionResult> GetSubCategory(int id)
        {
            List<SubCategory> subCategories = new List<SubCategory>();
            subCategories = await (from subcategory in _db.SubCategory
                                   where subcategory.CategoryId == id
                                   select subcategory).ToListAsync();
            return Json(new SelectList(subCategories, "Id", "Name"));
        }



        // GET: SubCategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SubCategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SubCategoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SubCategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SubCategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
