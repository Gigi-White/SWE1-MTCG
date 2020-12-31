using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Xml.Serialization;

namespace REST_HTTP_Server
{
    public interface IHTTPServer
    {
        public void Run();
        public void HandleClient(Object client);

        public TcpClient GetClient();
        public string ReadStream(ITcpClient myTcpClient);






    }
}
