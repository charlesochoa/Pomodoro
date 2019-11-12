using System;
using System.Collections.ObjectModel;
using Acr.UserDialogs;

namespace Pomodoro.ViewModels
{
    public class HistoryPageViewModel : NotificationObject
    {

        public HistoryPageViewModel(IUserDialogs dialogs) : base(dialogs)
        {

        }
        private ObservableCollection<DateTime> pomodoros;

        public ObservableCollection<DateTime> Pomodoros

        {
            get
            {
                return pomodoros;
            }
                set
            {
                pomodoros = value;
                OnPropertyChanged();
            }
        }

    }
}
