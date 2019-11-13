using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Newtonsoft.Json;
using Pomodoro.Configuration;
using Xamarin.Forms;

namespace Pomodoro.ViewModels
{
    public class HistoryPageViewModel : NotificationObject
    {
        public ICommand ResetHistory { get; set; }
        public HistoryPageViewModel(IUserDialogs dialogs) : base(dialogs)
        {
            LoadHistory();
            ResetHistory = new Command(async () => await ResetHistoryExecute());
        }

        private void LoadHistory()
        {
            if(Application.Current.Properties.ContainsKey(Literals.History))
            {
                var json = Application.Current.Properties[Literals.History].ToString();
                var history = JsonConvert.DeserializeObject<List<DateTime>>(json);
                Pomodoros = new ObservableCollection<DateTime>(history);
            }
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



        private async Task ResetHistoryExecute()
        {
            var result = await Dialogs.ConfirmAsync(new ConfirmConfig
            {
                Message = "¿Seguro quieres eliminar tu historial de Ciclos Completados? Esta operación es permanente.",
                OkText = "Seguro",
                CancelText = "Mejor no"
            });
            if (result)
            {
                Application.Current.Properties.Remove(Literals.History);
                Pomodoros = null;
                LoadHistory();

                await Application.Current.SavePropertiesAsync();
            }

        }

    }
}
