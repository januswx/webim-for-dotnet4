using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webim;

namespace Spacebuilder.Webim
{
    public class WebimService
    {

        private WebimDao webimDao = new WebimDao();

        private WebimHistoryDao historyDao = new WebimHistoryDao();

        private WebimSettingDao settingDao = new WebimSettingDao();

        public WebimService()
        { }

        //TODO: return current userid;
        public long CurrentUid()
        {
            return 1;
        }
        
        //TODO: you should read current user, and mapping to endpoint
        public WebimEndpoint CurrentEndpoint()
        { 
            WebimEndpoint ep = new WebimEndpoint(
                "1",
                "uid:1",
                "user1");
            ep.PicUrl = ""; //用户头像
            ep.Show = "available";
            ep.Url = ""; //用户空间
            ep.Status = ""; //用户状态
            return ep;
        }

        public WebimClient CurrentClient(string ticket = "")
        {
            WebimClient c = new WebimClient(
                CurrentEndpoint(),
                WebimConfig.DOMAIN,
                WebimConfig.APIKEY,
                WebimConfig.HOST,
                WebimConfig.PORT);
            c.Ticket = ticket;
            return c;
        }

        public IEnumerable<WebimEndpoint> GetBuddies(long uid)
        {
            //TODO: PERFORMANCE ISSUES
            return webimDao.GetBuddiesByUid(uid, 1000);
        }

        public IEnumerable<WebimGroup> GetGroups(long uid)
        {
            return webimDao.GetGroups(uid, 100);
        }

        //Groups
        public WebimGroup GetGroup(long gid)
        {
            return webimDao.GetGroup(gid);
        }

        //Offline
        public IEnumerable<WebimHistory> GetOfflineMessages(int uid)
        {
            return historyDao.GetOfflineMessages(uid);
        }

        public void OfflineMessageToHistory(int uid)
        {
            historyDao.OfflineMessageToHistory(uid);
        }

        public void InsertHistory(long uid, string offline, WebimMessage msg)
        {
            WebimHistory h = new WebimHistory();
            h.From = uid.ToString();
            h.Send = (offline == "true" ? 0 : 1);
            h.Nick = msg.Nick;
            h.Type = msg.Type;
            h.To = msg.To;
            h.Body = msg.Body;
            h.Style = msg.Style;
            h.Timestamp = msg.Timestamp;
            historyDao.Insert(h);
        }

        //Setting
        public string GetSetting(long uid)
        {
            return settingDao.Get(uid);
        }

        public void updateSetting(long uid, string data)
        {
            settingDao.Set(uid, data);
        }

        //History
        public IEnumerable<WebimHistory> GetHistories(long uid, string with, string type = "unicast")
        {
            return historyDao.GetHistories(uid, with, type);
        }

        public void ClearHistories(long uid, string with)
        {
            historyDao.ClearHistories(uid, with);
        }

        //Notifications
        public IEnumerable<WebimNotification> GetNotifications(long uid)
        {
            //TODO: unimplemented
            return new List<WebimNotification>();
        }

        //Menu
        public IEnumerable<WebimMenu> GetMenuList(long uid)
        {
            return new List<WebimMenu>();
        }

        public IEnumerable<WebimEndpoint> GetBuddiesByIds(IEnumerable<long> ids)
        {
            return webimDao.GetBuddiesByIds(ids);
        }
    }
}

