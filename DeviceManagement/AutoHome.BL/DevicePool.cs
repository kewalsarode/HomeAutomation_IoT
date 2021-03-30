using AutoHome.Models;
using Microsoft.Web.WebSockets;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoHome.BL
{
    public static class DeviceSessionPool
    {
        static Dictionary<string, WebSocketHandler> deviceSessions = new Dictionary<string, WebSocketHandler>();
        public static Dictionary<string, DeviceStatus> deviceStatus = new Dictionary<string, DeviceStatus>();

        public static void AddDeviceSession(string serialNumber, WebSocketHandler wsContext)
        {
            deviceSessions.Add(serialNumber, wsContext);
        }

        public static WebSocketHandler GetDeviceSession(string deviceSerial)
        {
            if(deviceSessions.ContainsKey(deviceSerial))
            {
                return deviceSessions[deviceSerial];
            }
            return null;
        }

        public static void UpdateDeviceSession(string deviceSerial, WebSocketHandler wsContext)
        {
            deviceSessions[deviceSerial] = wsContext;
        }

        public static void RemoveDeviceSession(string deviceSerial)
        {
            if(deviceSessions.ContainsKey(deviceSerial))
            {
                deviceSessions.Remove(deviceSerial);
            }
        }
    }
}
