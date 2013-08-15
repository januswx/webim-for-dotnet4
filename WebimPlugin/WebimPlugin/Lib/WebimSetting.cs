using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/*
 * CREATE TABLE webim_settings(
 *   `id` mediumint(8) unsigned NOT NULL AUTO_INCREMENT,
 *   `uid` mediumint(8) unsigned NOT NULL,
 *   `data` text,
 *   `created_at` date DEFAULT NULL,
 *   `updated_at` date DEFAULT NULL,
 *   PRIMARY KEY (`id`)
 * )ENGINE=MyISAM;
 */
namespace Webim
{

    public class WebimSetting
    {

        public WebimSetting(long uid, string data)
        {
            Uid = uid;
            Data = data;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

		public long Id { get; set; }

		public long Uid { get; set; }

		public string Data { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime UpdatedAt { get; set; }

    }
}
