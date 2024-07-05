using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TranDinhBien_BanHang.Models;
using TranDinhBien_BanHang.Identity;
using TranDinhBien_BanHang.ViewModel;
using System.Web.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using TranDinhBien_BanHang.Filters;

namespace TranDinhBien_BanHang.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();

        }
        [HttpPost]

        public ActionResult Login(LoginVM lvm)
        {

            var appDbContext = new AppDbContext();
            var userStore = new AppUserStore(appDbContext);
            var userManager = new AppUserManager(userStore);
            var user = userManager.Find(lvm.Username, lvm.Password);
            if (user != null)
            {
                var authenManager = HttpContext.GetOwinContext().Authentication;
                var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authenManager.SignIn(new AuthenticationProperties(), userIdentity);

                if(userManager.IsInRole(user.Id,"Admin"))
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                else
                {

                return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("Thông báo lỗi", "Không tồn tại tên người dùng và mật khẩu này");
                return View();
            }
        }
        public ActionResult Logout()
        {
            var authenManager = HttpContext.GetOwinContext().Authentication;
            authenManager.SignOut();
            return RedirectToAction("Index", "Home", null);
        }

        public ActionResult MyProfile(MyProfileVM mvm)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login");
            }

            // Lấy thông tin của người dùng đã đăng nhập

            var appDbContext = new AppDbContext();
            var userStore = new AppUserStore(appDbContext);
            var userManager = new AppUserManager(userStore);
            var user = new AppUser()
            {
                UserName = mvm.Username,
                Email = mvm.Email,
                City = mvm.City,
                Birthday = mvm.DateOfBirth,
                Address = mvm.Address,
                PhoneNumber = mvm.Mobile

            };
            return View(user);
        }

        public ActionResult Create()
        {
            return View();
        }
       
    }
    


}