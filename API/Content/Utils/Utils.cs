using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Content.Utils
{
    public static class Utils
    {

        public static string UrlEnCode(string url)
        {
            var url_code = string.Empty;
            if (url != string.Empty)
            {
                url_code = url.Replace('&', ',');
            }
            return url_code;
        }

        public static string UrlDecode(string url)
        {
            var url_decode = string.Empty;
            if (url != string.Empty)
            {
                url_decode = url.Replace(',', '&');
            }
            return url_decode;
        }
    }
}