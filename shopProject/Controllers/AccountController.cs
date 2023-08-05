using MyEshop;
using ProjectShopCMS;
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
        [Route("Register")]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [Route("Register")]
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
                    #region ActiveEmail
                    string body = PartialToString.RenderPartialView("ManagementEmail", "ActivationEmail", user);
                    SendEmail.Send(user.Email, "ایمیل فعالسازی", body);
                    #endregion
                    return View("SuccsessMessageForRegister",user);
                }
                else
                {
                    ModelState.AddModelError("Email", "ایمیل وارد شده تکرار می باشد");
                }
            }
            return View(register);
        }
        public ActionResult ActiveUser(string id)
        {
            var user = db.User.SingleOrDefault(u => u.ActivationCode == id);
            if(user == null)
            {
                return HttpNotFound();
            }
            else
            {
                user.IsActive = true;
                user.ActivationCode = Guid.NewGuid().ToString();
                db.SaveChanges();
                ViewBag.UserName = user.UserName;
            }
            return View();
        }

        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }


        [Route("Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVeiwModel login,string returnUrl = "/")
        {
            if (ModelState.IsValid)
            {
                string hashPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(login.Password, "MD5").ToLower();
                var user = db.User.SingleOrDefault(u => u.Email.Trim().ToLower() == login.Email.Trim().ToLower() && u.Password == hashPassword);
                if (user != null)
                {
                    if(user.IsActive == true)
                    {
                        FormsAuthentication.SetAuthCookie(user.UserName, login.RememberMe);
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "حساب کاربری شما فعال نمی باشد");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "کاربری با مشخصات مورد نظر یافت نشد");
                }
            }

            return View(login);
        }

        [Route("ForgetPassword")]
        public ActionResult ForgetPassword()
        {
            return View();
        }

        [Route("ForgetPassword")]
        [HttpPost]
        public ActionResult ForgetPassword(ForgetPasswordViewModel forgetVM)
        {
            if(ModelState.IsValid)
            {
                var user = db.User.SingleOrDefault(u => u.Email == forgetVM.Email);
                if(user == null)
                {
                    ModelState.AddModelError("Email", "کاربری با مشخصات وارد شده یافت نشد");
                }
                else
                {
                    if (user.IsActive == true)
                    {
                        #region ForgetPassword
                        string body = PartialToString.RenderPartialView("ManagementEmail", "RecoveryPasswordEmail", user);
                        SendEmail.Send(user.Email, "ایمیل بازیابی رمز عبور", body);
                        #endregion
                        return View("successMessageForRecoveryPassword",user);
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "حساب کاربری شما فعال نمی باشد");
                    }
                }
            }
            return View();
        }

        public ActionResult RecoveryPassword(string id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult RecoveryPassword(string id,RecoveryPasswordViewModel recoveryVM)
        {
            var user = db.User.SingleOrDefault(u => u.ActivationCode == id);
            if(user == null)
            {
                return HttpNotFound();
            }
            else
            {
                user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(user.Password, "MD5");
                user.ActivationCode = Guid.NewGuid().ToString();
                db.SaveChanges();
                return RedirectToAction("Login", new { Recovery = true }) ;
            }
            return View();
        }

        [Route("Logout")]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
    }
}