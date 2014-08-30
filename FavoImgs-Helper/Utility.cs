using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace FavoImgs_Helper
{
    class Utility
    {
        public static void downloadFile(string Path, params string[] Addresses)
        {
            WebClient webCL = new WebClient();
            if (Path.EndsWith("\\") == false)
            {
                Path += "\\";
            }
            foreach (string Address in Addresses)
            {
                webCL.DownloadFile(Address, Path + Address.Substring(Address.LastIndexOf("/") + 1));
            }
        }
    }
}
