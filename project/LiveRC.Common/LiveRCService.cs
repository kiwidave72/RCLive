using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Security.Authentication;
//using WebSocketSharp;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using WebSocket4Net;
using ErrorEventArgs = SuperSocket.ClientEngine.ErrorEventArgs;

namespace LiveRC.Common
{
    public class LiveRCService
    {
        public event EventHandler<ClockDataEventArgs> ClockDataEvent;
        public event EventHandler<RaceDataEventArgs> RaceDataEvent;
        public event EventHandler<DriverDataEventArgs> DriverDataEvent;

        public event EventHandler<EventArgs> ConnectedEvent;

        public bool IsConnected = false;
        public bool IsWebSocketConnected = false;

        private string TokenString = "";
        private WebSocket LiveRCWebSocket;

        private long offset = DateTime.Now.Ticks;
        private DateTime Timeoffset = DateTime.Now;

        private RaceClock currentRaceClock;
        private RaceData currentRaceData;
        private List<DriverData> currentDriverData;

        private string filename = null;

        public void SetData(RaceData raceData,List<DriverData> driverData,RaceClock raceClock)
        {
            currentRaceData = raceData;
            currentDriverData = driverData;
            currentRaceClock = raceClock;

        }
        public void ConnectToLiveRC(string filename)
        {
            this.filename = filename;
//            WebRequest request = WebRequest.Create("http://livedata.liverc.com:8080/socket.io/1/?userType=web_viewer&t=1410846873516");
            //WebRequest request = WebRequest.Create("http://livedata.liverc.com:8080/socket.io/1/?userType=web_viewer&t=1474073574904");

            WebRequest request = WebRequest.Create("https://livedata.liveracemedia.com/socket.io/1/?userType=scoring&t=1613609704219");
            //request.AuthenticationLevel

            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;
            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            
            if (((HttpWebResponse)response).StatusCode == HttpStatusCode.OK)
            {
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                Console.WriteLine(responseFromServer);
                TokenString = responseFromServer;
                // Clean up the streams and the response.
                reader.Close();


                SecWebSocketKey =  response.Headers["Sec-WebSocket-Accept"];



                // raise connected event
                EventArgs args = new EventArgs();
                this.IsConnected = true;
                OnConnected(args);
            }
            response.Close();


        }

        public string SecWebSocketKey { get; set; }

        private void ConnectToWebSocket()
        {
            Thread.Sleep(4000);

            var responseArray = TokenString.Split(':');

            string wsToken = responseArray[0];

            long offset = DateTime.Now.Ticks;
            DateTime Timeoffset = DateTime.Now;


            //var webSocketAddess ="wss://livedata.liveracemedia.com/socket.io/1/websocket/ny7Ktw_0-l41grrgeMSj?userType=scoring";


            var webSocketAddess = "wss://livedata.liveracemedia.com/socket.io/1/websocket/" + wsToken + "?userType=scoring";

            //string webSocketAddess = "ws://livedata.liverc.com:8080/socket.io/1/websocket/" + wsToken + "?userType=web_viewer";

            Console.WriteLine(webSocketAddess);


            //var mywebSocket = new MyWebSocketWrapper(new Uri(webSocketAddess));

            //Task.Run(() => mywebSocket.ConnectAsync());


            LiveRCWebSocket = new WebSocket(webSocketAddess,version:WebSocketVersion.Rfc6455,sslProtocols:SslProtocols.Default);
            LiveRCWebSocket.MessageReceived += WebsocketOnMessageReceived;
            LiveRCWebSocket.Error += WebsocketOnError;
            LiveRCWebSocket.Opened += WebsocketOnOpened;
            LiveRCWebSocket.Closed += LiveRCWebSocketOnClosed;
            LiveRCWebSocket.Open();


            //LiveRCWebSocket = new WebSocket(webSocketAddess);//, new[] { "permessage-deflate","client_max_window_bits" });
            //                                                 //LiveRCWebSocket.Compression = CompressionMethod.Deflate;
            //                                                 //LiveRCWebSocket.Extensions

            ////LiveRCWebSocket.Origin = "https://dnc.liverc.com";


            //LiveRCWebSocket.OnError += new EventHandler<WebSocketSharp.ErrorEventArgs>(LiveRCWebSocket_OnError);
            //LiveRCWebSocket.OnMessage += new EventHandler<MessageEventArgs>(LiveRCWebSocket_OnMessage);

            //LiveRCWebSocket.OnOpen += new EventHandler(LiveRCWebSocket_OnOpen);
            //LiveRCWebSocket.OnClose += new EventHandler<WebSocketSharp.CloseEventArgs>(LiveRCWebSocket_OnClosed);


            //LiveRCWebSocket.Connect();

        }

