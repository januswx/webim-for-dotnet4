using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webim
{
    public class WebimModel
    {

        /**
         * 读取与with用户聊天记录，查询逻辑:<br>
         * 
         * <pre>
         *     if (type == "chat")
         *       {
         *           
         *           "SELECT * FROM webim_Histories WHERE `type` = 'chat' 
         *           AND ((`to`=%s AND `from`=%s AND `fromdel` != 1) 
         *           OR (`send` = 1 AND `from`=%s AND `to`=%s AND `todel` != 1))  
         *           ORDER BY timestamp DESC LIMIT %d", $with, $uid, $with, $uid, $limit );
         *           
         *       }
         *       else
         *       {
         *           
         *           "SELECT * FROM  webim_histories 
         *               WHERE `to`=%s AND `type`='grpchat' AND send = 1 
         *               ORDER BY timestamp DESC LIMIT %d", , $with, $limit);
         *           
         *       }
         * </pre>
         * 
         * @param uid
         *            当前用户id
         * @param with
         *            对方id，可根据需要转换为long
         * @param type
         *            记录类型：chat | grpchat
         *            
         * @param limit
         * 			  记录条数
         * @return 聊天记录
         */
        public IEnumerable<WebimHistory> Histories(string uid, string with, string type = "chat", int limit = 50)
        {
            return new List<WebimHistory>();
        }

        /**
         * 读取用户的离线消息，SQL脚本:<br>
         * 
         * "SELECT * FROM  webim_histories WHERE `to` = ? and send != 1 ORDER BY timestamp DESC LIMIT %d"
         * , limit;
         * 
         * @param uid
         *            用户uid
         * @return 返回离线消息
         */
        public IEnumerable<WebimHistory> OfflineHistories(string uid, int limit = 50)
        {
            //TODO:
            return new List<WebimHistory>();
        }

        public void InsertHistory(string uid, WebimMessage msg)
        {
            // TODO Auto-generated method stub
        }


        /**
         * 清除与with用户聊天记录，SQL脚本:<br>
         * 
         * "UPDATE webim_histories SET fromdel = 1 Where from = @0 and to = @1 and type = 'chat'"
         * <br>
         * "UPDATE webim_histories SET todel = 1 Where to = @0 and from = @1 and type = 'chat'"
         * <br>
         * "DELETE FROM webim_histories WHERE fromdel = 1 AND todel = 1"
         * 
         * @param uid
         *            用户uid
         * @param with
         *            对方id,可根据需要转换为long
         */
        public void ClearHistories(string uid, string with)
        {
            //TODO: clear histories
        }

        /**
         * 离线消息转换为历史消息，SQL脚本:<br>
         * 
         * "UPDATE webim_histories SET send = 1 where to = ? and send = 0");
         * 
         * @param uid
         *            用户uid
         */
        public void OfflineHistoriesReaded(string uid)
        {
            //TODO:
        }


        /*
         * CREATE TABLE webim_settings(
         *   `Id` mediumint(8) unsigned NOT NULL AUTO_INCREMENT,
         *   `Uid` bigint unsigned NOT NULL,
         *   `Data` text,
         *   `CreatedAt` date DEFAULT NULL,
         *   `UpdatedAt` date DEFAULT NULL,
         *   PRIMARY KEY (`id`)
         * );
         */


        /**
         * 读取用户配置数据。<br>
         * 
         * <ol>
         * <li>数据库查询SQL脚本："select data from webim_settings where uid = ?", uid</li>
         * <li>如果data为空，返回: "{}"</li>
         * </ol>
         * 
         * @param uid
         *            用户uid
         * @return 配置数据，JSON格式
         */
        public string GetSetting(string uid)
        {
            return "{}";
        }

        /**
         * 设置用户配置数据。<br>
         * 
         * <ol>
         * <li>数据库SQL脚本: "update webim_settings set data =@0  where uid = @1", uid,
         * data</li>
         * <li>应该先读取配置检查是否存在，如不存在插入，存在更新。</li>
         * </ol>
         * 
         * @param uid
         *            用户uid
         * @param data
         *            配置数据，JSON格式
         */
        public void SaveSetting(string uid, string data)
        {
            //TODO: DEMO CODE
        }

        /**
         * 根据roomId读取临时讨论组
         * 
         * @param roomId
         * @return 临时讨论组
         */
        public WebimRoom FindRoom(string roomId)
        {
            return null;
        }

        /**
         * 读取当前用户的临时讨论组
         * 
         * @param uid
         * 
         * @return 群组列表
         */
        public IEnumerable<WebimRoom> Rooms(string uid)
        {
            //TODO:
            return new List<WebimRoom>();
        }

        /**
         * 根据临时讨论组id，读取临时讨论组列表
         * 
         * @param uid 用户UID
         * @param ids 临时讨论组Id列表
         * 
         * @return 群组列表
         */
        public IEnumerable<WebimRoom> RoomsByIds(string uid, string[] ids)
        {
            //TODO:
            return new List<WebimRoom>();
        }

        /**
         * 读取临时讨论组成员列表
         * 
         * @param room 临时讨论组ID
         * @return 成员列表
         */
        public IEnumerable<WebimMember> Members(string room)
        {
            return new List<WebimMember>();
        }

        /**
         * 创建临时讨论组
         * 
         * @param owner
         * @param name
         * @param nick
         */
        public WebimRoom CreateRoom(string owner, string name, string nick)
        {
            //TODO: insert into webim_rooms table
            return new WebimRoom(name, nick);
        }

        /**
         * 邀请成员加入临时讨论组
         * 
         * @param roomId 讨论组name
         * @param members 成员列表
         */
        public void InviteRoom(string roomId, IEnumerable<WebimEndpoint> members)
        {
            //TODO: invite members to room
        }

        /**
         * 加入临时讨论组
         * 
         * @param room 讨论组name
         * @param uid
         * @param nick
         */
        public void JoinRoom(string room, string uid, string nick)
        {
            //TODO: 
        }

        /**
         * 离开讨论组
         * 
         * @param room
         * @param uid
         */
        public void LeaveRoom(string room, string uid)
        {
            //TODO: 
        }

        /**
         * 屏蔽讨论组
         * 
         * @param room
         * @param uid
         */
        public void BlockRoom(string room, string uid)
        {
            //TODO:
        }

        /**
         * 解除屏蔽
         * 
         * @param room
         * @param uid
         */
        public void UnblockRoom(string room, string uid)
        {
            //TODO:
        }

        /**
         * 讨论组是否屏蔽
         * 
         * @param room
         * @param uid
         * 
         * @return is blocked
         */
        public bool IsRoomBlocked(string room, string uid)
        {
            //TODO:
            return false;
        }

        /**
         * 读取访客
         * 
         * @return
         */
        WebimEndpoint Visitor(string vid)
        {
            //TODO: should be from database
            return new WebimEndpoint(vid, vid);
        }

        /**
         * 保存访客
         * 
         * @return
         */
        WebimEndpoint InsertVisitor()
        {
            return new WebimEndpoint("vid:1", "visitor1");
        }

        /**
         * 根据id列表读取访客列表
         * 
         * @param vids
         * @return
         */
        IEnumerable<WebimEndpoint> Visitors(string[] vids)
        {
            return new List<WebimEndpoint>();
        }
	

    }
}