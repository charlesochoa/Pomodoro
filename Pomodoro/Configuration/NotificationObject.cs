using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Acr.UserDialogs;

namespace Pomodoro.Configuration
{
    public abstract class NotificationObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected IUserDialogs Dialogs { get; }


        public NotificationObject(IUserDialogs dialogs)
        {
            this.Dialogs = dialogs;

        }
    }
}
