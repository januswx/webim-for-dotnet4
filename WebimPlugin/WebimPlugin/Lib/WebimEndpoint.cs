using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webim
{
    /*
     * Webim User
     * vid:001
     * uid:19919
     */
    public class WebimEndpoint : WebimObject
    {

        /*
         * URI Examples:
         * 
         * vid:001
         * uid:19398
         * sid:echo
         */
        public WebimEndpoint(string id, string uri, string nick)
        {
            Id = id;
            Uri = uri;
            Nick = nick;
            Show = "available";
            Status = "Online";
            StatusTime = "";
            Url = "";
            PicUrl = "";
        }

        public override Dictionary<string, object>
            feed(Dictionary<string, object> data)
        {
            data["id"] = Id;
            data["uri"] = Uri;
            data["nick"] = Nick;
            data["show"] = Show;
            data["status"] = Status;
            data["status_time"] = StatusTime;
            data["url"] = Url;
            data["pic_url"] = PicUrl;
            return data;
        }

        public string Id { get; set; }

        public string Uri { get; set; }

        public string Nick { get; set; }

        public string Show { get; set; }

        public string Status { get; set; }

        public string StatusTime { get; set; }

        public string Url { get; set; }

        public string PicUrl { get; set; }

    }
}