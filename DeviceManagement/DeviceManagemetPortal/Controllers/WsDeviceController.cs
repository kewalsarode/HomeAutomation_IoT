using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.WebSockets;

namespace DeviceManagemetPortal.Controllers
{
    public class WsDeviceController : ApiController
    {
        public HttpResponseMessage Get()
        {
            if (HttpContext.Current.IsWebSocketRequest)
            {
                HttpContext.Current.AcceptWebSocketRequest(ProcessWSChat);
            }

            return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);
        }

        private Task ProcessWSChat(AspNetWebSocketContext arg)
        {
            throw new NotImplementedException();
        }
    }
}
