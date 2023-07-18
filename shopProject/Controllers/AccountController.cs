using ProjectShopCMS.DAL;
using ProjectShopCMS.DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace shopProject.Controllers
{
    public class AccountController : Controller
    {
        projectShopDBEntities db = new projectShopDBEntities();
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel register)
        {
            if(ModelState.IsValid)
            {
                if(!db.User.Any(u => u.Email == register.Email.Trim().ToLower()))
                {
                    User user = new User()
                    {
                        UserName = register.UserName,
                        Email = register.Email.Trim().ToLower(),
                        Password = FormsAuthentication.HashPasswordForStoringInConfigFile(register.Password, "MD5"),
                        ActivationCode = Guid.NewGuid().ToString(),
                        IsActive = false,
                        CreateDate = DateTime.Now,
                        RoleId = 1
                    };
                    db.User.Add(user);
                    db.SaveChanges();
                    return View("SuccsessMessageForRegister",user);
                }
                else
                {
                    ModelState.AddModelError("Email", "ایمیل وارد شده تکرار می باشد");
                }
            }
            return View(register);
        }
    }
}