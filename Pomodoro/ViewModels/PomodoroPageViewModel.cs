using System;
using System.Timers;
using System.Windows.Input;
using Xamarin.Forms;

namespace Pomodoro.ViewModels
{
    public class PomodoroPageViewModel : NotificationObject
    {
        private Timer timer;
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
            StartOfPauseCommand = new Command(StartOfPauseCommandExecute);
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;

        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Ellapsed = Ellapsed.Add(TimeSpan.FromSeconds(1));
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
