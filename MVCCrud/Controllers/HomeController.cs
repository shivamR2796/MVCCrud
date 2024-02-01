using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCCrud.DBConn;
using MVCCrud.Models;

namespace MVCCrud.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        SHIVAMDBEntities1 obj = new SHIVAMDBEntities1();
        List<EmpModel> emps = new List<EmpModel>();
        public ActionResult Index()
        {
            var res = obj.EMPs.ToList();
            foreach (var item in res)
            {
                emps.Add(new EmpModel()
                {
                    EMP_ID=item.EMP_ID,
                    EMP_NAME=item.EMP_NAME,
                    AGE=item.AGE,
                    CITY=item.CITY,
                });
            }
            return View(emps);
        }
        public ActionResult Delete(int EMP_ID)
        {
            var deletedata = obj.EMPs.Where(m=>m.EMP_ID== EMP_ID).First();
            obj.EMPs.Remove(deletedata);
            obj.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult AddDetails(EmpModel res)
        {
            if (res.EMP_ID == 0)
            {
                ViewBag.msg = "Add";
            }
            else
            {
                ViewBag.msg = "Update";
            }
            ModelState.Clear();
            return View(res);
        }
        [HttpPost]
        public ActionResult AddDetails(EMP r)
        {
            if (r.EMP_ID == 0)
            {
                obj.EMPs.Add(r);
                obj.SaveChanges();
            }
            else
            {
                obj.Entry(r).State = System.Data.Entity.EntityState.Modified;
                obj.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int EMP_ID)
        {
            var editdata = obj.EMPs.Where(a=>a.EMP_ID == EMP_ID).First();
            EmpModel res = new EmpModel();
            res.EMP_ID = editdata.EMP_ID;
            res.EMP_NAME = editdata.EMP_NAME;
            res.AGE = editdata.AGE;
            res.CITY = editdata.CITY;
            return RedirectToAction("AddDetails", res);
        }

        [AllowAnonymous]
        public ActionResult signup()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult signup(LoginModel res) 
        {
            Login j = new Login();
            j.Name = res.Name;
            j.Email = res.Email;
            j.Password = res.Password;
            obj.Logins.Add(j);
            obj.SaveChanges();
            return RedirectToAction("Login", "Login");
        }




    }
}