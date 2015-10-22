using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tg_bot.response
{

    public class MessageResponse
    {
        public bool ok { get; set; }
        public MessageResponseResult result { get; set; }
    }

    public class MessageResponseResult
    {
        public int message_id { get; set; }
        public MessageResponseFrom from { get; set; }
        public MessageResponseChat chat { get; set; }
        public int date { get; set; }
        public string text { get; set; }
    }

    public class MessageResponseFrom
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string username { get; set; }
    }

    public class MessageResponseChat
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string username { get; set; }
        public string type { get; set; }
    }

}
