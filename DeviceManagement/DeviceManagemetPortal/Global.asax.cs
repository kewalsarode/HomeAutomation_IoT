using DeviceManagemetPortal.BL;
using DeviceManagemetPortal.Models;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace DeviceManagemetPortal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static WebSocketServer objWsServer;
        public static WebSocketSharp.Server.HttpServer objHttpServer;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configure(WebApiConfig.Register);

            //objWsServer = new WebSocketServer("ws://localhost/");
            //objWsServer.AddWebSocketService<DeviceSocket>("/");
            //objWsServer.Start();

            //objHttpServer = new WebSocketSharp.Server.HttpServer(80);
            //objHttpServer.AddWebSocketService<DeviceSocket>("/");
            //objHttpServer.Start();
        }

    }

    public class DeviceSocket : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            var msg = e.Data == "Hi"? "This is not chat...": "Echo: " + e.Data;
            Send(msg);
        }

        protected override void OnOpen()
        {
            try
            {
                string deviceSerial = this.Context.Headers.GetValues("device-serial").FirstOrDefault();

                DeviceModel objDevice = DevicePool.GetDevice(deviceSerial);
                if (objDevice == null)
                {
                    using (var ctx = new DeviceMgmtEntities())
                    {
                        objDevice = ctx.Devices.Where(d => d.DeviceSerialNr.Equals(deviceSerial)).Select(dvc => new DeviceModel() { DeviceID = dvc.ID, DeviceDescription = dvc.Description, DeviceName = dvc.Name, DeviceSerialNumber = dvc.DeviceSerialNr, DeviceType = dvc.DeviceType, SessionId = this.ID }).FirstOrDefault();
                    }
                    DevicePool.AddDevice(objDevice);
                }
                else
                {
                    DevicePool.UpdateDeviceSession(objDevice.DeviceSerialNumber, this.ID);
                }
            }
            catch
            {
                //Logs goes here...
            }
        }
    }
}
