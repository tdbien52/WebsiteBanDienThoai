using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TranDinhBien_BanHang.Models;

namespace TranDinhBien_BanHang.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
            DBConn db = new DBConn();
        public ActionResult Index()
        {
            HomeModel objhomemodel = new HomeModel();
            objhomemodel.ListProduct= db.Products.ToList();
           
           objhomemodel.ListCategory= db.Categories.ToList();
            
            return View(objhomemodel);
        }
        public ActionResult Error404()
        {
            return View();  
        }
    }
}