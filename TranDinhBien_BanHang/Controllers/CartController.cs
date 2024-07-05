using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TranDinhBien_BanHang.Models;
using TranDinhBien_BanHang.Filters;
namespace TranDinhBien_BanHang.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart

        [MyAuthenFilter]
        public ActionResult Index()
        {
            DBConn db = new DBConn();
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                List<Category> categories = db.Categories.ToList();
                ViewBag.Category = categories;
                List<Cart> carts = db.Carts.ToList();
                return View(carts);
            }
        }
        public ActionResult Add(int id = 0)
        {
            if (User.Identity.IsAuthenticated)
            {
                
                    if (id > 0)
                    {
                        DBConn db = new DBConn();
                        Cart cartItem = db.Carts.Where(row => row.ProductID == id).FirstOrDefault();
                        if (cartItem != null)
                        {
                            cartItem.Quantity += 1;
                        }
                        else
                        {
                            Cart cart = new Cart();
                            cart.ProductID = id;
                            cart.Quantity = 1;
                            db.Carts.Add(cart);
                        }
                        db.SaveChanges();
                    }
                return RedirectToAction("Index");

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }

            public ActionResult UpdateQuantity(int quan = 0, int proid = 0)
            {
                DBConn db = new DBConn();

                if (quan > 0)
                {
                    Cart cartItem = db.Carts.Where(cart => cart.ProductID == proid).FirstOrDefault();
                    if (cartItem != null)
                    {
                        cartItem.Quantity = quan;
                        db.SaveChanges();
                    }

                }
                return RedirectToAction("Index");
            }
            public ActionResult Delete(int id = 0)
            {
                DBConn db = new DBConn();
                Cart cartItem = db.Carts.Where(row => row.ProductID == id).FirstOrDefault();
                db.Carts.Remove(cartItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }