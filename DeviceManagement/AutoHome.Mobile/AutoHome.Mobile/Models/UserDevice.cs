using System;
using System.Collections.Generic;
using System.Text;

namespace AutoHome.Mobile.Models
{
    public class UserDevice
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DeviceSerialNr { get; set; }
        public string DeviceType { get; set; }
        public string Image { get; set; }
    }
}
