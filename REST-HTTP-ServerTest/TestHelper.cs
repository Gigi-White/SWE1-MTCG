using Moq;
using NUnit.Framework;
using REST_HTTP_Server;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Xml.Serialization;

namespace REST_HTTP_ServerTest
{
    public class TestHelper
    {
        //create my fake IFilhanlder, 
        public static Mock<IFileHandler> GeneratesFileHandlerMock(string[]files, string messages)
        {
            var filehanlder = new Mock<IFileHandler>();
            //create fake FileInfo Array from files array;
            FileInfo[] fakeFileNames = new FileInfo[5];
            int i = 0;
            foreach(var item in files)
            {
                FileInfo one = new FileInfo(item);
                fakeFileNames[i] = one;
                i++;
                if (i==5)
                {
                    break;
                }
            }



            filehanlder.Setup(mr => mr.NewMessage(It.IsAny<string>(), It.IsAny<string>()))
                       .Verifiable();
            
            filehanlder.Setup(mr => mr.GetFileInfo(It.IsAny<string>()))
                       .Returns(fakeFileNames);
            
            filehanlder.Setup(mr => mr.GetAllMessages(It.IsAny<FileInfo[]>()))
                       .Returns(messages);
            
            filehanlder.Setup(mr => mr.GetOneMessage(It.IsAny<FileInfo[]>(), It.IsAny<int>()))
                       .Returns(messages);
            
            filehanlder.Setup(mr => mr.Delete(It.IsAny<FileInfo[]>(), It.IsAny<int>()))
                       .Verifiable();

            filehanlder.Setup(mr => mr.Override(It.IsAny<FileInfo[]>(), It.IsAny<int>(), It.IsAny<string>()))
                      .Verifiable();

            return filehanlder;
        }

    }
}
