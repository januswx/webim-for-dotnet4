using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webim
{
    public class WebimException : System.Exception
    {
        private int code;

        public WebimException(int code, string status)
            : base(status)
        {
            this.code = code;
        }

        public int getCode()
        {
            return code;
        }

    }
}