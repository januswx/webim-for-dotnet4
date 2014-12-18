using System;
using System.Collections.Generic;

namespace Webim
{
    //TODO: Should be in DB.

    /**
     *
     * 测试的配置文件，配置消息服务器的地址、端口、通信DOMAIN、APIKEY。<br>
     * 
     * TODO: 正式项目配置应写在XML文件或者数据库中。
     *
     * @author erylee <ery.lee at gmail.com>
     *
     */
    public class WebimConfig
    {

        /**
         * Webim库版本
         */
        public static string VERSION = "5.4.1";

        /**
         * 是否开启
         */
        public static bool ISOPEN = true;

        /**
         * 站点域名
         */
        public static string DOMAIN = "dotnet.webim20.cn";

        /**
         * 通信APIKEY
         */
        public static string APIKEY = "460cf5d46be2c04e";

        /**
         * 消息服务器地址
         */
        public static string HOST = "t.nextalk.im";

        /**
         * 消息服务器端口
         */
        public static int PORT = 8000;

        /**
         * 界面Theme
         */
        public static string THEME = "base";

        /**
         * 本地语言
         */
        public static string LOCAL = "zh-CN";

        /**
         * 表情库
         */
        public static string EMOT = "default";

        /**
         * 工具条透明度
         */
        public static int OPACITY = 80;

        /**
         * 群组聊天
         */
        public static bool ENABLE_ROOM = true;

        /**
         * 临时讨论组
         */
        public static bool ENABLE_DISCUSSION = true;

        /**
         * 显示通知按钮
         */
        public static bool ENABALE_NOTI = true;

        /**
         * 支持快捷工具栏
         */
        public static bool ENABLE_SHORTCUT = false;

        /**
         * 支持聊天链接
         */
        public static bool ENABLE_CHATLINK = false;

        /**
         * 显示菜单栏
         */
        public static bool ENABLE_MENU = false;

        /**
         * 显示不在线好友
         */
        public static bool SHOW_UNAVAILABLE = false;

        /**
         * 支持访客
         */
        public static bool ENABLE_VISITOR = false;

        /**
         * 支持文件上传
         */
        public static bool ENABLE_UPLOAD = false;


    }

}
