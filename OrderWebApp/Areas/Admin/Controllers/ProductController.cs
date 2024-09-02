using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrderWebApp.DataAccess.Repository.IRepository;
using OrderWebApp.Models;
using OrderWebApp.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace OrderWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        private readonly IUnitOfWork _unitofwork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitofwork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> objCategoriesList = _unitofwork.Product.GetAll(includeProperties:"Category").ToList();
            
            return View(objCategoriesList);
        }

        public IActionResult Upsert(int? id)
        {


            ProductVM productVM = new()
            {
                CategoryList = _unitofwork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
               Product = new Product()

            };
            if (id == null || id == 0)
            {
            return View(productVM);
            }
            else
            {
                productVM.Product = _unitofwork.Product.Get(u=>u.Id == id);
                return View(productVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
               
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        // Find old image path and trim the \ at the beginning of the ImageUrl because Combine automatically appends the \
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }
                
                    // This ensures that there is always a return statement, even if the file is null.
                    //productVM.CategoryList = _unitofwork.Category.GetAll().Select(u => new SelectListItem
                    //{
                    //    Text = u.Name,
                    //    Value = u.Id.ToString()
                    //});
                 else if(productVM.Product.Id != 0)
                {
                    var existingProduct = _unitofwork.Product.GetByIdAsNoTracking(productVM.Product.Id);
                    productVM.Product.ImageUrl=existingProduct.ImageUrl;
                }


                    if (productVM.Product.Id == 0)
                    {
                        _unitofwork.Product.Add(productVM.Product);
                        TempData["Success"] = "Product Created successfully.";
                }
                    else
                    {
                        _unitofwork.Product.Update(productVM.Product);
                        TempData["Success"] = "Product Updated successfully.";
                }
                _unitofwork.Save();
                return RedirectToAction("Index");

            }
            else
            {
                productVM.CategoryList = _unitofwork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });

                TempData["Error"] = "Invalid data entered.";
                return View(productVM);
            }
        }


        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Product? dbProductObj = _unitofwork.Product.Get(u => u.Id == id);

        //    //Different method for finding the Category objects
        //    //Category? dbCategoryObj1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
        //    //Category? dbCategoryObj2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();


        //    if (dbProductObj == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(dbProductObj);
        //}

        //[HttpPost]
        //public IActionResult Edit(Product obj)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitofwork.Product.Update(obj);
        //        _unitofwork.Save();
        //        TempData["Info"] = "Product Updated successfully.";
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}

        //Action method for the delete
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Product? dbProductObj = _unitofwork.Product.Get(u => u.Id == id);

        //    if (dbProductObj == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(dbProductObj);
        //}

        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePost(int? id)
        //{

        //    Product? dbProductObj = _unitofwork.Product.Get(u => u.Id == id);
        //    if (dbProductObj == null)
        //    {
        //        return NotFound();
        //    }
        //    _unitofwork.Product.Remove(dbProductObj);
        //    _unitofwork.Save();
        //    TempData["Danger"] = "Product Deleted successfully.";
        //    return RedirectToAction("Index");

        //}

        #region API CALLS
        [HttpGet]
        public IActionResult GetALL() {
            List<Product> objCategoriesList = _unitofwork.Product.GetAll().ToList();
            return Json(new {data= objCategoriesList});
        }


        [HttpDelete]
        public IActionResult Delete( int? id)
        {
            var productToBeDeleted = _unitofwork.Product.Get(u => u.Id == id);
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitofwork.Product.Remove(productToBeDeleted);
            _unitofwork.Save();
            return Json(new { success = true, message = "Product deleted successfully" });
        }
        #endregion

    }
}
