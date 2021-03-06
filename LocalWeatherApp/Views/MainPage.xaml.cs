﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using LocalWeatherApp.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using LocalWeatherApp.Models;

namespace LocalWeatherApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {

            this.InitializeComponent();
            
            this.MasterBehavior = MasterBehavior.Popover;
            this.Detail = new NavigationPage(App.ServiceProvider.GetService<WeatherPage>());
            this.MenuPages.Add((int)MenuItemType.Weather, (NavigationPage)this.Detail);
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!this.MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Weather:
                        this.MenuPages.Add(id, new NavigationPage(App.ServiceProvider.GetService<WeatherPage>().CheckForNull()));
                        break;
                    case (int)MenuItemType.About:
                        this.MenuPages.Add(id, new NavigationPage(App.ServiceProvider.GetService<AboutPage>().CheckForNull()));
                        break;
                }
            }

            var newPage = this.MenuPages[id];

            if (newPage != null && this.Detail != newPage)
            {
                this.Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                this.IsPresented = false;
            }
        }
    }
}