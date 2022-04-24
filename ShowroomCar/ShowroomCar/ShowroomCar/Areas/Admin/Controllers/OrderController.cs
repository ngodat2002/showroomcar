using ShowroomCar.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ShowroomCar.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        // GET: Admin/Order
        private ShowroomCarDbContext db = new ShowroomCarDbContext();
        // GET: Admin/Order
        public ActionResult Index()
        {
            var list = db.Orders.Where(m => m.Status != 0 && m.Type == 3).OrderByDescending(m => m.ID).ToList();
            return View(list);
        }

        // GET: Admin/Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order mOrder = db.Orders.Find(id);
            if (mOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.carOrder = db.Cars.Find(mOrder.CarID);
            return View(mOrder);
        }

        public ActionResult Trash()
        {
            var list = db.Orders.Where(m => m.Status == 0 && m.Type == 3).OrderByDescending(m => m.ID).ToList();
            return View("Trash", list);
        }

        //status
        public ActionResult Status(int id)
        {
            Order mOrder = db.Orders.Find(id);
            mOrder.Status = (mOrder.Status == 1) ? 2 : 1;
            db.Entry(mOrder).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Change status success", "success");
            return RedirectToAction("Index");
        }
        public ActionResult Deltrash(int id)
        {
            Order mOrder = db.Orders.Find(id);
            mOrder.Status = 0;
            db.SaveChanges();
            Message.set_flash("Delete success", "success");
            return RedirectToAction("Index");
        }

        public ActionResult Retrash(int id)
        {
            Order mOrder = db.Orders.Find(id);
            mOrder.Status = 2;
            db.Entry(mOrder).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Retrash success", "success");
            return RedirectToAction("trash");
        }
        public ActionResult deleteTrash(int id)
        {
            Order mOrder = db.Orders.Find(id);
            db.Orders.Remove(mOrder);
            db.SaveChanges();
            Message.set_flash("Delete success", "success");
            return RedirectToAction("trash");
        }
        public ActionResult CheckPaid(int? id)
        {
            Order mOrder = db.Orders.Find(id);
            mOrder.Type = 3;
            mOrder.PaymentMethod = "Cash";
            mOrder.PaymentStatus = (mOrder.PaymentStatus == 1) ? 0 : 1;
            db.Entry(mOrder).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Create order success", "success");
            return Redirect("~/admin/order/index");
        }
    }
}