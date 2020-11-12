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
    [TestFixture]
    public class ServerTests
    {
        //#################HTTP ServerTests###########################################
        [Test] // Test if the function ReadStream gets the right text from the StreamReader
        public void ReadStreamTest()
        {

           

            //arrange
            var myStream = new Mock<ITcpClient>();

            //create my new StreamReader with my own text
            string testString = "GET /messages HTTP/1.1";
            UTF8Encoding encoding = new UTF8Encoding();
            UnicodeEncoding uniEncoding = new UnicodeEncoding();

            byte[] testArray = encoding.GetBytes(testString);

            MemoryStream ms = new MemoryStream(testArray);

            StreamReader sr = new StreamReader(ms);
            //mock the GetStreamReader Function to return my selfmade Streamreader
            myStream
                .Setup(c => c.GetStreamReader())
                .Returns(sr);

            //act
            HTTPServer myServer = new HTTPServer(myStream.Object);
            string actual = myServer.ReadStream();

            //assert
            Assert.AreEqual(testString, actual);
        
        }

        //##########################################################################


        //#####################Request Tests########################################

        [Test]  //Check if Request Constructor creates Request Object with the right values
        public void RequestTest()
        {
            //arrange
            string testMessage = "GET /messages HTTP/1.1\n" +
                "User-Agent: PostmanRuntime/7.26.8\n" +
                "Postman-Token: 6eb0f280-6486-402c-a24f-8a7cf1ffabf9\n" +
                "Host: localhost:8080";

            string testType = "GET";
            string testOrder = "/messages";
            string testVersion = "HTTP/1.1";
            string[] testHeadRest = { "GET /messages HTTP/1.1", "User-Agent: PostmanRuntime/7.26.8", 
                "Postman-Token: 6eb0f280-6486-402c-a24f-8a7cf1ffabf9", "Host: localhost:8080" };


            //act
            Request actualRequest = new Request(testMessage);
            //assert
            Assert.AreEqual(testType, actualRequest.Type);
            Assert.AreEqual(testOrder, actualRequest.Order);
            Assert.AreEqual(testVersion, actualRequest.Version);
            Assert.AreEqual(testHeadRest.ToList(), actualRequest.HeadRest);
        }

        //############################################################################################

        //####################################Response Tests##########################################
    }
}