using System;
using System.Collections.Generic;
using System.Text;

namespace AutoHome.Mobile.Models
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}
