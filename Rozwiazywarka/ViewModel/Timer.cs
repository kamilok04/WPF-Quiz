using Quiz.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Rozwiazywarka.ViewModel
{
    class Timer : ObservableObject
    {
        DispatcherTimer timer = new();
        private int _timeElapsed;
        public Timer()
        {
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }
        public int TimeElapsed
        { get { return _timeElapsed; } }
        public void Start()
        {
            timer.Start();
        }
        public void Stop()
        {
            timer.Stop();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            _timeElapsed++;
            OnPropertyChanged(nameof(TimeElapsed));
        }

    }
}
