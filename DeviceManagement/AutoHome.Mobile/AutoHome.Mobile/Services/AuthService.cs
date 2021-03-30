using AutoHome.Mobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace AutoHome.Mobile.Services
{
    public class AuthService
    {
        public AuthResponse Login(string username, string password)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = Constants.baseUrl + "api/Auth";
                    string jsonData = JsonConvert.SerializeObject(new { Username = username, Password = password });
                    var response = client.PostAsync(url, new StringContent(jsonData, Encoding.UTF8, "application/json")).Result;
                    var responseBody = response.Content.ReadAsStringAsync().Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<AuthResponse>(responseBody);
                    }
                }
            }
            catch(Exception ex)
            {
                //logs
            }
            return null;
        }
    }
}
