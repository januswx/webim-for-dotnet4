using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webim
{
    /*
     * Webim Message
     */
    public class WebimMessage : WebimObject
    {

        public WebimMessage(string type, string to, string nick, string body, string style, double timestamp)
        {
            Type = type;
            To = to;
            Nick = nick;
            Body = body;
            Style = style;
            Timestamp = timestamp;
        }

        public override Dictionary<string, object> feed(
    Dictionary<string, object> data)
        {
            data.Add("type", Type);
            data.Add("to", To);
            data.Add("nick", Nick);
            data.Add("body", Body);
            data.Add("style", Style);
            data.Add("timestamp", Timestamp);
            return data;
        }


        public string Type { get; set; }

        public string Nick { get; set; }
       
        public string To { get; set; }

        public string Body { get; set; }

        public string Style { get; set; }

        public double Timestamp { get; set; }

    }
}