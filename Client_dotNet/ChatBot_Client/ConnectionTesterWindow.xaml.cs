using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.Concurrent;
using System.Threading;
using System.Windows.Threading;

namespace ChatBot_Client
{
    /// <summary>
    /// Interaction logic for ConnectionTester.xaml
    /// </summary>
    public partial class ConnectionTesterWindow : Window
    {
        private readonly ObservableConcurrentCollection<string> consoleHistory;
        private readonly DispatcherTimer totalTimer;
        private readonly DispatcherTimer milisecondTimer;

        public ConnectionTesterWindow()
        {
            InitializeComponent();
            consoleHistory = new ObservableConcurrentCollection<string>();
            totalTimer = new DispatcherTimer();
            milisecondTimer = new DispatcherTimer();
            ConsoleOutput.ItemsSource = consoleHistory;
        }

        private void StartButton_OnClick(object sender, RoutedEventArgs e)
        {
            TesterSetup();
        }

        private void TesterSetup()
        {
            try
            {
                int connectionCount = ParseConnectionCountInput();
                int timeRange = ParseTimeRangeInput();
                ThreadLocal<ConnectionTester> testingThread = new ThreadLocal<ConnectionTester>(
                    () => new ConnectionTester(connectionCount, timeRange, consoleHistory)
                    );

                Thread thread = new Thread(testingThread.Value.Test);
                TimersSetup(timeRange);
                thread.Start();

            }
            catch (Exception e)
            {
                consoleHistory.AddFromEnumerable(new[] { e.Message });
            }
        }

        private void TimersSetup(int timeRange)
        {
            totalTimer.Interval = TimeSpan.FromMilliseconds(timeRange);
            Start.IsEnabled = false;
            totalTimer.Tick += TestFinishedEventHandler;

            milisecondTimer.Interval = TimeSpan.FromMilliseconds(10);
            ProgressBar.Visibility = Visibility.Visible;
            ProgressBar.Maximum = timeRange;
            ProgressBar.Value = 0;
            milisecondTimer.Tick += ProgresBarUpdateEventHandler;

            totalTimer.Start();
            milisecondTimer.Start();
        }

        private void TestFinishedEventHandler(object sender, EventArgs e)
        {
            Start.IsEnabled = true;
            ProgressBar.Visibility = Visibility.Hidden;
            ProgressBar.Value = 0;
            totalTimer.Stop();
            milisecondTimer.Stop();
        }

        private void ProgresBarUpdateEventHandler(object sender, EventArgs e)
        {
            ProgressBar.Value += 10;
        }

        private int ParseTimeRangeInput()
        {
            try
            {
                return int.Parse(TimeRangeInput.Text);
            }
            catch (Exception)
            {
                throw new Exception("Time range must be a positive integer");
            }
        }

        private int ParseConnectionCountInput()
        {
            try
            {
                return int.Parse(ConnectionCountInput.Text);
            }
            catch (Exception)
            {
                throw new Exception("Connection count must be a positive integer");
            }
        }
    }
}
