using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeviceManagemetPortal.Models;
using DeviceManagemetPortal.BL;
using System.Web.Script.Serialization;

namespace DeviceManagemetPortal.Controllers
{
    public class DeviceController : Controller
    {
        // GET: Device
        public ActionResult Index()
        {

            return View();
        }

        // GET: Device/Details/5
        public JsonResult DeviceDetails()
        {

            int id = ((UserSessionModel)this.HttpContext.Session["user"]).Id;
            List<DeviceModel> deviceModels = new DeviceManagement(id).GetUserDevices();
            return Json(deviceModels, JsonRequestBehavior.AllowGet);
        }

        // GET: Device/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Device/Create
        [HttpPost]
        public JsonResult Create(DeviceModel model)
        {
            try
            {
                int id = ((UserSessionModel)this.HttpContext.Session["user"]).Id;
                DeviceManagement objDevceManager = new DeviceManagement(id);
                objDevceManager.CreateDevice(model);
                return Json(true);
            }
            catch
            {
                return Json(false);
            }
        }

        // GET: Device/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Device/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Device/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Device/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public JsonResult Control(DeviceActionModel model)
        {
            try
            {
                var device = DevicePool.GetDevice(model.DeviceSerial);
                MvcApplication.objWsServer.WebSocketServices["/"].Sessions.SendTo(new JavaScriptSerializer().Serialize(model), device.SessionId);
                return Json(true);
            }
            catch
            {
                return Json(false);
            }
        }

        [HttpPost]
        public JsonResult ResetWifi(DeviceActionModel model)
        {
            try
            {
                model.Action = "RESET";
                var device = DevicePool.GetDevice(model.DeviceSerial);
                MvcApplication.objWsServer.WebSocketServices["/"].Sessions.SendTo(new JavaScriptSerializer().Serialize(model), device.SessionId);
                return Json(true);
            }
            catch
            {
                return Json(false);
            }
        }
    }
}
