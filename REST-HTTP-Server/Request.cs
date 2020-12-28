using System;
using System.Collections.Generic;
using System.Text;

namespace REST_HTTP_Server
{
    public class Request
    { 


        public String Type { get; set; }

        public String Order { get; set; }
        public String Version { get; set; }
        public String[] HeadRest{get; set;}
        

        public Request(String request)
        {
            if (String.IsNullOrEmpty(request))
            {
                Type = "";
                Order = "";
                Version = "";
                HeadRest[0] = "";

            }

            else
            {
                String[] tokens = request.Split("\n");
                String[] mymessage = tokens[0].Split(" ");
                Type = mymessage[0];
                Order = mymessage[1];
                Version = mymessage[2];
                HeadRest = tokens;
            }
        }
       






    }
}
