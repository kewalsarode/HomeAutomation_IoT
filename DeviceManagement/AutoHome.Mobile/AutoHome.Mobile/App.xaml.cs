using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AutoHome.Mobile.Views;
using Xamarin.Essentials;
using Newtonsoft.Json;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AutoHome.Mobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            Models.AuthResponse token = JsonConvert.DeserializeObject<Models.AuthResponse>(Preferences.Get("Token", ""));
            
            if(token != null && token.Expires >= DateTime.UtcNow)
            {
                Services.Constants.token = token.Token;
                MainPage = new MainPage();
            }
            else
            {
                MainPage = new LoginPage();
            }
            
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public void LoadPage()
        {
            MainPage = new MainPage();
        }
    }
}
