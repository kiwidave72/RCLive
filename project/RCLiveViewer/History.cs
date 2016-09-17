using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveRC.Common;
using System.IO;

namespace LiveRCViewer
{
    public class History
    {

        LiveRCService service = new LiveRCService();

        public Dictionary<long, DriverData> driverData;


        private RaceData myrace;
        private List<DriverData> myDriverData;
        private RaceClock myclock ;

        // live stream
        public History()
        {
            myclock = new RaceClock();
            myrace = new RaceData();
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

            service.SetData(myrace, myDriverData, myclock);
            
            driverData=new Dictionary<long,DriverData>();

            service.DriverDataEvent += new EventHandler<DriverDataEventArgs>(service_DriverDataEvent);
            service.ConnectToLiveRC(null);


        }

        void service_DriverDataEvent(object sender, DriverDataEventArgs e)
        {
            driverData.Add(e.Data.Tick,e.Data);

        }

    }
}
