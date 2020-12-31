using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace REST_HTTP_Server
{
    public interface IFileHandler
    {

        public void NewMessage(string message, string filename);
        public FileInfo[] GetFileInfo(string theWay);
        public string GetAllMessages(FileInfo[] files);
        public string GetOneMessage(FileInfo[] files, int number);
        public void Delete(FileInfo[] files, int number);
        public void Override(FileInfo[] files, int number, string message);
    }
}
