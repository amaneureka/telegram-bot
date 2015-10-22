using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tg_bot.response;

namespace tg_bot.service
{
    public class latest : services
    {
        public latest(botx4150 parent)
            : base(parent, "/latest", "Latest update from CollegeSpace") { }

        public override bool Execute(int id, string[] message)
        {
            const string URL = "http://updates.collegespace.in/wp-json/posts?filter[posts_per_page]={0}&filter[category_name]=notices&page=0";

            int count = message.Length > 1 ? int.Parse(message[1]) : 5;

            var res = mparent.ExecuteMethod<CollegeSpacePost[]>(string.Format(URL, count), null, true).Result;
            
            var sb = new StringBuilder();
            sb.AppendLine("Latest Updates (`Notices`)");
            if (count != res.Length)
                sb.AppendLine("Sorry I could't fetched all the post");
            count = Math.Min(count, res.Length);            
            for (int i = 0; i < count; i++)
                sb.AppendLine(res[i].title);
            mparent.SendMessage(id, sb.ToString());
            return true;
        }
    }
}
