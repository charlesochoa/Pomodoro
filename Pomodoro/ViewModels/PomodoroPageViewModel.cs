using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using Acr.UserDialogs;
using Newtonsoft.Json;
using Pomodoro.Configuration;
using Xamarin.Forms;

namespace Pomodoro.ViewModels
{
    public class PomodoroPageViewModel : NotificationObject
    {
        private Timer timer;
        private int pomodoroDuration;
        private int breakDuration;
        private bool toggleShowNumber = true;
        private TimeSpan ellapsed;
        private string dynamicTextColor = "DarkSlateGray";
        private int durationP;

        private int durationB;

        public int DurationP
        {
            get { return durationP; }
            set
            {
                durationP = value;
                OnPropertyChanged();
            }
        }

        public int DurationB
        {
            get { return durationB; }
            set
            {
                durationB = value;
                OnPropertyChanged();
            }
        }

        public string DynamicTextColor
        {
            get { return dynamicTextColor; }
            set
            {
                dynamicTextColor = value;
                OnPropertyChanged();
            }
        }


        public TimeSpan Ellapsed
        {
            get { return ellapsed; }
            set
            {
                ellapsed = value;
                OnPropertyChanged();
            }
        }


        public ICommand Open { get; }

        public ICommand StartOfPauseCommand { get; set; }



        public PomodoroPageViewModel(IUserDialogs dialogs) : base(dialogs)
        {

            InitializeTimerAsync();
            LoadConfiguredValues();
            IsInWork = true;
            DurationP = pomodoroDuration * 60;
            DurationB = breakDuration * 60;
            StartOfPauseCommand = new Command(async () => await StartOfPauseCommandExecute());

        }

        private void LoadConfiguredValues()
        {
            pomodoroDuration = (int)Application.Current.Properties[Literals.PomodoroDuration];
            breakDuration = (int)Application.Current.Properties[Literals.BreakDuration];
        }

        private void InitializeTimerAsync()
        {
            timer = new Timer
            {
                Interval = 1000
            };
            timer.Elapsed += async (sender, e) => await Timer_Elapsed(sender, e);

        }

        private async Task Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (IsRunning)
            {
                Ellapsed = Ellapsed.Add(TimeSpan.FromSeconds(1));
            } else
            {
                Ellapsed = Ellapsed.Add(TimeSpan.FromSeconds(0));
                if (toggleShowNumber)
                {

                }
            }

            if (IsRunning && IsInWork && !IsInBreak && (int)Ellapsed.TotalSeconds >= pomodoroDuration * 60)
            {
                IsInBreak = true;
                IsInWork = false;
                Ellapsed = TimeSpan.Zero;
                StopTimer();
                await SavePomodoroAsync();
            }

            if (IsRunning && IsInBreak && !IsInWork && (int)Ellapsed.TotalSeconds >= breakDuration * 60)
            {
                IsInBreak = false;
                IsInWork = true;
                Ellapsed = TimeSpan.Zero;
                StopTimer();
            }
        }

        private async Task SavePomodoroAsync()
        {
            List<DateTime> history;
            if(Application.Current.Properties.ContainsKey(Literals.History))
            {

                var json = Application.Current.Properties[Literals.History].ToString();
                history = JsonConvert.DeserializeObject<List<DateTime>>(json);
                
            }
            else
            {
                history = new List<DateTime>();
            }
            history.Add(DateTime.Now);

            var serializedObject = JsonConvert.SerializeObject(history);
            Application.Current.Properties[Literals.History] = serializedObject;

            await Application.Current.SavePropertiesAsync();
        }

        private void StartTimer()
        {
            timer.Start();
            IsRunning = true;
            if (IsInBreak)
            {
                DynamicTextColor = "DarkGreen";
            } else if (IsInWork)
            {
                DynamicTextColor = "DarkRed";
            }

        }

        private void StopTimer()
        {
            timer.Stop();
            IsRunning = false;
            DynamicTextColor = "DarkSlateGray";
        }

        private bool isRunning;

        public bool IsRunning
        {
            get { return isRunning; }
            set
            {
                isRunning = value;
                OnPropertyChanged(); 
            }
        }

        private bool isInBreak;

        public bool IsInBreak
        {
            get { return isInBreak; }
            set
            {
                isInBreak = value;
                OnPropertyChanged();
            }
        }


        private bool isInWork;

        public bool IsInWork
        {
            get { return isInWork; }
            set
            {
                isInWork = value;
                OnPropertyChanged();
            }
        }




        private async Task StartOfPauseCommandExecute()
        {
            

            if (IsRunning)
            {
                if (IsInWork)
                {
                    var result = await Dialogs.ConfirmAsync(new ConfirmConfig
                    {
                        Message = "¿Seguro quieres detener tu ciclo de trabajo? Esto reiniciará tu temporizador a cero.",
                        OkText = "Parar",
                        CancelText = "¡Quiero seguir!"
                    });
                    if (result)
                    {
                        Ellapsed = TimeSpan.Zero;
                        StopTimer();
                    }
                }
                else if (IsInBreak)
                {
                    Dialogs.Alert("No puedes detener un ciclo de descanso, aprovéchalo mientras tengas tiempo.");
                }
                
            }
            else
            {
                if (!IsInWork && !IsInBreak)
                {
                    IsInWork = true;
                }
                StartTimer();
            }
        }
    }
}
