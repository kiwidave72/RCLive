using System;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LiveRC.Common
{
    public  class MyWebSocketWrapper
    {
        private readonly Uri _uri;

        public MyWebSocketWrapper(Uri uri)
        {
            _uri = uri;
        }


        public async Task<WebSocket> ConnectAsync()
        {
            var socket = new ClientWebSocket();

            //socket.Options.SetRequestHeader(Constants.Headers.SecWebSocketVersion, Constants.Headers.SupportedVersion);
            //await socket.ConnectAsync(_uri, CancellationToken.None);

            
            TimeSpan TimeOutMilliseconds = new TimeSpan(10000);


            using (var cws = new ClientWebSocket())
            {
               // cws.Options.AddSubProtocol("websocket");
                //cws.Options.SetRequestHeader("X-CustomHeader1", "Value1");
                //cws.Options.SetRequestHeader("X-CustomHeader2", "Value2");
                using (var cts = new CancellationTokenSource(TimeOutMilliseconds))
                {
                    Task taskConnect = cws.ConnectAsync(_uri, cts.Token);
                    //Assert.True(
                    //    (cws.State == WebSocketState.None) ||
                    //    (cws.State == WebSocketState.Connecting) ||
                    //    (cws.State == WebSocketState.Open),
                    //    "State immediately after ConnectAsync incorrect: " + cws.State);
                    await taskConnect;
                }

               // Assert.Equal(WebSocketState.Open, cws.State);

                byte[] buffer = new byte[65536];
                var segment = new ArraySegment<byte>(buffer, 0, buffer.Length);
                WebSocketReceiveResult recvResult;
                using (var cts = new CancellationTokenSource(TimeOutMilliseconds))
                {
                    recvResult = await cws.ReceiveAsync(segment, cts.Token);
                }

                //Assert.Equal(WebSocketMessageType.Text, recvResult.MessageType);
                //string headers = WebSocketData.GetTextFromBuffer(segment);
                //Assert.True(headers.Contains("X-CustomHeader1:Value1"));
                //Assert.True(headers.Contains("X-CustomHeader2:Value2"));

                await cws.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
            }

            return null;
        }

       
    }
}