using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TranDinhBien_BanHang.Models;
using TranDinhBien_BanHang.Filters;

namespace TranDinhBien_BanHang.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        public ActionResult Index()
        {
            DBConn db = new DBConn();
            List<Category> categories = db.Categories.ToList();
            return View(categories);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Create(Category cate)
        {
            DBConn db = new DBConn();
            if(ModelState.IsValid)
            {
                db.Categories.Add(cate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {

                return RedirectToAction("Create");
            }
        }
        public ActionResult Edit(int id)
        {
            DBConn db = new DBConn();
            Category cate = db.Categories.Where(r => r.CategoryID == id).FirstOrDefault();
            return View(cate);
        }
        [HttpPost]
        public ActionResult Edit(int id, Category ca)
        {
            DBConn db = new DBConn();
            Category cate = db.Categories.Where(r => r.CategoryID == id).FirstOrDefault();

            cate.CategoryName = ca.CategoryName;
            cate.CategoryAvatar = ca.CategoryAvatar;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            DBConn db = new DBConn();
            Category cate = db.Categories.Where(r => r.CategoryID == id).FirstOrDefault();
            return View(cate);
        }
        [HttpPost]
        public ActionResult Delete(int id, Category ca)
        {
            DBConn db = new DBConn();
            Category cate = db.Categories.Where(r => r.CategoryID == id).FirstOrDefault();
            db.Categories.Remove(cate);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}