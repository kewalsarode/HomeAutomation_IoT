using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using AutoHome.Mobile.Models;
using AutoHome.Mobile.Views;

namespace AutoHome.Mobile.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<UserDevice> Devices { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Devices";
            Devices = new ObservableCollection<UserDevice>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, UserDevice>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as UserDevice;
                Devices.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Devices.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    item.Image = Services.Constants.baseUrl +"img/" + item.DeviceType + ".jpg";
                    Devices.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}