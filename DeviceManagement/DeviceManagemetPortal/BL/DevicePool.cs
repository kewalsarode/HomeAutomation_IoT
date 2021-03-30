using DeviceManagemetPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeviceManagemetPortal.BL
{
    public static class DevicePool
    {
        static List<DeviceModel> devices = new List<DeviceModel>();

        internal static void AddDevice(DeviceModel objDevice)
        {
            devices.Add(objDevice);
        }

        internal static DeviceModel GetDevice(string deviceSerial)
        {
            return devices.Where(d => d.DeviceSerialNumber.Equals(deviceSerial)).FirstOrDefault();
        }

        internal static void UpdateDeviceSession(string deviceSerial, string id)
        {
            foreach(var device in devices)
            {
                if(device.DeviceSerialNumber.Equals(deviceSerial))
                {
                    device.SessionId = id;
                }
            }
        }
    }
}