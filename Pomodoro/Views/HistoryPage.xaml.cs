using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using Pomodoro.ViewModels;
using Xamarin.Forms;

namespace Pomodoro.Views
{
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage(Acr.UserDialogs.IUserDialogs instance)
        {
            InitializeComponent();
            this.BindingContext = new HistoryPageViewModel(instance);
        }
    }
}
