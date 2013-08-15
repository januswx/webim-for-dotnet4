using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webim
{
    public class WebimPresence : WebimObject
    {

        public WebimPresence(string show, string status)
        {
            Show = show;
            Status = status;
        }

        public override Dictionary<string, object> feed(
            Dictionary<string, object> data)
        {
            data.Add("show", Show);
            data.Add("status", Status);
            return data;
        }

        public string Show { get; set; }

        public string Status { get; set; }

    }
}