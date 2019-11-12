using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace Pomodoro.ViewModels
{
    public class RootPageViewModel : NotificationObject
    {
        private ObservableCollection<string> menuItems;

        public ObservableCollection<string> MenuItems
        {
            get { return menuItems; }
            set
            {
                menuItems = value;
                OnPropertyChanged();
            }
        }

        private string selectedMenuItem;

        public string SelectedMenuItem
        {
            get { return selectedMenuItem; }
            set
            {
                selectedMenuItem = value;
                OnPropertyChanged();
            }
        }

        public RootPageViewModel()
        {
            MenuItems = new ObservableCollection<string>
            {
                "Pomodoro",
                "Historial",
                "Configuración"
            };

            PropertyChanged += RootPageViewModel_PropertyChanged;


        }

        private void RootPageViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedMenuItem))
            {
                if(SelectedMenuItem == "Configuración")
                {
                    MessagingCenter.Send(this, "GoToConfiguration");
                } else if (SelectedMenuItem == "Pomodoro")
                {
                    MessagingCenter.Send(this, "GoToPomodoro");
                }
            }
        }
    }
}
