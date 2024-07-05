using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TranDinhBien_BanHang.Models;
using TranDinhBien_BanHang.Filters;

namespace TranDinhBien_BanHang.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(int id, string search="",string Column = "ProductName", string Icon = "fa-sort-asc", int page = 1)
        {

            DBConn db = new DBConn();
            //Lấy tên danh mục hiện tại
            var categoryName = db.Categories.Find(id).CategoryName;
           // Lưu tên danh mục vào viewbag
            ViewBag.Name = categoryName;
           // hiển thị danh mục theo sản phẩm av tiềm kiếm
            var lstProduct = db.Products.Where(r => r.CategoryID == id && r.ProductName.Contains(search)).ToList();
            ViewBag.CategoryID = id;
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

            //sắp xếp sản phẩm theo các thuộc tính khác nhau
            ViewBag.Column = Column;
            ViewBag.Icon = Icon;

            if (Column == "ProductName")
            {
                if (Icon == "fa-sort-asc")
                {
                    lstProduct = lstProduct.OrderBy(row => row.ProductName).ToList();
                }
                else
                {
                    lstProduct = lstProduct.OrderByDescending(row => row.ProductName).ToList();
                }
            }
            else if (Column == "Price")
            {
                if (Icon == "fa-sort-asc")
                {
                    lstProduct = lstProduct.OrderBy(row => row.Price).ToList();
                }
                else
                {
                    lstProduct = lstProduct.OrderByDescending(row => row.Price).ToList();
                }
            }
            return View(lstProduct);
        }

       
        public ActionResult Details(int id)
        {
            DBConn db = new DBConn();
            Product pro = db.Products.Where(row => row.ProductID == id).FirstOrDefault();
            List<Category> categories = db.Categories.ToList();
            ViewBag.Category = categories;

            // Thêm dữ liệu categoryname vào view
            ViewData["CategoryName"] = categories.FirstOrDefault().CategoryName;
            return View(pro);
        }


        //// tạo sản phẩm mới
        //public ActionResult Create()
        //{
        //    DBConn db = new DBConn();
        //    //navigation
        //    ViewBag.Categories = db.Categories.ToList();
        //    ViewBag.Brand = db.Brands.ToList();
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Create(Product pro)
        //{
        //    pro.Type = 0;
        //    DBConn db = new DBConn();
        //    if (ModelState.IsValid)
        //    {
        //        db.Products.Add(pro); // thêm vào

        //        db.SaveChanges();// lưu lại
        //        int categoryID = pro.CategoryID;
        //        string path = "/Admin/Category/ProductCategory/" + categoryID;
        //        return Redirect(path);
        //    }
        //    else
        //    {
        //        ViewBag.Categories = db.Categories.ToList();
        //        ViewBag.Brand = db.Brands.ToList();
        //        return RedirectToAction("Create");
        //    }

        //}

        //// chỉnh sửa lại sản phẩm

        //public ActionResult Edit(int id)
        //{
        //    DBConn db = new DBConn();
        //    Product pro = db.Products.Where(row => row.ProductID == id).FirstOrDefault();
        //    //navigation
        //    ViewBag.Categories = db.Categories.ToList();
        //    ViewBag.Brand = db.Brands.ToList();
        //    return View(pro);
        //}
        //[HttpPost]
        //public ActionResult Edit(int id, Product pro)
        //{
        //    DBConn db = new DBConn();
        //    // Check if the product with the specified ID exists
        //    Product existingProduct = db.Products.Where(row => row.ProductID == id).FirstOrDefault();
        //    if (existingProduct != null)
        //    {
        //        // Update the product's properties
        //        existingProduct.ProductName = pro.ProductName;
        //        existingProduct.Avatar = pro.Avatar;
        //        existingProduct.CategoryID = pro.CategoryID;
        //        existingProduct.BrandID = pro.BrandID;
        //        existingProduct.ShortDes = pro.ShortDes;
        //        existingProduct.FullDescription = pro.FullDescription;
        //        existingProduct.Price = pro.Price;
        //        existingProduct.PriceDiscount = pro.PriceDiscount;
        //        existingProduct.CreateAt = pro.CreateAt;
        //        existingProduct.UpdateAt = pro.UpdateAt;
        //        int categoryID = pro.CategoryID;
        //        // Update the product in the database
        //        db.SaveChanges();
        //        string path = "/Admin/Category/ProductCategory/" + categoryID;
        //        return Redirect(path);
        //        //return RedirectToAction("ProductCategory", "Category");
        //    }
        //    else
        //    {
        //        // Handle the case when the product is not found
        //        return RedirectToAction("Edit");
        //    }

        //}



        //    //xóa 1 sản phẩm
        //    public ActionResult Delete(int id)
        //    {
        //        DBConn db = new DBConn();
        //        Product pro = db.Products.Where(row => row.ProductID == id).FirstOrDefault();
        //        return View(pro);
        //    }
        //    [HttpPost]
        //    public ActionResult Delete(int id, Product p)
        //    {
        //        DBConn db = new DBConn();
        //        Product pro = db.Products.Where(row => row.ProductID == id).FirstOrDefault();
        //        db.Products.Remove(pro);
        //        db.SaveChanges();
        //        int categoryID = pro.CategoryID;
        //        string path = "/Admin/Category/ProductCategory/" + categoryID;
        //        return Redirect(path);
        //    }
    }
}