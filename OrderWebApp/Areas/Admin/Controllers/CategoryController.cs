using OrderWebApp.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using OrderWebApp.DataAccess.Data;
using OrderWebApp.Models;
using System.Collections.Generic;
using System.Linq;


namespace OrderWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitofwork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitofwork = unitOfWork;
        }

        public IActionResult Index()
        {

            List<Category> objCategoriesList = _unitofwork.Category.GetAll().ToList();
            return View(objCategoriesList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitofwork.Category.Add(obj);
                _unitofwork.Save();
                TempData["Success"] = "Category created successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }


        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? dbCategoryObj = _unitofwork.Category.Get(u => u.Id == id);

            //Different method for finding the Category objects
            //Category? dbCategoryObj1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Category? dbCategoryObj2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();


            if (dbCategoryObj == null)
            {
                return NotFound();
            }
            return View(dbCategoryObj);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitofwork.Category.Update(obj);
                _unitofwork.Save();
                TempData["Info"] = "Category Updated successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }



        //Action method for the delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? dbCategoryObj = _unitofwork.Category.Get(u => u.Id == id);

            if (dbCategoryObj == null)
            {
                return NotFound();
            }
            return View(dbCategoryObj);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {

            Category? dbCategoryObj = _unitofwork.Category.Get(u => u.Id == id);
            if (dbCategoryObj == null)
            {
                return NotFound();
            }
            _unitofwork.Category.Remove(dbCategoryObj);
            _unitofwork.Save();
            TempData["Danger"] = "Category Deleted successfully.";
            return RedirectToAction("Index");

        }
    }
}
