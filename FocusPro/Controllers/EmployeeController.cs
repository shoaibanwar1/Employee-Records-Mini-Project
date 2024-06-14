using FocusPro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FocusPro.Controllers
{
    public class EmployeeController : Controller
    {
        DalEmployee dalEmployee = new DalEmployee();
        // GET: Employee
        public ActionResult EmployeeIndex()
        {
            GetStates();
            GetEmployeeDetails();
            return View();
        }
        [HttpGet]
        public JsonResult GetCitiesWithStateName(string stateName)
        {
            List<CitiesDO> liCitiesDO = dalEmployee.GetCitiesWithStateName(stateName);
            TempData["liCitiesDO"] = liCitiesDO;
            return Json(liCitiesDO, JsonRequestBehavior.AllowGet);
        }
        //[HttpPost] 
        //public JsonResult InsertOrUpdateEmployeeDetails(EmployeeDO employeeDO,string type1)
        //{
        //    GetStates();
        //    string result= dalEmployee.InsertOrUpdateEmployee(employeeDO, type1);
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
        [HttpPost]
        public JsonResult InsertOrUpdateEmployeeDetails(EmployeeDO employeeDO, string type1)
        {
            GetStates();
            string result = "";
            if (type1 == "insert")
            {
                if (dalEmployee.ExistsEmployee(employeeDO.EmployeeName))
                {
                    return Json("Name already exists", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    result = dalEmployee.InsertOrUpdateEmployee(employeeDO, type1);

                }
            }
            else if (type1 == "update")
            {
                result = dalEmployee.InsertOrUpdateEmployee(employeeDO, type1);
            }
            else
            {
                result = dalEmployee.InsertOrUpdateEmployee(employeeDO, type1);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SearchEmployeeWithId(EmployeeDO employeeDO)
        {
            GetStates();
            GetEmployeeDetails();
            employeeDO = dalEmployee.GetEmployeeDetailsWithId(employeeDO.EmployeeId);
            return Json(new { employeeDO, JsonRequestBehavior.AllowGet });
        }
        [HttpGet]
        public ActionResult DeleteEmployeeWithId(int employeeId)
        {
            dalEmployee.DeleteEmployeeWithId(employeeId);
            return RedirectToAction("EmployeeIndex");
        }
        public void GetEmployeeDetails()
        {
            List<EmployeeDO> liemployeeDOs = dalEmployee.GetEmployeeDetails();
            TempData["liemployeeDOs"] = liemployeeDOs;
            TempData.Keep();
        }
        public void GetStates()
        {
            List<StatesDO> liStatesDOs = dalEmployee.GetStates();
            TempData["liStatesDOs"] = liStatesDOs;
            TempData.Keep();
        }
    }
}