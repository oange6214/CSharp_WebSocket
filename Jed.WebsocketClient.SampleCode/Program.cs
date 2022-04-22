using System;
using WebSocket4Net;

namespace Jed.WebsocketClient.SampleCode
{
    internal class Program
    { 
        static void Main(string[] args)
        {
            // 期望 連接到某個 WebSocket Server（ws = web socket)
            WebSocket webSocket = new WebSocket("ws://127.0.0.1:9090");
            
            webSocket.Open();

            webSocket.Opened += WebSocket_Opened;
            webSocket.Error += WebSocket_Error;
            webSocket.MessageReceived += WebSocket_MessageReceived;

            // 資訊傳送
            webSocket.Send(Console.ReadLine());

            //webSocket.Close();
            Console.ReadLine();
        }

        private static void WebSocket_Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            Console.WriteLine($"錯誤了{e.Exception}");
        }

        private static void WebSocket_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("Client Connect to Sucessful");
        }

        private static void WebSocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