        private void LiveRCWebSocketOnClosed(object sender, EventArgs e)
        {
            Console.WriteLine("Closed");
        }

        private void WebsocketOnOpened(object sender, EventArgs e)
        {

            Console.WriteLine("Opened");

            var join = @"5:::{""name"":""joinTrack"",""args"":[""t577""]}";
            LiveRCWebSocket.Send(join);

        }

        private void WebsocketOnError(object sender, ErrorEventArgs e)
        {
            Console.WriteLine(e.Exception.Message);
        }

        private void WebsocketOnMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        private void Websocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        //private void LiveRCWebSocket_OnClosed(object sender, CloseEventArgs e)
        //{

        //}


        //private void retry()
        //{
        //    LiveRCWebSocket.Connect();

        //}

        //void LiveRCWebSocket_OnMessage(object sender, MessageEventArgs e)
        //{
        //    Console.WriteLine((DateTime.Now - Timeoffset).TotalSeconds + "->" + e.Data );



        //    // write out to a file??
        //    var array = e.Data.Split(':');

        //    if (array[0] == "1")
        //    {
        //        //var join = @"5:::{""name"":""joinTrack"",""args"":[""t577""]}";

        //        //Console.WriteLine(join);

        //        //LiveRCWebSocket.Send(join);

        //    }
        //    else if (array[0] != "2")//not a ping
        //    {
        //        if (!string.IsNullOrEmpty(filename))
        //        {
        //            TextWriter writer = File.AppendText(filename);
        //            writer.WriteLine(DateTime.UtcNow.AddHours(1).Ticks + "|" + DateTime.UtcNow.AddHours(1).ToString() + "|" + e.Data);
        //            writer.Close();
        //        }
        //        Console.WriteLine((DateTime.Now - Timeoffset).TotalSeconds + "->" + e.Data);

        //        // parse race data
        //        if (array[0] == "5" && array[4].Contains("updateRaceData"))
        //        {
        //            //"args":["Nitro Buggy|1|running|780|11|15|1|Hebert, Guillaume 3/2:14.338 (Rnd 1)"]}
        //            string input = array[5].Remove(0, 2);
        //            input=input.Substring(0, input.Length - 4);

        //            string[] data = input.Split('|');
        //            currentRaceData.RaceTitle= data[0];

        //                currentRaceData.RoundNumber = TryConvertToInt(data[6]);
        //                currentRaceData.RaceNumber = Convert.ToInt16(data[4]);
        //                currentRaceData.Status = data[2];



        //        }
        //        else if (array[0] == "5" && array[4].Contains("updateClockData"))
        //        {
        //            //5:::{"name":"updateClockData","args":["336|264"]}
        //            string input = e.Data.Remove(0, e.Data.IndexOf("args") +8);
        //            input = input.Substring(0, input.Length - 3);

        //            string[] data = input.Split('|');
        //            currentRaceClock.ElapsedTime = TryConvertToIntWhereDecimal(data[0]);
        //            currentRaceClock.RemainingTime = TryConvertToIntWhereDecimal(data[1]);

        //            //if (currentRaceData.Status == "staging" || currentRaceData.Status==null)
        //            //{
        //                //currentRaceClock.ElapsedTime = 0;
        //                currentRaceClock.Length = currentRaceClock.ElapsedTime + currentRaceClock.RemainingTime+60 ;
        //            //}
        //                ClockDataEventArgs args = new ClockDataEventArgs();
        //                args.Data = currentRaceClock;
        //                OnUpdateClockData(args);

