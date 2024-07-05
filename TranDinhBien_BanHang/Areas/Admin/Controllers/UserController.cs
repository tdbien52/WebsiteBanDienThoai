using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TranDinhBien_BanHang.Identity;
using TranDinhBien_BanHang.Filters;
using TranDinhBien_BanHang.Models;
using TranDinhBien_BanHang.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Web.Helpers;

namespace TranDinhBien_BanHang.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class UserController : Controller
    {
        // GET: Admin/User
        public ActionResult Index()
        {
            AppDbContext db = new AppDbContext();
            List<AppUser> user = db.Users.ToList();
            return View(user);
        }

        public ActionResult Edit(string id)
        {
            AppDbContext db = new AppDbContext();
            AppUser us = db.Users.Where(u => u.Id == id).FirstOrDefault();
            return View(us);
        }
        [HttpPost]
        public ActionResult Edit(string id, AppUser us)
        {
            
            AppDbContext db = new AppDbContext(); 
            AppUser user = db.Users.Where(u => u.Id == id).FirstOrDefault();
            if (user != null)
            {
                user.UserName = us.UserName;
                user.Email= us.Email;
                user.PhoneNumber= us.PhoneNumber;
                user.City= us.City;
                user.Birthday= us.Birthday;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {

                return RedirectToAction("Edit");
            }

        }
        public ActionResult Delete(string id)
        {
            AppDbContext db = new AppDbContext();
            AppUser us = db.Users.Where(u => u.Id == id).FirstOrDefault();
            return View(us);
        }
        [HttpPost]
        public ActionResult Delete(string id, AppUser ca)
        {
            AppDbContext db = new AppDbContext();
            AppUser user = db.Users.Where(u => u.Id == id).FirstOrDefault();
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}