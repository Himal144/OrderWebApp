using Microsoft.AspNetCore.Mvc;
using OrderWebApp.Data;
using OrderWebApp.Models;

namespace OrderWebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
            
        }
        public IActionResult Index()
        {
            
            List<Category> objCategoriesList = _db.Categories.ToList();
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
            _db.Categories.Add(obj);
            _db.SaveChanges();
            TempData["Success"] = "Category created successfully.";
            return RedirectToAction("Index");
            }
            return View();
        }


        public IActionResult Edit( int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }
            Category? dbCategoryObj = _db.Categories.Find(id);

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
                _db.Categories.Update(obj);
                _db.SaveChanges();
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
            Category? dbCategoryObj = _db.Categories.Find(id);

            if (dbCategoryObj == null)
            {
                return NotFound();
            }
            return View(dbCategoryObj);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            
                 Category? dbCategoryObj= _db.Categories.Find(id);
                 if (dbCategoryObj == null)
                {
                    return NotFound();
                }
            _db.Categories.Remove(dbCategoryObj);
            _db.SaveChanges();
            TempData["Danger"] = "Category Deleted successfully.";
            return RedirectToAction("Index");
                 
        }
    }
}
