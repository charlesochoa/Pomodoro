using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Pomodoro.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pomodoro.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RootPage : MasterDetailPage
    {
        public RootPage()
        {
            InitializeComponent();
            this.BindingContext = new RootPageViewModel(UserDialogs.Instance);
            Color navColor = Color.FromHex( "#143963");

            MessagingCenter.Subscribe<RootPageViewModel>(this, "GoToPomodoro", (a) =>
            {
                Detail = new NavigationPage(new PomodoroPage(UserDialogs.Instance))
                {
                    BarBackgroundColor = navColor
                };
                IsPresented = false;

            });

            MessagingCenter.Subscribe<RootPageViewModel>(this, "GoToConfiguration", (a) =>
            {
                Detail = new NavigationPage(new ConfigurationPage(UserDialogs.Instance))
                {
                    BarBackgroundColor = navColor
                };
                IsPresented = false;

            });


            MessagingCenter.Subscribe<RootPageViewModel>(this, "GoToHistory", (a) =>
            {
                Detail = new NavigationPage(new HistoryPage(UserDialogs.Instance))
                {
                    BarBackgroundColor = navColor,
                };
                IsPresented = false;

            });
            

        }

    }
}
