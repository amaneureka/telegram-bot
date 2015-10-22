using System;
using System.IO;
using tg_bot.response;
using System.Net;
using System.Collections.Generic;

namespace tg_bot.service
{
    public class getimg : services
    {
        public getimg(botx4150 parent)
            : base(parent, "/getimg", "Fetches image for you") { }

        public override bool Execute(int id, string[] message)
        {
            const string URL = "https://pbs.twimg.com/profile_images/536066598608461826/mBO3ZVDX.jpeg";
            mparent.SendMessage(id, "DEBUG::You asked me to fetch: " + message[1]);
            using(var wc = new WebClient())
                wc.DownloadFile(URL, "temp.jpeg");
            mparent.SendPhoto(id, new FileToSend("temp.jpeg", new FileStream("temp.jpeg", FileMode.Open)));
            return true;
        }
    }
}
