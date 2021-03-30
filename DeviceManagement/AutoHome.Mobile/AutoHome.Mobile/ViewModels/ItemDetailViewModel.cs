using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoHome.Mobile.Models;
using Xamarin.Forms;

namespace AutoHome.Mobile.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public UserDevice UserDevice { get; set; }
        public Command<bool> SwitchCommand { get; set; }
        public Command ResetCommand { get; set; }

        private bool isToggled;
        public bool IsToggled
        {
            get { return isToggled; }
            set
            {
                isToggled = value;
                OnPropertyChanged();
            }
        }

        private Color statusColor;
        public Color StatusColor
        {
            get { return statusColor; }
            set
            {
                statusColor = value;
                OnPropertyChanged();
            }
        }
        private string onlineStatus;
        public string OnlineStatus
        {
            get { return onlineStatus; }
            set
            {
                onlineStatus = value;
                OnPropertyChanged();
            }
        }

        private string switchStatus;
        public string SwitchStatus
        {
            get { return switchStatus; }
            set
            {
                switchStatus = value;
                OnPropertyChanged();
            }
        }

        public ItemDetailViewModel(UserDevice device = null)
        {
            Title = device?.Name;
            IsToggled = false;
            UserDevice = device;
            StatusColor = Color.Gray;
            OnlineStatus = "Offline";
            SwitchStatus =  "OFF";
            SwitchCommand = new Command<bool>((switchValue) => ExecuteSwitchCommand(switchValue));
            ResetCommand = new Command(() => ExecuteResetCommand());
            Task.Run(() => RunThread());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ExecuteResetCommand()
        {
            var deviceAction = new DeviceAction()
            {
                Action = "RESET",
                DeviceSerial = UserDevice.DeviceSerialNr,
                DeviceType = UserDevice.DeviceType
            };

            ((Services.DeviceDataStore)DataStore).ResetWifi(deviceAction);
        }

        private void ExecuteSwitchCommand(bool switchValue)
        {
            var deviceAction = new DeviceAction()
            {
                Action = switchValue ? "ON":"OFF",
                DeviceSerial = UserDevice.DeviceSerialNr,
                DeviceType = UserDevice.DeviceType
            };

            ((Services.DeviceDataStore)DataStore).SwitchOnOff(deviceAction);
        }

        private async Task RunThread()
        {
            while(true)
            {
                8var deviceStatus = ((Services.DeviceDataStore)DataStore).CheckStatus(UserDevice.DeviceSerialNr);
                await Task.Delay(5000);
                bool status = ((Services.DeviceDataStore)DataStore).IsOnline(UserDevice.DeviceSerialNr);
                StatusColor = status ? Color.Green : Color.Gray;
                OnlineStatus = status ? "Online" : "Offline";
                IsToggled = GetDeviceStatus(UserDevice.DeviceType, deviceStatus);
                SwitchStatus = IsToggled ? "ON" : "OFF";
            }
        }

        private bool GetDeviceStatus(string deviceType, DeviceStatus deviceStatus)
        {
            switch(deviceType.ToLowerInvariant())
            {
                case "switch": return Convert.ToBoolean(deviceStatus.Switch);
                case "fan" : return Convert.ToBoolean(deviceStatus.Fan);
                default : return false;
            }
        }
    }
}
