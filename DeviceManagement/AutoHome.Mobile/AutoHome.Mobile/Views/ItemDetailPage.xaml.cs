using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AutoHome.Mobile.Models;
using AutoHome.Mobile.ViewModels;

namespace AutoHome.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var device = new UserDevice
            {
                Name = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new ItemDetailViewModel(device);
            BindingContext = viewModel;
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            viewModel.SwitchCommand.Execute(e.Value);
        }

        private void Remove_Clicked(object sender, EventArgs e)
        {
           
        }
    }
}