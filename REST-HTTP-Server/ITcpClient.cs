﻿using System;
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
    interface ITcpClient
    {
        public StreamReader GetStreamReader();
        public StreamWriter GetStreamWriter();

        public void End();

    }
}
