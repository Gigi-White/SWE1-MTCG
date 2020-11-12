using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace REST_HTTP_Server
{
    public class FileHandler
    {

        //Create new message and new FileFolder if it does not exist yet
        public void NewMessage(string message, string filename)
        {
            using (StreamWriter sw = File.CreateText(filename))
            {
                sw.WriteLine(message);
            }

        }

        //Create the File info and give it back;
        public FileInfo[] GetFileInfo(string theWay)
        {
            DirectoryInfo directory = new DirectoryInfo(theWay);
            FileInfo[] files = directory.GetFiles("*.txt");
            return files;
        }

        //Get all messages form the files and put it in one string
        public string GetAllMessages(FileInfo[] files)
        {
            string body="";
            int number = 1;
            foreach (var item in files)
            {
                body += number.ToString() + ". Message:\n" + File.ReadAllText(item.ToString()) + "\n";
                number++;
            }
            return body; 
        }
        public string GetOneMessage(FileInfo[] files, int number)
        {
            string body = "";
            body += number.ToString() + ". Message:\n" + File.ReadAllText(files[number - 1].ToString()) + "\n";
            return body;
        }

        public void Delete(FileInfo[] files, int number) 
        {
            File.Delete(files[number - 1].ToString());
        }

        public void Override(FileInfo[] files, int number, string message) 
        {
            File.WriteAllText(files[number - 1].ToString(), message);
        }






    }
}
