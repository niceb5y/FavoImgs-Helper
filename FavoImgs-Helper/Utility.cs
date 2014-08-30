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
            //구성요소 자동 다운로드를 위한 파일 다운로드 함수
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
