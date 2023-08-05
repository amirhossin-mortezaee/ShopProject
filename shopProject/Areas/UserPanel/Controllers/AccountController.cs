using ProjectShopCMS.DAL;
using ProjectShopCMS.DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace shopProject.Areas.UserPanel.Controllers
{
    public class AccountController : Controller
    {
        projectShopDBEntities db = new projectShopDBEntities();
        // GET: User/changePasswordUser
        public ActionResult ChangePasswordUser()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePasswordUser(ChangePasswordUser changeVM)
        {
            if (ModelState.IsValid)
            {
                var user = db.User.Single(u => u.UserName == User.Identity.Name);
                string HashOldPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(changeVM.OldPassword, "MD5");
                if (user.Password == HashOldPassword)
                {
                    user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(changeVM.Password, "MD5");
                    user.ActivationCode = Guid.NewGuid().ToString();
                    db.SaveChanges();
                    ViewBag.successmassageChangePassword = true;
                }
                else
                {
                    ModelState.AddModelError("OldPassword", "کلمه عبور فعلی درست نیست");
                }
            }
            return View();
        }
    }
}