using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Json;

namespace Webim
{
    public class WebimClient
    {
        private int port;
        private string domain;
        private string apikey;
        private string host;
        private string ticket = "";
        private WebimEndpoint ep;

        public WebimClient(WebimEndpoint ep, string domain, string apikey, string host, int port)
        {
            this.ep = ep;
            this.host = host;
            this.port = port;
            this.domain = domain;
            this.apikey = apikey;
        }

        public WebimEndpoint Endpoint
        {
            get { return ep; }
            set { ep = value; }
        }

        public string Ticket
        {
            get { return ticket; }
            set { ticket = value; }
        }

        public string Domain
        {
            get { return domain; }
        }

        public JsonObject Online(IEnumerable<string> buddies, IEnumerable<string> groups)
        {
            Dictionary<string, object> data = NewData();
            data.Add("groups", this.ListJoin(",", groups));
            data.Add("buddies", this.ListJoin(",", buddies));
            data.Add("uri", ep.Uri);
            data.Add("id", ep.Id);
            data.Add("name", ep.Id);
            data.Add("nick", ep.Nick);
            data.Add("status", ep.Status);
            data.Add("show", ep.Show);
            try
            {
                JsonObject json = HttpPost("/presences/online", data);
                this.ticket = (string)json["ticket"];
                return json;
            }
            catch (System.Exception e)
            {
                throw new WebimException(500, e.Message);
            }
        }


        /**
        * User Offline
        *
        * @return JsonObject "{'status': 'ok'}" or "{'status': 'error', 'message': 'blabla'}"
        * @throws WebIMException
        */
        public JsonObject Offline()
        {
            Dictionary<string, object> data = NewData();
            try
            {
                return HttpPost("/presences/offline", data);
            }
            catch (Exception e)
            {
                throw new WebimException(500, e.Message);
            }
        }

        /**
        * Publish updated presence.
        *
        * @param presence
        * @return JsonObject "{'status': 'ok'}" or "{'status': 'error', 'message': 'blabla'}"
        * @throws WebIMException
        */
        public JsonObject Publish(WebimPresence presence)
        {
            Dictionary<string, object> data = NewData();
            data.Add("nick", ep.Nick);
            presence.feed(data);
            try
            {
                return HttpPost("/presences/show", data);
            }
            catch (Exception e)
            {
                throw new WebimException(500, e.Message);
            }
        }

        /**
        * Publish status
        * @param status
        * @return JsonObject "{'status': 'ok'}" or "{'status': 'error', 'message': 'blabla'}"
        * @throws WebIMException
        */
        public JsonObject Publish(WebimStatus status)
        {
            Dictionary<string, object> data = NewData();
            data.Add("nick", ep.Nick);
            status.feed(data);
            try
            {
                return HttpPost("/statuses", data);
            }
            catch (Exception e)
            {
                throw new WebimException(500, e.Message);
            }
        }

        /**
        * Publish Message
        * @param message
        * @return JsonObject "{'status': 'ok'}" or "{'status': 'error', 'message': 'blabla'}"
        * @throws WebIMException
        */
        public JsonObject Publish(WebimMessage message)
        {
            Dictionary<string, object> data = NewData();
            message.feed(data);
            try
            {
                return HttpPost("/messages", data);
            }
            catch (Exception e)
            {
                throw new WebimException(500, e.Message);
            }
        }

        /**
        * Get group members
        * @param grpid
        * @return member list
        * @throws WebIMException
        */
        public JsonObject Members(string grpid)
        {

            Dictionary<string, object> data = NewData();
            data.Add("group", grpid);
            try
            {
                return HttpGet("/group/members", data);
            }
            catch (Exception e)
            {
                throw new WebimException(500, e.Message);
            }
        }

        /**
        * Join Group
        * @param grpid
        * @return JsonObject "{'id': 'grpid', 'count': '0'}"
        * @throws WebIMException
        */
        public JsonObject Join(string grpid)
        {
            Dictionary<string, object> data = NewData();
            data.Add("nick", ep.Nick);
            data.Add("group", grpid);
            try
            {
                JsonObject respObj = HttpPost("/group/join", data);
                JsonObject rtObj = new JsonObject();
                rtObj.Add("id", grpid);
                rtObj.Add("count", (int)respObj.GetValue(grpid));
                return rtObj;
            }
            catch (Exception e)
            {
                throw new WebimException(500, e.Message);
            }
        }

        /**
        * Leave Group
        * @param grpid
        * @return JsonObject "{'status': 'ok'}" or "{'status': 'error', 'message': 'blabla'}"
        * @throws WebIMException
        */
        public JsonObject Leave(string grpid)
        {
            Dictionary<string, object> data = NewData();
            data.Add("nick", ep.Nick);
            data.Add("group", grpid);
            try
            {
                return HttpPost("/group/leave", data);
            }
            catch (Exception e)
            {
                throw new WebimException(500, e.Message);
            }
        }

        private JsonObject HttpGet(string path, Dictionary<string, object> parameters)
        {
            String url = this.ApiUrl(path);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(url + "?" + UrlEncode(parameters)).Result;
            response.EnsureSuccessStatusCode();
            string content = response.Content.ReadAsStringAsync().Result;
            return (JsonObject)JsonObject.Parse(content);
        }

        private string UrlEncode(Dictionary<string, object> parameters)
        {
            List<string> l = new List<string>();
            foreach (KeyValuePair<string, object> p in parameters)
            {
                //TODO: FIXME Later
                l.Add(p.Key + "=" + Uri.EscapeUriString(p.Value.ToString()));
            }
            return string.Join("&", l.ToArray());
        }

        private JsonObject HttpPost(string path, Dictionary<string, object> data)
        {
            String url = this.ApiUrl(path);
            HttpClient client = new HttpClient();
            Dictionary<string, string> formData = new Dictionary<string, string>();
            foreach(KeyValuePair<string, object> kv in data)
            {
                formData[kv.Key] = kv.Value.ToString();
            }
            HttpResponseMessage response = client.PostAsync(url, 
                new FormUrlEncodedContent(formData.AsEnumerable())).Result;
            response.EnsureSuccessStatusCode();
            string content = response.Content.ReadAsStringAsync().Result;
            return (JsonObject)JsonObject.Parse(content);
        }

        private Dictionary<string, object> NewData()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("version", "v4");
            data.Add("domain", Domain);
            data.Add("apikey", apikey);
            data.Add("ticket", Ticket);
            return data;
        }

        private string ListJoin(string sep, IEnumerable<string> list)
        {
            bool first = true;
            StringBuilder sb = new StringBuilder();
            foreach (string g in list)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    sb.Append(sep);
                }
                sb.Append(g);

            }
            return sb.ToString();
        }

        private string ApiUrl(string path)
        {
            if (!path.StartsWith("/"))
            {
                path = "/" + path;
            }
            return "http://" + host + ":" + port.ToString() + "/v4" + path;
        }

    }

}