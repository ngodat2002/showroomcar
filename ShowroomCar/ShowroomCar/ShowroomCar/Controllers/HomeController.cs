using ShowroomCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShowroomCar.Controllers
{
    public class HomeController : Controller
    {
        private ShowroomCarDbContext db = new ShowroomCarDbContext();
        // GET: Home
        public ActionResult Index()
        {
            var list = db.Cars.Where(m => m.Status != 0).OrderByDescending(m => m.ID).ToList();
            return View("Index",list);
        }

        public ActionResult Detail(int? id)
        {
            Car mpost = db.Cars.Find(id);
            return View("Detail", mpost);
        }
        public ActionResult OrderView(int Id, int type, float totalPrice)
        {
            ViewBag.id = Id;
            ViewBag.type = type;
            ViewBag.totalPrice = totalPrice;
            return View("OrderView");
        }
        [HttpPost]
        public ActionResult OrderView(Order order)
        {
            order.Status = 2;
            order.PaymentStatus = 0;
            order.CreateDate = DateTime.Now;
            order.Code = "ORDERCAR_" + order.ID;
            db.Orders.Add(order);
            db.SaveChanges();
            return Redirect("~/home");
        }

        public ActionResult Sreach(string subscribe,string SearchString)
        {
            var list = db.Cars.Where(m => m.Status != 0 && m.Name.Contains(SearchString)).OrderByDescending(m => m.ID).ToList();
            return View("Index", list);
        }
    }
}