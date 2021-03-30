using DeviceManagemetPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeviceManagemetPortal.BL;

namespace DeviceManagemetPortal.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        public ActionResult LogIn()
        {
            //ViewData["ErrorMessage"] = ViewData["ErrorMessage"] ?? "";
            return View();
        }
        
        [HttpPost]
        public ActionResult LogIn(LoginViewModel model)
        {
            try
            {
                UserManagement userMgmt = new UserManagement();
                UserSessionModel userDetails = userMgmt.ValidateCredential(model);
                if (!string.IsNullOrEmpty(userDetails.UserName.ToString()) && !string.IsNullOrEmpty(userDetails.Id.ToString()))
                {
                    Session["user"] = userDetails;
                    return RedirectToAction("Index", "Device");
                }
            }
            catch(Exception e) {

                string err = e.Message;
            }
            
            //ViewData["ErrorMessage"] = "Enter valid username and password.";
            ModelState.AddModelError("", "Enter valid username and password.");
            return View(model);

        }
    }
}