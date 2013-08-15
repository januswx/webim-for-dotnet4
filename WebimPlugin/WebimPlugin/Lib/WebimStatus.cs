using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webim
{
    public class WebimStatus : WebimObject
    {

        public WebimStatus(string to, string show, string status)
        {
            To = to;
            Show = show;
            Status = status;
        }

        public override Dictionary<string, object> feed(
            Dictionary<string, object> data)
        {
            data.Add("to", To);
            data.Add("show", Show);
            data.Add("status", Status);
            return data;
        }

        public string To { get; set; }
        public string Show { get; set; }
        public string Status { get; set; }
    }
}