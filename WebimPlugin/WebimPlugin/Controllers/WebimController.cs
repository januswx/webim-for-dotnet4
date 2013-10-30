﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Json;
using Webim;

namespace Webim.Controllers
{
    public class WebimController : Controller
    {

        //TODO: There should be userService, groupService and FollowService

        private WebimService webimService = new WebimService();

        // GET: /Webim/Index
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Webim/Run
        [HttpGet]
        public ActionResult Run()
        {
			long uid = webimService.CurrentUid();
            string setting = webimService.GetSetting(uid);
            string body = string.Format(@"var _IMC = {{
	            production_name: 'dotnet',
	            version: '1.0',
	            path: '{0}',
	            uiPath: '{1}',
	            is_login: true,
	            user: '',
	            setting: '',
	            menu: '',
				disable_room: false,
				disable_noti: false,
	            disable_chatlink: true,
	            enable_shortcut: '',
	            disable_menu: 'true',
	            theme: 'base',
	            local: 'zh-CN',
                aspx: false,
	            min: """" //window.location.href.indexOf(""webim_debug"") != -1 ? """" : "".min""
            }};
            
            _IMC.script = window.webim ? '' : ('<link href=""' + _IMC.uiPath + 'static/webim.'+ _IMC.production_name + _IMC.min + '.css?' + _IMC.version + '"" media=""all"" type=""text/css"" rel=""stylesheet""/><link href=""' + _IMC.uiPath + 'static/themes/' + _IMC.theme + '/jquery.ui.theme.css?' + _IMC.version + '"" media=""all"" type=""text/css"" rel=""stylesheet""/><script src=""' + _IMC.uiPath + 'static/webim.' + _IMC.production_name + _IMC.min + '.js?' + _IMC.version + '"" type=""text/javascript""></script><script src=""' + _IMC.uiPath + 'static/i18n/webim-' + _IMC.local + '.js?' + _IMC.version + '"" type=""text/javascript""></script>');
            _IMC.script += '<script src=""' + _IMC.uiPath + 'webim.js?' + _IMC.version + '"" type=""text/javascript""></script>';
            document.write( _IMC.script );

            ", ("/Webim/"), ("/UI/"));

            return Content(body, "text/javascript");
        }

        // POST: /Webim/Online
        [HttpPost]
        public ActionResult Online()
        {
            //当前用户登录
            long uid = webimService.CurrentUid();
            IEnumerable<WebimEndpoint> buddies = webimService.GetBuddies(uid);
            IEnumerable<WebimGroup> groups = webimService.GetGroups(uid);
            //Forward Online to IM Server
            WebimClient client = webimService.CurrentClient();
            var buddyIds = from b in buddies select b.Id;
            var groupIds = from g in groups select g.Id;
            try
            {
                JsonObject json = client.Online(buddyIds, groupIds);
                Debug.WriteLine(json.ToString());

                Dictionary<string, string> conn = new Dictionary<string, string>();
                conn.Add("ticket", (string)json["ticket"]);
                conn.Add("domain", client.Domain);
                conn.Add("jsonpd", (string)json["jsonpd"]);
                conn.Add("server", (string)json["jsonpd"]);
                conn.Add("websocket", (string)json["websocket"]);

                //Online Buddies
                Dictionary<string, WebimEndpoint> buddyDict = new Dictionary<string, WebimEndpoint>();
                foreach (WebimEndpoint e in buddies)
                {
                    buddyDict[e.Id] = e;
                }
                List<WebimEndpoint> onlines = new List<WebimEndpoint>();
                foreach (JsonValue v in (JsonArray)json["buddies"])
                {
                    JsonObject o = (JsonObject)v;
                    onlines.Add(buddyDict[(string)o["name"]]);
                }

                //Groups with count
                Dictionary<string, WebimGroup> groupDict = new Dictionary<string, WebimGroup>();
                foreach (WebimGroup g in groups)
                {
                    groupDict[g.Id] = g;
                }
                List<WebimGroup> groups1 = new List<WebimGroup>();
                foreach (JsonValue v in (JsonArray)json["groups"])
                {
                    JsonObject o = (JsonObject)v;
                    string gid = (string)o["name"];
                    WebimGroup group = groupDict[gid];
                    group.Count = (int)o["total"];
                    groups1.Add(group);
                }

                //{"success":true,
                // "connection":{
                // "ticket":"fcc493f7a7b17cfadbf4|admin",
                // "domain":"webim20.cn",
                // "server":"http:\/\/webim20.cn:8000\/packets"},
                // "buddies":[
                //           {"uid":"5","id":"demo","nick":"demo","group":"stranger","url":"home.php?mod=space&uid=5","pic_url":"picurl","status":"","presence":"online","show":"available"}],
                // "rooms":[],
                // "server_time":1370751451399.4,
                // "user":{"uid":"1","id":"admin","nick":"admin","pic_url":"pickurl","show":"available","url":"home.php?mod=space&uid=1","status":""},
                // "new_messages":[]}


                var buddyArray = (from b in onlines select b.Data()).ToArray();
                var groupArray = (from g in groups1 select g.Data()).ToArray();
                return Json(new
                {
                    success = true,
                    connection = conn,
                    buddies = buddyArray,
                    groups = groupArray,
                    rooms = groupArray,
                    server_time = Timestamp(),
                    user = client.Endpoint.Data()
                }, JsonRequestBehavior.AllowGet);

            }
            catch (WebimException)
            {
                return Json(
                    new { success = false, error_msg = "IM Server is not found" },
                    JsonRequestBehavior.AllowGet
                );
            }

        }

