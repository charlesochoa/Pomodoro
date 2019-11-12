﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Pomodoro.ViewModels;
using Pomodoro.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pomodoro
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RootPage : MasterDetailPage
    {
        public RootPage()
        {
            InitializeComponent();
            this.BindingContext = new RootPageViewModel(UserDialogs.Instance);

            MessagingCenter.Subscribe<RootPageViewModel>(this, "GoToPomodoro", (a) =>
            {
                Detail = new NavigationPage(new PomodoroPage(UserDialogs.Instance));
                IsPresented = false;

            });

            MessagingCenter.Subscribe<RootPageViewModel>(this, "GoToConfiguration", (a) =>
            {
                Detail = new NavigationPage(new ConfigurationPage(UserDialogs.Instance));
                IsPresented = false;

            });

        }

    }
}
