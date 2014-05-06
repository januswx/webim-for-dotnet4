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
        public List<WebimEndpoint> Buddies(string uid)
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
 * API: buddies by ids
 *
 * @param buddy id array
 *
 * @return Buddy list
 *
 */

        List<WebimEndpoint> BuddiesByIds(string uid, string[] ids)
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
 * @return 群组
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
        public List<WebimRoom> Rooms(String uid)
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
        public List<WebimRoom> RoomsByIds(String uid, String[] ids)
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
        public List<WebimMember> Members(string roomId)
        {
            List<WebimMember> members = new List<WebimMember>();
            members.Add(new WebimMember("1", "user1"));
            members.Add(new WebimMember("2", "user2"));
            return members;
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
        public List<WebimNotification> Notifications(string uid)
        {
            List<WebimNotification> notifications = new List<WebimNotification>();
            notifications.Add(new WebimNotification("通知", "#"));
            return notifications;
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
        public List<WebimMenu> Menu(string uid)
        {
            return new List<WebimMenu>();
        }

    }

}