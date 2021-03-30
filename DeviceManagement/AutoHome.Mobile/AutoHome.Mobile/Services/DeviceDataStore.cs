using AutoHome.Mobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AutoHome.Mobile.Services
{
    public class DeviceDataStore : IDataStore<UserDevice>
    {
        IEnumerable<UserDevice> devices = null;
        public DeviceDataStore()
        {
            devices = new List<UserDevice>();

        }
        public async Task<bool> AddItemAsync(UserDevice item)
        {
            try
            {
                Models.AuthResponse token = JsonConvert.DeserializeObject<Models.AuthResponse>(Preferences.Get("Token", ""));

                using (HttpClient client = new HttpClient())
                {
                    string url = Constants.baseUrl + "api/Device";
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", token.Token);
                    client.DefaultRequestHeaders.Add("AcceptEncoding", "application/json");

                    var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json"));
                    var responseBody = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                }
            }
            catch
            {
                //Logs
            }
            return false;
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<UserDevice> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDevice>> GetItemsAsync(bool forceRefresh = false)
        {
            try
            {
                Models.AuthResponse token = JsonConvert.DeserializeObject<Models.AuthResponse>(Preferences.Get("Token", ""));

                using (HttpClient client = new HttpClient())
                {
                    string url = Constants.baseUrl + "api/Device";
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", token.Token);
                    client.DefaultRequestHeaders.Add("AcceptEncoding", "application/json");

                    var response = client.GetAsync(url).Result;
                    var responseBody = response.Content.ReadAsStringAsync().Result;
                    if (response.IsSuccessStatusCode)
                    {
                        devices = JsonConvert.DeserializeObject<List<UserDevice>>(responseBody);
                        return Task.FromResult(devices);
                    }
                }
            }
            catch (Exception ex)
            {
                //logs
            }
            return null;
        }

        public Task<bool> UpdateItemAsync(UserDevice item)
        {
            throw new NotImplementedException();
        }

        public bool SwitchOnOff(DeviceAction deviceAction)
        {
            Models.AuthResponse token = JsonConvert.DeserializeObject<Models.AuthResponse>(Preferences.Get("Token", ""));

            using (HttpClient client = new HttpClient())
            {
                string url = Constants.baseUrl + "api/Device/Control";
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Authorization", token.Token);
                client.DefaultRequestHeaders.Add("AcceptEncoding", "application/json");

                var response = client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(deviceAction), Encoding.UTF8, "application/json")).Result;
                var responseBody = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }

            return false;
        }

        public bool ResetWifi(DeviceAction deviceAction)
        {
            Models.AuthResponse token = JsonConvert.DeserializeObject<Models.AuthResponse>(Preferences.Get("Token", ""));

            using (HttpClient client = new HttpClient())
            {
                string url = Constants.baseUrl + "api/Device/ResetWifi";
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Authorization", token.Token);
                client.DefaultRequestHeaders.Add("AcceptEncoding", "application/json");

                var response = client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(deviceAction), Encoding.UTF8, "application/json")).Result;
                var responseBody = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsOnline(string deviceSerial)
        {
            try
            {
                Models.AuthResponse token = JsonConvert.DeserializeObject<Models.AuthResponse>(Preferences.Get("Token", ""));

                using (HttpClient client = new HttpClient())
                {
                    string url = Constants.baseUrl + "api/Device/IsOnline/" + deviceSerial;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", token.Token);
                    client.DefaultRequestHeaders.Add("AcceptEncoding", "application/json");

                    var response = client.GetAsync(url).Result;
                    var responseBody = response.Content.ReadAsStringAsync().Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return bool.Parse(responseBody);
                    }
                }
            }
            catch (Exception ex)
            {
                //logs
            }
            return false;
        }

        public DeviceStatus CheckStatus(string deviceSerial)
        {
            try
            {
                Models.AuthResponse token = JsonConvert.DeserializeObject<Models.AuthResponse>(Preferences.Get("Token", ""));

                using (HttpClient client = new HttpClient())
                {
                    string url = Constants.baseUrl + "api/Device/CheckStatus/" + deviceSerial;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", token.Token);
                    client.DefaultRequestHeaders.Add("AcceptEncoding", "application/json");

                    var response = client.GetAsync(url).Result;
                    var responseBody = response.Content.ReadAsStringAsync().Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<DeviceStatus>(responseBody);
                    }
                }
            }
            catch (Exception ex)
            {
                //logs
            }
            return null;
        }
    }
}
