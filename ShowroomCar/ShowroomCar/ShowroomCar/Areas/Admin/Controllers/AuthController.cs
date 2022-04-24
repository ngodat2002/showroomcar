
using ShowroomCar.Common;
using ShowroomCar.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ShowroomCar.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        // GET: Admin/Auth
        private ShowroomCarDbContext db = new ShowroomCarDbContext();
        public ActionResult login()
        {
            return View("_login");
        }
        [HttpPost]
        public ActionResult login(FormCollection fc)
        {
            String Username = fc["username"];
            string Pass = Mystring.ToMD5(fc["password"]);
            var user_account = db.users.Where(m => m.Access != 1 && m.Status == 1 && (m.Username == Username));
            var userC = db.users.Where(m => m.Username == Username && m.Access == 1);
            if (userC.Count() != 0)
            {
                ViewBag.error = "Bạn không có quyền đăng nhập";
            }
            else
            {
                if (user_account.Count() == 0)
                {
                    ViewBag.error = "Tên Đăng Nhập Không Đúng";
                }
                else
                {
                    var pass_account = db.users.Where(m => m.Access != 1 && m.Status == 1 && m.Password == Pass);
                    if (pass_account.Count() == 0)
                    {
                        ViewBag.error = "Mật Khẩu Không Đúng";
                    }
                    else
                    {
                        var user = user_account.First();
                        var userSession = new Userlogin();
                        userSession.UserName = user.Username;
                        userSession.UserID = user.ID;
                        Session.Add(CommonConstants.USER_SESSION, userSession);
                        var i = Session["SESSION_CREDENTIALS"];
                        Session["Admin_id"] = user.ID;
                        Session["Admin_user"] = user.Username;
                        Session["Admin_fullname"] = user.Fullname;
                        Response.Redirect("~/Admin");
                    }
                }
            }
            ViewBag.sess = Session["Admin_id"];
            return View("_login");

        }

        public ActionResult logout()
        {
            Session["Admin_id"] = "";
            Session["Admin_user"] = "";
            Response.Redirect("~/Admin");
            return View();
        }
        public ActionResult EditUser()
        {
            int id = 1;
            id = int.Parse(Session["Admin_id"].ToString());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User muser = db.users.Find(id);
            if (muser == null)
            {
                return HttpNotFound();
            }
            return View(muser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(User muser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(muser).State = EntityState.Modified;
                db.SaveChanges();
                Message.set_flash("Cập nhật thành công", "success");
                return RedirectToAction("EditUser");
            }
            return View(muser);
        }
    }
}