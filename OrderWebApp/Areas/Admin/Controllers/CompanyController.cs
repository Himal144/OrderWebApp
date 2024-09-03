using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrderWebApp.DataAccess.Repository.IRepository;
using OrderWebApp.Models;
using OrderWebApp.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using OrderWebApp.Utility;

namespace OrderWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = (SD.Role_Admin))]
    public class CompanyController : Controller
    {

        private readonly IUnitOfWork _unitofwork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitofwork = unitOfWork;
         
        }
        public IActionResult Index()
        {
            List<Company> objCategoriesList = _unitofwork.Company.GetAll().ToList();
            
            return View(objCategoriesList);
        }

        public IActionResult Upsert(int? id)
        {


           
            if (id == null || id == 0)
            {
            return View(new Company());
            }
            else
            {
                //Update
                Company companyObj = _unitofwork.Company.Get(u=>u.Id == id);
                return View(companyObj);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Company companyObj)
        {
            if (ModelState.IsValid)
            { 
                    if (companyObj.Id == 0)
                    {
                        _unitofwork.Company.Add(companyObj);
                        TempData["Success"] = "Company Created successfully.";
                }
                    else
                    {
                        _unitofwork.Company.Update(companyObj);
                        TempData["Success"] = "Company Updated successfully.";
                }
                _unitofwork.Save();
                return RedirectToAction("Index");

            }
            else
            {
                TempData["Error"] = "Invalid data entered.";
                return View(companyObj);
            }
        }


      

        #region API CALLS
        [HttpGet]
        public IActionResult GetALL() {
            List<Company> objCompanyList = _unitofwork.Company.GetAll().ToList();
            return Json(new {data= objCompanyList});
        }


        [HttpDelete]
       
        public IActionResult Delete( int? id)
        {
            var companyToBeDeleted = _unitofwork.Company.Get(u => u.Id == id);
            if (companyToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            
            _unitofwork.Company.Remove(companyToBeDeleted);
            _unitofwork.Save();
            return Json(new { success = true, message = "Company deleted successfully" });
        }
        #endregion

    }
}
