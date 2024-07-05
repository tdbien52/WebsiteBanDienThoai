using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TranDinhBien_BanHang.Models;

namespace TranDinhBien_BanHang.Areas.Admin.Controllers
{
    public class CartController : Controller
    {
        // GET: Admin/Cart
        public ActionResult Index()
        {
            DBConn db = new DBConn();
            List<Category> categories = db.Categories.ToList();
            ViewBag.Category = categories;
            List<Cart> carts = db.Carts.ToList();
            return View(carts);
        }
    }
}