        //        }
        //        else if (array[0] == "5" && array[4].Contains("updateDriverData"))
        //        {
        //            //"args":["{\"p1\":\"1|Tebo, Jared|12|19|37.677|12:37.3|20/13:17.2|37.631|1|false|0|8842||||39.86||12||\"
        //            //,\"p2\":\"2|Kellum, Kurt|11|17|41.862|12:05.2|19/13:30.5|36.454|1|false|0|8764||||42.66||2||\",\"p3\":\"3|Batlle, Robert|6|17|38.069|12:14.3|19/13:40.7|37.474|1|false|0|8711||||43.20|9.1|9||\",\"p4\":\"4|Osaka, Kenji|5|16|41.062|11:57.3|18/13:27.0|39.123|1|false|0|8798||||44.83||13||\",\"p5\":\"5|Hudy, Juraj|8|16|44.740|12:09.1|18/13:40.2|3.132|1|false|0|8755||||45.57|11.8|3||\",\"p6\":\"6|Sartel, Jerome|7|16|39.475|12:13.0|18/13:44.7|38.808|1|false|0|8827||||45.82|3.9|14||\",\"p7\":\"7|Pfeiffer, Gerd|4|15|48.084|11:53.5|17/13:28.6|20.196|1|false|0|8806||||47.57||1||\",\"p8\":\"8|Vutov, Mario|2|14|62.183|12:01.9|16/13:45.1|43.827|1|false|0|8856||||51.57||13||\",\"p9\":\"9|Bottoso, Nicola|3|14|41.169|12:18.0|15/13:10.7|41.169|1|false|0|8717||||52.72|16.0|14||\",\"p10\":\"10|Campanelli, William|1|13|43.699|12:02.4|15/13:53.6|41.841|1|false|0|8723||||55.57||9||\",\"p11\":\"11|Siboni, Hovav|10|13|60.180|12:05.2|14/13:01.0|49.715|1|false|0|8832||||55.79|2.8|2||\",\"p12\":\"12|Martin, Lee|9|10|39.085|12:12.9|11/13:26.2|37.860|1|false|0|8779||||73.29||4||\",\"p13\":\"\",\"p14\":\"\",\"p15\":\"\",\"p16\":\"\",\"p17\":\"\",\"p18\":\"\",\"p19\":\"\",\"p20\":\"\"}"]}

        //            string input = e.Data.Remove(0, e.Data.IndexOf("args")+11);
        //            input = input.Substring(0, input.Length - 4);
        //            int startPos = 0;
        //            int endPos = 5;



        //            string p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12;

        //            p1=p2=p3=p4=p5=p6=p7=p8=p9=p10=p11=p12    =@"";

        //            ParseLine(1,input, "p1\\", out p1, currentDriverData[0]);
        //            ParseLine(2,input, "p2\\", out p2, currentDriverData[1]);
        //            ParseLine(3,input, "p3\\", out p3, currentDriverData[2]);
        //            ParseLine(4,input, "p4\\", out p4, currentDriverData[3]);
        //            ParseLine(5,input, "p5\\", out p5, currentDriverData[4]);
        //            ParseLine(6,input, "p6\\", out p6, currentDriverData[5]);
        //            ParseLine(7,input, "p7\\", out p7, currentDriverData[6]);
        //            ParseLine(8,input, "p8\\", out p8, currentDriverData[7]);
        //            ParseLine(9,input, "p9\\", out p9, currentDriverData[8]);
        //            ParseLine(10,input, "p10\\", out p10, currentDriverData[9]);
        //            ParseLine(11,input, "p11\\", out p11, currentDriverData[10]);
        //            ParseLine(12,input, "p12\\", out p12, currentDriverData[11]);
        //        }
        //    }
        //}
        private int TryConvertToIntWhereDecimal(string value)
        {

            if (value.Contains('.'))
            {
                return Convert.ToInt32(value.Substring(0, value.IndexOf('.')));
            }
            else
            {
                return Convert.ToInt32(value);
            }


        }
        private int TryConvertToInt(string value)
        {

            try
            {
                return Convert.ToInt16(value);
            }
            catch
            {

                try
                {
                    return TryConvertToInt(value[1].ToString());
                }
                catch
                {
                    return 0;
                }

            }

        }



