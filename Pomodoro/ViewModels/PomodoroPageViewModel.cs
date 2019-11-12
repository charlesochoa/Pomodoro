using System;
using System.Timers;
using System.Windows.Input;
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

        public TimeSpan Ellapsed
        {
            get { return ellapsed; }
            set
            {
                ellapsed = value;
                OnPropertyChanged();
            }
        }

        public ICommand StartOfPauseCommand { get; set; }

        public PomodoroPageViewModel()
        {
            InitializeTimer();
            LoadConfiguredValues();
            IsRunning = true;
            StartOfPauseCommand = new Command(StartOfPauseCommandExecute);
        }

        private void LoadConfiguredValues()
        {
            pomodoroDuration = (int) Application.Current.Properties[Literals.PomodoroDuration];
            breakDuration = (int)Application.Current.Properties[Literals.BreakDuration];
        }

        private void InitializeTimer()
        {
            timer = new Timer
            {
                Interval = 1000
            };
            timer.Elapsed += Timer_Elapsed;

        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (IsRunning)
            {
                Ellapsed = Ellapsed.Add(TimeSpan.FromSeconds(17));
            } else
            {
                Ellapsed = Ellapsed.Add(TimeSpan.FromSeconds(0));
                if (toggleShowNumber)
                {

                }
            }
            
            if (IsRunning && !IsInBreak && (int)Ellapsed.TotalSeconds >= pomodoroDuration * 60)
            {
                IsInBreak = true;
                Ellapsed = TimeSpan.Zero;
            }

            if (IsRunning && IsInBreak && (int)Ellapsed.TotalSeconds >= breakDuration * 60)
            {
                IsInBreak = false;
                Ellapsed = TimeSpan.Zero;
            }
        }

        private void StartTimer()
        {
            timer.Start();
            IsRunning = true;

        }

        private void StopTimer()
        {
            timer.Stop();
            IsRunning = false;
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
            { isInBreak = value;
                OnPropertyChanged();
            }
        }

        private void StartOfPauseCommandExecute()
        {
            if (IsRunning)
            {
                StopTimer();
            } else
            {
                StartTimer();
            }
        }
    }
}
