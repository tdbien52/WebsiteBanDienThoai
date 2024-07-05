using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using TranDinhBien_BanHang.Models;
using TranDinhBien_BanHang.Filters;
using System.Data.Entity;
using System.IO;

namespace TranDinhBien_BanHang.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class ProductController : Controller
    {
        // GET: Admin/Product
        public ActionResult Index(int id, string search = "", int page = 1)
        {
            DBConn db = new DBConn();
            // Lấy tên danh mục hiện tại
            var categoryName = db.Categories.Find(id).CategoryName;
            // Lưu tên danh mục vào viewbag
            //ViewBag.Name = categoryName;
            // hiển thị danh mục theo sản phẩm av tiềm kiếm
            var lstProduct = db.Products.Where(r => r.CategoryID == id && r.ProductName.Contains(search)).ToList();
            ViewBag.CategoryID = id;

            //var lstProduct = db.Products.Where(r =>r.ProductName.Contains(search)).ToList();
            ViewBag.Search = search;
            // lấy danh mục
            List<Category> categories = db.Categories.ToList();
            ViewBag.Category = categories;

            // phân trang
            int NoOfRecordPerPage = 6;// chi muon hien thi 6 dong 
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(lstProduct.Count)// tinh ra so luong trang
                / Convert.ToDouble(NoOfRecordPerPage)));  // math.celling() la phep toan dung de lam tron
            int NoOfRecordToSkip = (page - 1) * NoOfRecordPerPage; // so record can skip
            ViewBag.Page = page;
            ViewBag.NoOfPages = NoOfPages;
            lstProduct = lstProduct.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();
            return View(lstProduct);
        }

        // tạo sản phẩm mới
        public ActionResult Create(int id)
        {
            DBConn db = new DBConn();
            //navigation
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Brand = db.Brands.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product prod, HttpPostedFileBase imageFile)
        {
            DBConn db = new DBConn();

            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    // Kiểm tra loại file
                    var allowedExtensions = new[] { ".jpg", ".png" };
                    var fileEx = Path.GetExtension(imageFile.FileName).ToLower();

                    if (!allowedExtensions.Contains(fileEx))
                    {
                        ModelState.AddModelError("Image", "Chỉ chấp nhận hình ảnh dạng JPG hoặc PNG.");
                        ViewBag.Categories = db.Categories.ToList();
                        ViewBag.Brand = db.Brands.ToList();
                        return View();
                    }

                    // Truy vấn và đổi tên ảnh
                    var fileName = prod.ProductID.ToString() + fileEx;
                    var pathh = Path.Combine(Server.MapPath("~/themes/images/products/"), fileName);
                    imageFile.SaveAs(pathh);

                    prod.Avatar = fileName;
                }

                // Lưu thông tin vào CSDL
                db.Products.Add(prod);
                db.SaveChanges();

                int categoryID = prod.CategoryID;
                string path = "/Admin/product/index/" + categoryID;
                return Redirect(path);
            }
            else
            {
                ViewBag.Categories = db.Categories.ToList();
                ViewBag.Brand = db.Brands.ToList();
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            DBConn db = new DBConn();
            Product pro = db.Products.Where(row => row.ProductID == id).FirstOrDefault();

            List<Category> categories = db.Categories.ToList();
            ViewBag.Category = categories;

            return View(pro);
        }
        public ActionResult Edit(int id)
        {
            DBConn db = new DBConn();
            Product pro = db.Products.Where(row => row.ProductID == id).FirstOrDefault();
            //navigation
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Brand = db.Brands.ToList();
            return View(pro);
        }
        [HttpPost]
        public ActionResult Edit(int id, Product pro)
        {
            DBConn db = new DBConn();
            // Check if the product with the specified ID exists
            Product existingProduct = db.Products.Where(row => row.ProductID == id).FirstOrDefault();
            if (existingProduct != null)
            {
                // Update the product's properties
                existingProduct.ProductName = pro.ProductName;
                existingProduct.Avatar = pro.Avatar;
                existingProduct.CategoryID = pro.CategoryID;
                existingProduct.BrandID = pro.BrandID;
                existingProduct.ShortDes = pro.ShortDes;
                existingProduct.FullDescription = pro.FullDescription;
                existingProduct.Price = pro.Price;
                existingProduct.PriceDiscount = pro.PriceDiscount;
                existingProduct.CreateAt = pro.CreateAt;
                existingProduct.UpdateAt = pro.UpdateAt;
                int categoryID = pro.CategoryID;
                // Update the product in the database
                db.SaveChanges();
                string path = "/Admin/product/index/" + categoryID;
                return Redirect(path);
                
            }
            else
            {
                
                return RedirectToAction("Edit");
            }

        }

        //xóa 1 sản phẩm
        public ActionResult Delete(int id)
        {
            DBConn db = new DBConn();
            Product pro = db.Products.Where(row => row.ProductID == id).FirstOrDefault();
            return View(pro);
        }
        [HttpPost]
        public ActionResult Delete(int id, Product p)
        {
            DBConn db = new DBConn();
            Product pro = db.Products.Where(row => row.ProductID == id).FirstOrDefault();
            db.Products.Remove(pro);
            db.SaveChanges();
            int categoryID = pro.CategoryID;
            string path = "/Admin/product/index/" + categoryID;
            return Redirect(path);
        }

    }
}