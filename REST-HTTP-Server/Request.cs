using System;
using System.Collections.Generic;
using System.Text;

namespace REST_HTTP_Server
{
    class Request
    { 


        public String Type { get; set; }

        public String Order { get; set; }
        public String Version { get; set; }
        public String[] HeadRest{get; set;}
        

        public Request(String type, String order, String version, String[] headRest)
        {
            Type = type;
            Order = order;
            Version = version;
            HeadRest = headRest;         

        }
        //Get the sweet sweet Request infos----------------------------------
        public static Request GetRequest(String request) 
        {
            if( String.IsNullOrEmpty(request))
            {
                return null;
            }

            String[] tokens = request.Split("\n");
            String [] mymessage = tokens[0].Split(" ");
            String type = mymessage[0];
            String order = mymessage[1];
            String url = mymessage[2];
            String[] headRest = tokens;
            
            
            
            return new Request(type, order, url, headRest);

        }

      

      
        


    }
}
