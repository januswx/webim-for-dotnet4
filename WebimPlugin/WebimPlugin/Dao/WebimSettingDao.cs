using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
namespace Webim
{

    public class WebimSettingDao
    {


        //TODO: QUERY DATABASE
        public string Get(long uid)
        {
            //"select data from spb_Webim_Settings where uid = @0", uid
            return "{}";
        }

        //TODO: QUERY DATABASE
        public void Set(long uid, string data)
        {
            //"update spb_Webim_Settings set data =@0  where uid = @1", uid, data
        }
    }
}
