using AutoHome.Mobile.Services;
using System;
using System.ComponentModel;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Essentials;
using Newtonsoft.Json;

namespace AutoHome.Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Action DisplayInvalidLoginPrompt;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        AuthService authService = new AuthService();
        public Command LoginCommand { get; set; }
        private INavigation navigation;

        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                PropertyChanged(this, new PropertyChangedEventArgs("username"));
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("password"));
            }
        }

        public LoginViewModel(INavigation navigation)
        {
            Title = "Login";

            LoginCommand = new Command(() => ExecuteLoginCommand());
            this.navigation = navigation;
        }

        private void ExecuteLoginCommand()
        {
            var token = authService.Login(username, password);
            if (token == null)
            {
                DisplayInvalidLoginPrompt();
            }
            else
            {
                Preferences.Set("Token", JsonConvert.SerializeObject(token));
                App.Current.MainPage = new Views.MainPage();
            }
        }

    }
}
