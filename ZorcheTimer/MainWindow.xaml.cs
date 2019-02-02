using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace ZorcheTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _firstRun = true;
        private Stopwatch _swWork = new Stopwatch();
        private Stopwatch _swDist = new Stopwatch();
        private DispatcherTimer _timer;

        public MainWindow()
        {
            InitializeComponent();

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(500)
            };
            _timer.Tick += (o, e) => UpdateLabel();
            _timer.Start();
        }

        private void UpdateLabel()
        {
            TimerLabelWorking.Content = $"Working {_swWork.Elapsed:hh':'mm':'ss}";
            TimerLabelDistracted.Content = $"Distracted { _swDist.Elapsed:hh':'mm':'ss}";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var isWorking = _swWork.IsRunning;
            if (!_firstRun)
            {
                if (isWorking)
                {
                    _swWork.Stop();
                    _swDist.Start();
                } else
                {
                    _swWork.Start();
                    _swDist.Stop();
                }
            }
            else
            {
                _swDist.Reset();
                _swWork.Restart();
                _firstRun = false;
            }
            FlipButton.Content = "Flip " + (isWorking ? "D" : "W");
            FlipButton.ToolTip = isWorking ? "Distracted": "Working";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _swWork.Stop();
            _swDist.Stop();
            FlipButton.Content = "Start";
            _firstRun = true;
        }
    }
}
