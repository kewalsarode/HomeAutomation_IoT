using AutoHome.API.Filters;
using AutoHome.BL;
using AutoHome.BL.DbServiceManager;
using AutoHome.Models;
using Microsoft.Web.WebSockets;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Web.WebSockets;

namespace AutoHome.API.Controllers
{
    public class DeviceController : ApiController
    {
        [JWTAuthenticationFilter]
        public HttpResponseMessage Get()
        {
            try
            {
                var identity = Thread.CurrentPrincipal.Identity as JWTAuthenticationIdentity;

                var deviceManager = new DeviceManager();
                var devices = deviceManager.GetDevices(identity.UserId);

                return Request.CreateResponse(HttpStatusCode.OK, devices);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [JWTAuthenticationFilter]
        public HttpResponseMessage Post(AutoHome.Models.EF.Device device)
        {
            try
            {
                var identity = Thread.CurrentPrincipal.Identity as JWTAuthenticationIdentity;

                var deviceManager = new DeviceManager();
                return Request.CreateResponse(HttpStatusCode.OK, deviceManager.CreateDevice(device, identity.UserId));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [Route("api/Device/Connect")]
        public HttpResponseMessage Connect()
        {
            if (HttpContext.Current.IsWebSocketRequest)
            {
                HttpContext.Current.AcceptWebSocketRequest(new DeviceEventHandler());
            }

            return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);
        }

        [HttpPost]
        [JWTAuthenticationFilter]
        [Route("api/Device/Control")]
        public HttpResponseMessage Control(DeviceAction deviceAction)
        {
            try
            {
                var deviceSession = DeviceSessionPool.GetDeviceSession(deviceAction.DeviceSerial);
                deviceSession.Send((new JavaScriptSerializer().Serialize(deviceAction)));
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, false);
            }
        }

        [HttpPost]
        [JWTAuthenticationFilter]
        [Route("api/Device/ResetWifi")]
        public HttpResponseMessage ResetWifi(DeviceAction deviceAction)
        {
            try
            {
                deviceAction.Action = "RESET";
                var deviceSession = DeviceSessionPool.GetDeviceSession(deviceAction.DeviceSerial);
                deviceSession.Send((new JavaScriptSerializer().Serialize(deviceAction)));
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, false);
            }
        }

        [HttpGet]
        [Route("api/Device/IsOnline/{deviceSerial}")]
        public HttpResponseMessage IsOnline(string deviceSerial)
        {
            try
            {
                if(DeviceSessionPool.GetDeviceSession(deviceSerial)==null)
                    return Request.CreateResponse(HttpStatusCode.OK, false);
                else
                    return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("api/Device/CheckStatus/{deviceSerial}")]
        public HttpResponseMessage CheckStatus(string deviceSerial)
        {
            try
            {
                var deviceAction = new DeviceAction
                {
                    Action = "STATUS",
                    DeviceSerial = deviceSerial
                };

                var deviceSession = DeviceSessionPool.GetDeviceSession(deviceSerial);
                deviceSession.Send((new JavaScriptSerializer().Serialize(deviceAction)));
                return Request.CreateResponse(HttpStatusCode.OK, DeviceSessionPool.deviceStatus[deviceSerial]);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }

    public class DeviceEventHandler : WebSocketHandler
    {
        public DeviceEventHandler()
        {

        }

        public override void OnOpen()
        {
            MessageManager manager = new MessageManager();
            base.OnOpen();
            try
            {
                string deviceSerial = this.WebSocketContext.Headers.GetValues("device-serial").FirstOrDefault();
                WebSocketHandler objDeviceSession = DeviceSessionPool.GetDeviceSession(deviceSerial);
                manager.PushMessage("OnOpen_1", deviceSerial);
                if (objDeviceSession == null)
                {
                    DeviceManager objDeviceManager = new DeviceManager();
                    if(objDeviceManager.IsDeviceExists(deviceSerial))
                    {
                        DeviceSessionPool.AddDeviceSession(deviceSerial, this);
                    }
                }
                else
                {
                    DeviceSessionPool.UpdateDeviceSession(deviceSerial, this);
                }
            }
            catch(Exception ex)
            {
                manager.PushMessage("OnOpen_exception", ex.Message);
                base.Send("You are not connected. " + ex.Message);
            }
        }

        public override void OnMessage(string message)
        {
            MessageManager manager = new MessageManager();
            manager.PushMessage("received_message0", message);
            try
            {
                string deviceSerial = this.WebSocketContext.Headers.GetValues("device-serial").FirstOrDefault();
                var deviceStatus = JsonConvert.DeserializeObject<DeviceStatus>(message);
                manager.PushMessage("received_message1", deviceSerial + " and Message: " + message);

                if (!DeviceSessionPool.deviceStatus.ContainsKey(deviceSerial))
                    DeviceSessionPool.deviceStatus.Add(deviceSerial, deviceStatus);
                else
                    DeviceSessionPool.deviceStatus[deviceSerial] = deviceStatus;
            }
            catch(Exception ex)
            {
                manager.PushMessage("received_message1", ex.Message);
            }
        }

        public override void OnClose()
        {
            MessageManager manager = new MessageManager();
            manager.PushMessage("Closed", DateTime.UtcNow.ToString());
            string deviceSerial = this.WebSocketContext.Headers.GetValues("device-serial").FirstOrDefault();
            DeviceSessionPool.RemoveDeviceSession(deviceSerial);
            // Free resources, close connections, etc.
            base.OnClose();
        }
    }
}
