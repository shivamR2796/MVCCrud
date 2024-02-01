using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;
using System.Web.Security;
using MVCCrud.DBConn;
using MVCCrud.Models;

namespace MVCCrud.Controllers
{
    public class LoginController : Controller
    {
        SHIVAMDBEntities1 obj = new SHIVAMDBEntities1();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel l)
        {
            var emp_Login = obj.Logins.Where(a=>a.Email.ToLower() == l.Email.ToLower()).FirstOrDefault();
            if (emp_Login == null) 
            {
                TempData["InvalidEmail"] = "Please Enter Valid Email";
            }
            else
            {
                if(emp_Login.Email.ToLower() == l.Email.ToLower() && emp_Login.Password == l.Password)
                {
                    FormsAuthentication.SetAuthCookie(emp_Login.Email, false);
                    Session["i"] = emp_Login.Email;
                    Session["j"] = emp_Login.Name;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["InvalidPassword"] = "Please Enter Correct Password";
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}