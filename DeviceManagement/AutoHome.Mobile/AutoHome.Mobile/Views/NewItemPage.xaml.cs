using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AutoHome.Mobile.Models;

namespace AutoHome.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public UserDevice UserDevice { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            UserDevice = new UserDevice
            {
                Name = "Device name",
                Description = "This is an item description.",
                DeviceSerialNr = "Serial Number",
                DeviceType = "Switch"
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            if(UserDevice.Name.Equals("Device name") || UserDevice.DeviceSerialNr.Equals("Serial Number"))
            {
                await DisplayAlert("Device Validation", "Please enter correct details.", "OK");
                return;
            }
            MessagingCenter.Send(this, "AddItem", UserDevice);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                UserDevice.DeviceType = picker.Items[selectedIndex];
            }
        }
    }
}