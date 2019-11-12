using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace Pomodoro.ViewModels
{
    public class ConfigurationPageViewModel : NotificationObject
    {
        private ObservableCollection<int> pomodoroDurations;

        public ObservableCollection<int> PomodoroDurations
        {
            get { return pomodoroDurations; }
            set
            {
                pomodoroDurations = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<int> breakDurations;

        public ObservableCollection<int> BreakDurations
        {
            get { return breakDurations; }
            set
            {
                breakDurations = value;
                OnPropertyChanged();
            }
        }

        private int selectedPomodoroDuration;

        public int SelectedPomodoroDuration
        {
            get { return selectedPomodoroDuration; }
            set
            {
                selectedPomodoroDuration = value;
                OnPropertyChanged();
            }
        }

        private int selectedBreakDuration;

        public int SelectedBreakDuration
        {
            get { return selectedBreakDuration; }
            set
            {
                selectedBreakDuration = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; set; }


        public ConfigurationPageViewModel(IUserDialogs dialogs) : base(dialogs)
        {
            LoadPomodoroDurations();
            LoadBreakDurations();
            LoadConfigurations();
            SaveCommand = new Command(SaveCommandExecute);
        }


        private void LoadBreakDurations()
        {

            BreakDurations = new ObservableCollection<int>
            {
                1,
                5,
                10,
                15,
                20
            };

        }

        private void LoadPomodoroDurations()
        {
            PomodoroDurations = new ObservableCollection<int>
            {
                1,
                5,
                10,
                25,
                45
            };

        }


        private void LoadConfigurations()
        {
            if(Application.Current.Properties.ContainsKey(Literals.PomodoroDuration))
            {
                SelectedPomodoroDuration = (int)Application.Current.Properties[Literals.PomodoroDuration];
            }

             
            if (Application.Current.Properties.ContainsKey(Literals.BreakDuration))
            {
                SelectedBreakDuration = (int)Application.Current.Properties[Literals.BreakDuration];
            }

        }

        private async void SaveCommandExecute()
        {
            Application.Current.Properties[Literals.PomodoroDuration] = SelectedPomodoroDuration;

            Application.Current.Properties[Literals.BreakDuration] = SelectedBreakDuration;
            await Application.Current.SavePropertiesAsync();
        }
    }
}
