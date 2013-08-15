using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webim
{
    public class WebimGroup : WebimObject
    {
        public WebimGroup(string id, string uri, string nick)
        {
            Id = id;
            Uri = uri;
            Nick = nick;
            Count = 0;
            AllCount = 0;
            Url = "";
            PicUrl = "";
        }
        public override Dictionary<string, object> feed(
            Dictionary<string, object> data)
        {
            data["id"] = Id;
            data["uri"] = Uri;
            data["nick"] = Nick;
            data["url"] = Url;
            data["pic_url"] = PicUrl;
            data["count"] = Count;
            data["all_count"] = AllCount;
            return data;
        }

        public string Id { get; set; }

        public string Uri { get; set; }

        public string Nick  { get; set; }

        public int Count { get; set; }

        public int AllCount { get; set; }

        public string Url { get; set; }

        public string PicUrl { get; set; }

    }
}