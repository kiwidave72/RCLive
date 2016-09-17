using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using WebSocketSharp;
using System.Threading;
using System.Timers;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using LiveRC.Common;


namespace LiveRC
{
    class Program
    {
        public WebSocket ws;

        static void Main(string[] args)
        {

                LiveRCService service = new LiveRCService();
            
                RaceData myrace = new RaceData();
                List<DriverData> myDriverData = new List<DriverData>();
                RaceClock myclock = new RaceClock();

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


                
                service.SetData(myrace,myDriverData,myclock);
                service.ConnectToLiveRC("Q1_Buggy_"+DateTime.Now.ToLongTimeString().Replace(" ","_").Replace(".","_").Replace(":","_")+"_.txt");


                ConsoleKeyInfo key; 
                do{

                    //ws.Send("");

                    //Thread.Sleep(1000);
                    //ws.Ping("");

                    key = Console.ReadKey(); 

                }
                while(key.Key!=ConsoleKey.Escape);
        }

       

        }
}
