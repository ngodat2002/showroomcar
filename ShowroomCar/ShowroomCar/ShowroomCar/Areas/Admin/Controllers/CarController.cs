using ShowroomCar.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ShowroomCar.Areas.Admin.Controllers
{
    public class CarController : Controller
    {
        private ShowroomCarDbContext db = new ShowroomCarDbContext();

        // GET: Admin/Car
        public ActionResult Index()
        {
            var list = db.Cars.Where(m => m.Status != 0).OrderByDescending(m => m.ID).ToList();
            return View(list);
        }
        // GET: Admin/Post/Create
        public ActionResult Create()
        {
            ViewBag.listTopic = db.Cars.Where(m => m.Status == 1).ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Car car)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file;
                string slug = Mystring.ToSlug(car.Name.ToString());
                string namecate = "car";
                file = Request.Files["img"];
                string filename = file.FileName.ToString();
                string ExtensionFile = Mystring.GetFileExtension(filename);
                string namefilenew = namecate + "/" + slug + "." + ExtensionFile;
                var path = Path.Combine(Server.MapPath("~/public/images/Product/"), namefilenew);
                var folder = Server.MapPath("~/public/images/Product/" + namecate);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                file.SaveAs(path);
                car.Image = namefilenew;
                db.Cars.Add(car);
                db.SaveChanges();
                Message.set_flash("Add success", "success");
                return RedirectToAction("Index");
            }
            Message.set_flash("Add faild", "danger");
            return View(car);
        }

        // GET: Admin/Post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            ViewBag.listTopic = db.Cars.Where(m => m.Status != 0).ToList();
            return View(car);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Car car)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file;
                file = Request.Files["img"];
                string filename = file.FileName.ToString();
                if (filename.Equals("") == false)
                {
                    string namecate = "car";
                    string ExtensionFile = Mystring.GetFileExtension(filename);
                    string slug = Mystring.ToSlug(car.Name.ToString());
                    string namefilenew = namecate + "/" + slug + "." + ExtensionFile;
                    var path = Path.Combine(Server.MapPath("~/public/images/product/"), namefilenew);
                    var folder = Server.MapPath("~/public/images/product/" + namecate);
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                    file.SaveAs(path);
                    car.Image = namefilenew;
                }
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                Message.set_flash("Success", "success");

                return RedirectToAction("Index");
            }
            Message.set_flash("Faild", "danger");
            return View(car);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car mpost = db.Cars.Find(id);
            if (mpost == null)
            {
                return HttpNotFound();
            }
            return View(mpost);
        }
        public ActionResult Status(int id)
        {
            Car mpost = db.Cars.Find(id);
            mpost.Status = (mpost.Status == 1) ? 2 : 1;
            db.Entry(mpost).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Change status success", "success");
            return RedirectToAction("Index");
        }
        public ActionResult trash()
        {
            var list = db.Cars.Where(m => m.Status == 0).ToList();
            return View("Trash", list);
        }
        public ActionResult Deltrash(int id)
        {
            Car mpost = db.Cars.Find(id);
            mpost.Status = 0; 
            db.Entry(mpost).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Delete success", "success");
            return RedirectToAction("Index");
        }
        public ActionResult Retrash(int id)
        {
            Car mpost = db.Cars.Find(id);
            mpost.Status = 2;
            db.Entry(mpost).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Retrash success", "success");
            return RedirectToAction("trash");
        }
        public ActionResult deleteTrash(int id)
        {
            Car mpost = db.Cars.Find(id); 
            db.Cars.Remove(mpost);
            db.SaveChanges();
            Message.set_flash("Delete success", "success");
            return RedirectToAction("trash");
        }
    }
}