        private void ParseLine(int position,string input,string pos, out string result,DriverData driverData)
        {
            if (input.Contains(pos))
                result = input.Substring(input.IndexOf(pos), input.Length - input.IndexOf(pos));
            else
                result = "";

            if (!string.IsNullOrEmpty(result))
            {
                string[] array = result.Split('|');
                if (array.Count() ==1)
                {
                    return;
                }
                string[] name = array[1].Split(',');

                driverData.Position = position;
                driverData.Number = Convert.ToInt16(array[2]);
                driverData.Name = name[0];
                driverData.Laps = Convert.ToInt16( array[3]);
                driverData.Time = array[5];
                driverData.Fastest = array[7];
                driverData.Average = array[15];
                driverData.Difference = array[16];
                driverData.Tick = DateTime.Now.Ticks;

                DriverDataEventArgs args = new DriverDataEventArgs();
                args.Data = driverData;
                OnUpdateDriverData(args);

                //DateTime date = DateTime.Parse(driverData.Fastest);
                if (driverData.Fastest != "")
                {
                    TimeSpan tempTime = TimeSpan.FromSeconds(Convert.ToDouble(driverData.Fastest));
                    if (currentRaceData.FastestLapTime < tempTime)
                    {
                        currentRaceData.FastestLapTime = tempTime;
                        currentRaceData.FastestLapDriverName = driverData.Name;
                        currentRaceData.FastestLapOnLap = driverData.Laps;
                        RaceDataEventArgs raceDataargs = new RaceDataEventArgs();
                        raceDataargs.Data = currentRaceData;
                        OnUpdateRaceData(raceDataargs);
                    }
                }
            }

        }

        void LiveRCWebSocket_OnError(object sender, WebSocketSharp.ErrorEventArgs e)
        {

            Console.WriteLine((DateTime.Now - Timeoffset).TotalSeconds + "->" + e.Message);
            Timeoffset = DateTime.Now;
            //Thread.Sleep(4000);
            //retry();

        }

        void LiveRCWebSocket_OnOpen(object sender, EventArgs e)
        {

            //Console.WriteLine(e);


            // tell liverc what we want to watch
            //LiveRCWebSocket.Send(@"5:::{""name"":""joinTrack"",""args"":[""t130""]}");

            //var join2 = @"5:::{""name"":""joinTrack"",""args"":[""t577""]}";


            //var join = @"5:::{""name"":""joinTrack"",""args"":[""t577""]}";

            //Console.WriteLine(join);

            //LiveRCWebSocket.Send(join);
            //LiveRCWebSocket.Send("2::");

            //System.Timers.Timer aTimer;
            //aTimer = new System.Timers.Timer(30000);
            //// Hook up the Elapsed event for the timer. 
            //aTimer.Elapsed += (_sender, _e) =>
            //{
            //    LiveRCWebSocket.Send("2::");
            //};
            //aTimer.Enabled = true;
        }

        protected virtual void OnUpdateClockData(ClockDataEventArgs e)
        {
            EventHandler<ClockDataEventArgs> handler = ClockDataEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        protected virtual void OnUpdateRaceData(RaceDataEventArgs e)
        {
            EventHandler<RaceDataEventArgs> handler = RaceDataEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        protected virtual void OnUpdateDriverData(DriverDataEventArgs e)
        {
            EventHandler<DriverDataEventArgs> handler = DriverDataEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }


        protected virtual void OnConnected(EventArgs e)
        {
            EventHandler<EventArgs> handler = ConnectedEvent;
            if (handler != null)
            {
                handler(this, e);
            }

            ConnectToWebSocket();

        }

    }

    public class ClockDataEventArgs : EventArgs
    {
        public RaceClock Data { get; set; }
    }
    public class RaceDataEventArgs : EventArgs
    {
         public RaceData Data { get; set; }
    }
    public class DriverDataEventArgs : EventArgs
    {
        public DriverData Data { get; set; }
    }
}
