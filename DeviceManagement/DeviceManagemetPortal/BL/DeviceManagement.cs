using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeviceManagemetPortal.Models;

namespace DeviceManagemetPortal.BL
{
    public class DeviceManagement
    {
        private int userId;

        public DeviceManagement(int userId)
        {
            this.userId = userId;
        }

        public List<DeviceModel> GetUserDevices()
        {
            List<DeviceModel> deviceModels = null;
            using (var ctx = new DeviceMgmtEntities())
            {
                deviceModels = ctx.Devices.Where(ua => ua.UserAccount_ID == userId).Select(dvc => new DeviceModel() { DeviceID = dvc.ID, DeviceDescription = dvc.Description, DeviceName = dvc.Name, DeviceSerialNumber = dvc.DeviceSerialNr, DeviceType = dvc.DeviceType }).ToList<DeviceModel>();

            }
            return deviceModels;
        }

        public bool CreateDevice(DeviceModel deviceModel)
        {
            using (var ctx = new DeviceMgmtEntities())
            {
                var device = new Device()
                {
                    UserAccount_ID = userId,
                    Description = deviceModel.DeviceDescription,
                    DeviceSerialNr = deviceModel.DeviceSerialNumber,
                    DeviceType = deviceModel.DeviceType,
                    Name = deviceModel.DeviceName
                };
                ctx.Devices.Add(device);
                if (0 < ctx.SaveChanges())
                    return true;
                return false;
            }
        }
    }
}