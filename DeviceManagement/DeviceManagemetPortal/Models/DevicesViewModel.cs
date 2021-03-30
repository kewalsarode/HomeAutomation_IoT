using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeviceManagemetPortal.Models
{
    public class DeviceModel
    {
        public int DeviceID { get; set; }
        public string DeviceName { get; set; }
        public string DeviceDescription { get; set; }
        public string DeviceType { get; set; }
        public string DeviceSerialNumber { get; set; }
        public string SessionId { get; set; }

    }

    public class DevicesViewModel
    {
        List<DeviceModel> Devices { get; set; }
    }
}