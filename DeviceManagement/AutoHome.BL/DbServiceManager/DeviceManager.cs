using AutoHome.Models;
using System;
using System.Collections.Generic;
using System.Text;
using AutoHome.Models.EF;
using AutoHome.Models.Repository;
using System.Linq;

namespace AutoHome.BL.DbServiceManager
{
    public class DeviceManager
    {
        IRepository<Device> objDeviceRepo = new Repository<Device>(new DeviceManagementEntities());

        public DeviceModel GetDevice(string serialNumber)
        {
            var device = objDeviceRepo.SearchFor(d => d.DeviceSerialNr.Equals(serialNumber)).FirstOrDefault();

            if (device == null)
                return null;

            return new DeviceModel()
            {
                DeviceID = device.ID,
                DeviceDescription = device.Description,
                DeviceName = device.Name,
                DeviceSerialNumber = device.DeviceSerialNr,
                DeviceType = device.DeviceType
            };
        }

        public bool IsDeviceExists(string serialNumber)
        {
            var device = objDeviceRepo.SearchFor(d => d.DeviceSerialNr.Equals(serialNumber)).FirstOrDefault();

            if (device == null)
                return false;
            else
                return true;
        }

        public IEnumerable<Device> GetDevices(int userId)
        {
            return objDeviceRepo.SearchFor(d => d.UserAccount_ID == userId).ToList();
        }

        public int CreateDevice(Device deviceModel, int userId)
        {
            var device = new Device
            {
                Name = deviceModel.Name,
                Description = deviceModel.Description,
                DeviceSerialNr = deviceModel.DeviceSerialNr,
                DeviceType = deviceModel.DeviceType,
                UserAccount_ID = userId
            };

            var newDevice = objDeviceRepo.Insert(device);

            return newDevice.ID;
        }
    }
}
