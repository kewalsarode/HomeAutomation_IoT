using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeviceManagemetPortal.Models
{
    public class DeviceActionModel
    {
        public string DeviceSerial { get; set; }
        public string DeviceType { get; set; }
        public string Action { get; set; }
    }
}