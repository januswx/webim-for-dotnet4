using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webim
{
    public class WebimHistoryDao
    {
        //TODO: INSERT INTO DATABASE
        public void Insert(WebimHistory h)
        {
            return;
        }

        //TODO: QUERY DATABASE
        public IEnumerable<WebimHistory> GetHistories(long uid, string with, string type = "unicast", int limit = 30)
        {
            if (type == "unicast")
            {
                /*
                "SELECT * FROM webim_Histories WHERE `type` = 'unicast' 
                AND ((`to`=%s AND `from`=%s AND `fromdel` != 1) 
                OR (`send` = 1 AND `from`=%s AND `to`=%s AND `todel` != 1))  
                ORDER BY timestamp DESC LIMIT %d", $with, $uid, $with, $uid, $limit );
                */
            }
            else
            {
                /*
                "SELECT * FROM  spb_Webim_Histories 
                    WHERE `to`=%s AND `type`='multicast' AND send = 1 
                    ORDER BY timestamp DESC LIMIT %d", , $with, $limit);
                */
            }
            return new List<WebimHistory>();
        }

        //TODO: QUERY DATABASE
        public void ClearHistories(long uid, string with)
        {
            //"UPDATE spb_Webim_Histories SET fromdel = 1 Where from = @0 and to = @1 and type = 'unicast'"
            //"UPDATE spb_Webim_Histories SET todel = 1 Where to = @0 and from = @1 and type = 'unicast'"
            //"DELETE FROM spb_Webim_Histories WHERE fromdel = 1 AND todel = 1"
        }

        //TODO: QUERY DATABASE
        public IEnumerable<WebimHistory> GetOfflineMessages(long uid, int limit = 50)
        {
            //"SELECT * FROM  spb_Webim_Histories WHERE `to` = ? and send != 1 ORDER BY timestamp DESC LIMIT %d", limit;

            return new List<WebimHistory>();
        }

        //TODO: QUERY DATABASE
        public void OfflineMessageToHistory(long uid)
        {
            //"UPDATE spb_Webim_Histories SET send = 1 where to = ? and send = 0");
        }

 
    }
}