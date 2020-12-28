using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Xml.Serialization;
using System.Net.Security;

namespace REST_HTTP_Server
{
    class TcpClient: ITcpClient
    {
        

        private readonly System.Net.Sockets.TcpClient myclient;
        public TcpClient()
        {
            myclient = new System.Net.Sockets.TcpClient();
        }

        public TcpClient(System.Net.Sockets.TcpClient client)
        {
            myclient = client;
        }

        public StreamReader GetStreamReader()
        {
            return new StreamReader(myclient.GetStream());
        }
        
        public StreamWriter GetStreamWriter()
        {
            return new StreamWriter(myclient.GetStream()) { AutoFlush = true };
        }

        public void End()
        {
            myclient.Close();
        }
    }
}
