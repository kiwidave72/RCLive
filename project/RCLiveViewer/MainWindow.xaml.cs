using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using LiveRC.Common;

namespace LiveRCViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private History history; 
        private Player player; 

        LiveRCService service = new LiveRCService();

        public  RaceData myrace { get; set; }
        public List<DriverData> myDriverData { get; set; }
        public RaceClock myclock { get; set; }

        public MainWindow()
        {
            //history= new History();
            //player = new Player(history);

            
            myclock = new RaceClock();
            myrace =new RaceData();
            
            myDriverData = new List<DriverData>();
            myDriverData.Add(new DriverData());
            myDriverData.Add(new DriverData());
            myDriverData.Add(new DriverData());
            myDriverData.Add(new DriverData());
            myDriverData.Add(new DriverData());
            myDriverData.Add(new DriverData());
            myDriverData.Add(new DriverData());
            myDriverData.Add(new DriverData());
            myDriverData.Add(new DriverData());
            myDriverData.Add(new DriverData());
            myDriverData.Add(new DriverData());
            myDriverData.Add(new DriverData());


            InitializeComponent();
            myRaceTime.DataContext = myclock;
            DataContext = myrace;
            //fastestLap.DataContext = myrace;
            //progress.DataContext = myclock;

            myrace.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(myrace_PropertyChanged);
            mylist.ItemsSource = this.myDriverData;

        }

        System.Timers.Timer aTimer = new System.Timers.Timer(100);
        DateTime raceStarted;
        void myrace_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Status")
            {
                if (myrace.Status == "running")
                {
                    raceStarted = DateTime.Now.AddSeconds(-this.myclock.ElapsedTime );
                    aTimer.Enabled = true;
                    // Hook up the Elapsed event for the timer. 
                    aTimer.Elapsed += (_sender, _e) =>
                    {
                        this.myclock.ElapsedTime = this.myclock.ElapsedTime + 1;
                        this.myclock.RaceTime =(DateTime.Now - raceStarted);
                    };

                }
                else
                {
                    aTimer.Enabled = false;

                }
            }
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            //displayActualWidth.Text = this.ActualWidth.ToString();

            service.SetData(myrace,myDriverData,myclock);

            service.ConnectToLiveRC(null);

        }

       

    }
}
