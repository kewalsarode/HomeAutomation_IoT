using System;
using System.Collections.Generic;
using System.Text;

namespace AutoHome.Mobile.Models
{
    public enum MenuItemType
    {
        Devices,
        About,
        Login,
        Logout
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
