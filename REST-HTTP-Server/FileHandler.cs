using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace REST_HTTP_Server
{
    class FileHandler
    {

        //Create new message and new FileFolder if it does not exist yet
        public void NewMessage(string message) 
        {

            string path = "Files";
            Directory.CreateDirectory(path); //create new Directory

            //id name of new file by counting the number of files in the folder 
            int fileCount = Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly).Length;
            fileCount++;
            string newfilename = path +"/" +fileCount.ToString()+ ".txt";

            //creating file and write message in it
            using (StreamWriter sw = File.CreateText(newfilename))
            {
                sw.WriteLine(message);
            }

        }





    }
}
