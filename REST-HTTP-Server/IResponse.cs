using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Net.Mime;
using System.Net.Sockets;
using System.Text;

namespace REST_HTTP_Server
{
    interface IResponse
    {
        public string[] From(Request request);
        public string[] PostHandler(Request request);
        public string CreateNewFile(string body, string path);
        public string[] GetHandler(Request request);
        public string[] GetAll(String path);
        public string[] GetOne(int number);
        public string[] DeleteHandler(Request request);
        public string[] DeleteOne(int number);
        public string[] PutHandler(Request request);
        public string[] PutOne(int number, string message);
        public int ValidOrder(string order);
        public void ServerResponse(StreamWriter write);
        

    }
}
