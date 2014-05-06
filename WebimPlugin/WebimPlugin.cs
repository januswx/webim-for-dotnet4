using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webim
{
    public class WebimPlugin
    {
       /**
        * API: current user
        *
        * 返回当前的Webim端点(用户)
 *
 * @return current user
 */

        public WebimEndpoint Endpoint()
        {
            // TODO: 应替换该代码，返回集成系统的当前用户。
            WebimEndpoint ep = new WebimEndpoint("1", "user1");
            ep.PicUrl = "/static/images/male.png";
            ep.Show = "available";
            ep.Url = "#";// 用户空间
            ep.Status = ""; // 用户状态
            return ep;

        }

        /**
         * API: Buddies of current user.
         *
         * Buddy:
         *
         *  id:         uid
         *  uid:        uid
         *  nick:       nick
         *  pic_url:    url of photo
         *  presence:   online | offline
         *  show:       available | unavailable | away | busy | hidden
         *  url:        url of home page of buddy
         *  status:     buddy status information
         *  group:      group of buddy

         * @param string current uid
         *
         * @return Buddy list
         *
         */
        public IEnumerable<WebimEndpoint> Buddies(string uid)
        {
            //TODO: STUB CODE
            List<WebimEndpoint> buddies = new List<WebimEndpoint>();
            WebimEndpoint e = new WebimEndpoint("1", "user1");
            e.PicUrl = "/static/images/male.png";
            buddies.Add(e);
            e = new WebimEndpoint("2", "user2");
            e.PicUrl = "/static/images/female.png";
            buddies.Add(e);
            return buddies;

        }

        /**
        * API: buddies by ids
        *
        * @param buddy id array
        *
        * @return Buddy list
        *
        */

        public IEnumerable<WebimEndpoint> BuddiesByIds(string uid, string[] ids)
        {

            List<WebimEndpoint> buddies = new List<WebimEndpoint>();
            WebimEndpoint e = new WebimEndpoint("1", "user1");
            e.PicUrl = "/static/images/male.png";
            buddies.Add(e);
            e = new WebimEndpoint("2", "user2");
            e.PicUrl = "/static/images/female.png";
            buddies.Add(e);
            return buddies;
        }

        /**
        * 根据roomId读取群组
        * 
        * @param roomId
        * @return WebimRoom
        */
        public WebimRoom findRoom(string roomId)
        {
            // TODO: 示例代码，需要替换
            if (roomId.Equals("room1"))
            {
                WebimRoom room = new WebimRoom("room1", "Room1");
                room.PicUrl = "/static/images/room.png";
                return room;
            }
            return null;
        }

        /**
         * API：rooms of current user
         *
         * @param uid
         *
         * @return rooms
         *
         * Room:
         *
         *  id:         Room ID,
         *  nick:       Room Nick
         *  url:        Home page of room
         *  pic_url:    Pic of Room
         *  status:     Room status
         *  count:      count of online members
         *  all_count:  count of all members
         *  blocked:    true | false
         */
        public IEnumerable<WebimRoom> Rooms(String uid)
        {
            // TODO: 示例代码，需要替换
            List<WebimRoom> rooms = new List<WebimRoom>();
            WebimRoom room = new WebimRoom("room1", "Room1");
            room.PicUrl = "/static/images/room.png";
            rooms.Add(room);
            return rooms;
        }

        /**
         * API: rooms by ids
         *
         * @param room id array
         *
         * @return rooms
         *
         * Room
         *
         */
        public IEnumerable<WebimRoom> RoomsByIds(String uid, String[] ids)
        {
            // TODO: 示例代码，需要替换
            List<WebimRoom> rooms = new List<WebimRoom>();
            WebimRoom room = new WebimRoom("room1", "Room1");
            room.PicUrl = "/static/images/room.png";
            rooms.Add(room);
            return rooms;
        }

        /**
         * API: members of room
         *
         * @param roomId
         * @return member list
         */
        public IEnumerable<WebimMember> Members(string roomId)
        {
            return new List<WebimMember> { 
                new WebimMember("1", "user1"), 
                new WebimMember("2", "user2") 
            };
        }

        /**
         * API: notifications of current user
         *
         * @return notification list
         *
         * Notification:
         *
         *  text: text
         *  link: link
         */
        public IEnumerable<WebimNotification> Notifications(string uid)
        {
            return new List<WebimNotification>() { new WebimNotification("通知", "#") };
        }


        /**
         * API: menu
         *
         * @return menu list
         *
         * Menu:
         *
         * icon
         * text
         * link
         */
        public IEnumerable<WebimMenu> Menu(string uid)
        {
            return new List<WebimMenu>();
        }

    }

}