        // POST: /Webim/Offline
        [HttpPost]
        public ActionResult Offline()
        {
            WebimClient c = webimService.CurrentClient(Request["ticket"]);
            c.Offline();
            return Json("ok");
        }

        //POST: /Webim/Message
        [HttpPost]
        public ActionResult Message()
        {
            long uid = webimService.CurrentUid();
            WebimClient c = webimService.CurrentClient(Request["ticket"]);
            string type = Request["type"];
            string offline = Request["offline"];
            string to = Request["to"];
            string body = Request["body"];
            string style = Request["style"];
            if (style == null) { style = ""; }
            WebimMessage msg = new WebimMessage(type, to, c.Endpoint.Nick, body, style, Timestamp());
            c.Publish(msg);
            webimService.InsertHistory(uid, offline, msg);
            return Json("ok");
        }

        //POST: /Webim/Presence
        [HttpPost]
        public ActionResult Presence()
        {
            WebimClient c = webimService.CurrentClient(Request["ticket"]);
            string show = Request["show"];
            string status = Request["status"];
            c.Publish(new WebimPresence(show, status));
            return Json("ok");
        }

        //POST: /Webim/Status
        [HttpPost]
        public ActionResult Status()
        {
            WebimClient c = webimService.CurrentClient(Request["ticket"]);
            string to = Request["to"];
            string show = Request["show"];
            string status = Request["status"];
            if (status == null) { status = ""; }
            WebimStatus s = new WebimStatus(to, show, status);
            c.Publish(s);
            return Json("ok");
        }

        //POST: /Webim/Refresh
        [HttpPost]
        public ActionResult Refresh()
        {
            WebimClient c = webimService.CurrentClient(Request["ticket"]);
            c.Offline();
            return Json("ok");
        }

        //POST: /Webim/Setting
        [HttpPost]
        public ActionResult Setting()
        {
            long uid = webimService.CurrentUid();
            string data = Request["data"];
            webimService.updateSetting(uid, data);
            return Json("ok");
        }

        //GET: /Webim/History
        [HttpGet]
        public ActionResult History()
        {
            long uid = webimService.CurrentUid();
            string id = Request["id"];
            string type = Request["type"];
            IEnumerable<WebimHistory> histories = webimService.GetHistories(uid, id, type);
            var list = from h in histories select h.Data();
            return Json(list.ToArray(), JsonRequestBehavior.AllowGet);
        }

        //POST: /Webim/ClearHistory
        public ActionResult ClearHistory()
        {
            long uid = webimService.CurrentUid();
            string id = Request["id"];
            webimService.ClearHistories(uid, id);
            return Json("ok");

        }

        //GET: /Webim/DownloadHistory
        [HttpGet]
        public ActionResult DownloadHistory()
        {
            long uid = webimService.CurrentUid();
            string id = Request["id"];
            string type = Request["type"];
            IEnumerable<WebimHistory> histories = webimService.GetHistories(uid, id, type);
            return View(histories);
        }


        //GET: /Webim/Members
        [HttpGet]
        public ActionResult Members()
        {
            WebimClient c = webimService.CurrentClient(Request["ticket"]);
            string gid = Request["id"];
            JsonObject obj = c.Members(gid);
            List<Dictionary<string,string>> list = new List<Dictionary<string,string>>();
            foreach (JsonObject m in (JsonArray)obj[gid])
            { 
                Dictionary<string,string> data = new Dictionary<string,string>();
                data["id"] = (string)m["id"];
                data["nick"] = (string)m["nick"];
                list.Add(data);
            }
            return Json(list.ToArray(), JsonRequestBehavior.AllowGet);

        }

        //POST: /Webim/Join
        [HttpPost]
        public ActionResult Join()
        {
            WebimClient c = webimService.CurrentClient(Request["ticket"]);
            string gid = Request["id"];
            JsonObject o = c.Join(gid);
            return Content(o.ToString(), "text/json");
        }

        //POST: /Webim/Leave
        [HttpPost]
        public ActionResult Leave()
        {
            WebimClient c = webimService.CurrentClient(Request["ticket"]);
            c.Leave(Request["id"]);
            return Json("ok");
        }

        //GET: /Webim/Buddies
        //TODO: SECURITY BUGS!!!
        [HttpGet]
        public ActionResult Buddies()
        {
            IEnumerable<long> ids = (from id in
                                         Request["ids"].Split(new char[1] { ',' })
                                     select long.Parse(id));
            IEnumerable<WebimEndpoint> buddies = webimService.GetBuddiesByIds(ids);
            var list = from buddy in buddies select buddy.Data();
            return Json(list.ToArray(), JsonRequestBehavior.AllowGet);
        }

        //GET: /Webim/Notifications
        [HttpGet]
        public ActionResult Notifications()
        {
            long uid = webimService.CurrentUid();
            return Json(webimService.GetNotifications(uid), JsonRequestBehavior.AllowGet);
        }

        //GET: /Webim/Menus
        [HttpGet]
        public ActionResult Menus()
        {
            long uid = webimService.CurrentUid();
            return Json(webimService.GetMenuList(uid), JsonRequestBehavior.AllowGet);
        }

        private double Timestamp()
       {
            return (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds;
        }
    }
}
