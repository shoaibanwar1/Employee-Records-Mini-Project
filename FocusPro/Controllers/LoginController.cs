using FocusPro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FocusPro.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult LoginIndex()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("LoginIndex");
        }
        [HttpPost]
        public JsonResult LoginIndex(int employeeId, string employeeName)
        {
            DalEmployee dalEmployee = new DalEmployee();
            bool isAuthenticated = dalEmployee.Login(employeeId, employeeName);

            if (isAuthenticated)
            {
                // Authentication successful
                Session["EmployeeId"] = employeeId;
                Session["EmployeeName"] = employeeName;
                return Json(new { success = true, message = "Login successful" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                // Authentication failed
                return Json(new { success = false, message = "Invalid credentials. Please try again." }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}