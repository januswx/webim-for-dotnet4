using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webim
{
    public class WebimDao
    {
        //TODO: QUERY DATABASE OR CALL API
        public IEnumerable<WebimEndpoint> GetBuddiesByUid(long uid, int limit = 1000)
        {
            //TODO: STUB CODE
            WebimEndpoint ep1 = new WebimEndpoint("1", "uid:1", "user1");
            WebimEndpoint ep2 = new WebimEndpoint("2", "uid:2", "user2");
            return new List<WebimEndpoint>() {ep1, ep2};
        }

        //TODO: QUERY DATABASE OR CALL API
        public IEnumerable<WebimEndpoint> GetBuddiesByIds(IEnumerable<long> ids)
        {
            var list = from id in ids select new WebimEndpoint(id.ToString(), "uid:"+id, "user:"+id);
            return list;
        }

        //TODO: QUERY DATABASE OR CALL API
        public IEnumerable<WebimGroup> GetGroups(long uid, int limit = 100)
        { 
            WebimGroup g = new WebimGroup("1", "gid:1", "group1");
            g.AllCount = 0; //TODO:
            g.PicUrl = ""; //TODO
            g.Url = ""; //TOODs
            return new List<WebimGroup> { g };
        }

        internal WebimGroup GetGroup(long gid)
        {
            WebimGroup g = new WebimGroup("1", "gid:1", "group1");
            g.AllCount = 0; //TODO:
            g.PicUrl = ""; //TODO
            g.Url = ""; //TOODs
            return g;
        }
    }
}