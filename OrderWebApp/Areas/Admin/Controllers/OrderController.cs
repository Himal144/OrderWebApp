using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderWebApp.DataAccess.Repository.IRepository;
using OrderWebApp.Models;
using OrderWebApp.Models.ViewModels;
using OrderWebApp.Utility;
using System.Security.Claims;

namespace OrderWebApp.Areas.Admin.Controllers
  
{
    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public OrderVM orderVM { get; set; }

        public OrderController( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int orderId)
        {
            orderVM = new()
            {
                OrderHeader = _unitOfWork.OrderHeader.Get(u => u.Id == orderId, includeProperties: "ApplicationUser"),
                OrderDetail = _unitOfWork.OrderDetail.GetAll(u => u.OrderHeaderId == orderId, includeProperties: "Product")
            };

            return View(orderVM);
        }

        [HttpPost]
        [Authorize(Roles=SD.Role_Admin+","+SD.Role_Employee)]
        public IActionResult UpdateOrderDetails()
        {
            var OrderHeaderFromDb = _unitOfWork.OrderHeader.Get(u => u.Id == orderVM.OrderHeader.Id);
            OrderHeaderFromDb.PhoneNumber = orderVM.OrderHeader.PhoneNumber;
            OrderHeaderFromDb.Address = orderVM.OrderHeader.Address;
            OrderHeaderFromDb.Name = orderVM.OrderHeader.Name;
            _unitOfWork.OrderHeader.Update(OrderHeaderFromDb);
            _unitOfWork.Save();
            TempData["Success"] = "Pick Up details updated successfully";
            return RedirectToAction(nameof(Details), new {orderId=OrderHeaderFromDb.Id});
        }

        [HttpPost]
        public IActionResult StartProcessing()
        {
            var OrderHeaderFromDb = _unitOfWork.OrderHeader.Get(u => u.Id == orderVM.OrderHeader.Id);
            OrderHeaderFromDb.OrderStatus = SD.StatusInProcess;
            _unitOfWork.OrderHeader.Update(OrderHeaderFromDb);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Details), new {orderId=OrderHeaderFromDb.Id});
                
                }



        [HttpPost]
        public IActionResult ShipOrder()
        {
            var OrderHeaderFromDb = _unitOfWork.OrderHeader.Get(u => u.Id == orderVM.OrderHeader.Id);
            OrderHeaderFromDb.OrderStatus = SD.StatusShipped;
            OrderHeaderFromDb.TrackingNumber = orderVM.OrderHeader.TrackingNumber;
            OrderHeaderFromDb.Carrier = orderVM.OrderHeader.Carrier;
            OrderHeaderFromDb.ShippingDate = DateTime.Now;
            if(OrderHeaderFromDb.PaymentStatus == SD.PaymentStatusDelayedPayment)
            {
                OrderHeaderFromDb.PaymentDueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(30));
            }
            _unitOfWork.OrderHeader.Update(OrderHeaderFromDb);
            _unitOfWork.Save();
            TempData["Success"] = "Order shipped successfully";
            return RedirectToAction(nameof(Details), new { orderId = OrderHeaderFromDb.Id });

        }


        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public IActionResult CancelOrder()
        {
            var OrderHeaderFromDb = _unitOfWork.OrderHeader.Get(u => u.Id == orderVM.OrderHeader.Id);
            OrderHeaderFromDb.OrderStatus = SD.StatusCancelled;
            OrderHeaderFromDb.PaymentStatus = SD.StatusRefunded;
            _unitOfWork.OrderHeader.Update(OrderHeaderFromDb);
            _unitOfWork.Save();
            TempData["Success"] = "Order cancelled successfully";
            return RedirectToAction(nameof(Details), new { orderId = OrderHeaderFromDb.Id });

        }


            #region API CALLS

            [HttpGet]
       
        public IActionResult GetALL()
        {
            List<OrderHeader> orderHeaderList;

            if (User.IsInRole(SD.Role_Admin) || User.IsInRole(SD.Role_Employee)) { 
               orderHeaderList = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser").ToList();
            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                orderHeaderList = _unitOfWork.OrderHeader.GetAll(u=>u.ApplicationUserId==userId,includeProperties: "ApplicationUser").ToList();

            }
            return Json(new { data = orderHeaderList });
        }

        #endregion

    }
}
