using SuperSocket.WebSocket;
using System;
using System.Collections.Generic;

namespace Jed.WebsocketServer.SampleCode
{
    internal class Program
    {
        /// <summary>
        /// 管理 客戶端 的 Session
        /// </summary>
        private static List<WebSocketSession> webSocketSessions = new List<WebSocketSession>();

        static void Main(string[] args)
        {
            // 可以利用 http://coolaf.com/tool/chattest 測試 ws://127.0.0.1:9090

            WebSocketServer server = new WebSocketServer();
            server.Setup(9090);

            Console.WriteLine("Server Open");
            server.Start();

            // 客戶端連線時
            server.NewSessionConnected += Server_NewSessionConnected;

            // 客戶端連線退出時
            server.SessionClosed += Server_SessionClosed;

            // 接收客戶端新的訊息時
            server.NewMessageReceived += Server_NewMessageReceived;
            

            string message = Console.ReadLine();
            webSocketSessions[0].Send(message);     // 指定預發送的客戶端

            Console.ReadLine();
        }

        /// <summary>
        /// [事件] 接收客戶端的新訊息
        /// </summary>
        /// <param name="session"></param>
        /// <param name="value"></param>
        private static void Server_NewMessageReceived(WebSocketSession session, string value)
        {
            Console.WriteLine($"客戶端：{session.SessionID}");
            Console.WriteLine($"MSG：{value}");

            // 兩端進行通訊過程中，都是字串
            // 譬如 WebAPI 也是大多使用 JSON
        }

        /// <summary>
        /// [事件] 客戶端連線退出
        /// </summary>
        /// <param name="session"></param>
        /// <param name="value"></param>
        private static void Server_SessionClosed(WebSocketSession session, SuperSocket.SocketBase.CloseReason value)
        {
            webSocketSessions.RemoveAll(s => s.SessionID == session.SessionID);

            Console.WriteLine($"有客戶端退出：{session.SessionID}");
        }

        /// <summary>
        /// [事件] 客戶端連線時
        /// </summary>
        /// <param name="session"></param>
        private static void Server_NewSessionConnected(WebSocketSession session)
        {
            webSocketSessions.Add(session);

            Console.WriteLine($"有客戶端進入：{session.SessionID}");
        }
    }
}
