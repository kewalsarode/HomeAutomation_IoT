using Microsoft.Web.WebSockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoHome.Models
{
    public class DeviceModel
    {
        public int DeviceID { get; set; }
        public string DeviceName { get; set; }
        public string DeviceDescription { get; set; }
        public string DeviceType { get; set; }
        public string DeviceSerialNumber { get; set; }
        public WebSocketHandler WsSession { get; set; }

    }

    public class DevicesViewModel
    {
        List<DeviceModel> Devices { get; set; }
    }
}
