using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tg_bot.response
{
    public class Updates
    {
        public bool ok { get; set; }
        public UpdatesResult[] result { get; set; }
    }

    public class UpdatesResult
    {
        public int update_id { get; set; }
        public UpdatesMessage message { get; set; }
    }

    public class UpdatesMessage
    {
        public int message_id { get; set; }
        public UpdatesFrom from { get; set; }
        public UpdatesChat chat { get; set; }
        public int date { get; set; }
        public string text { get; set; }
        public UpdatesDocument document { get; set; }
    }

    public class UpdatesFrom
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string username { get; set; }
    }

    public class UpdatesChat
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string username { get; set; }
        public string type { get; set; }
    }

    public class UpdatesDocument
    {
        public string file_name { get; set; }
        public string mime_type { get; set; }
        public UpdatesThumb thumb { get; set; }
        public string file_id { get; set; }
        public int file_size { get; set; }
    }

    public class UpdatesThumb
    {
        public string file_id { get; set; }
        public int file_size { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }
}
