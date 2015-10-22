using System;
using System.IO;
using System.Collections.Generic;

namespace tg_bot.response
{
    public class FileToSend
    {
        public string Filename { get; internal set; }
        public Stream Content { get; internal set; }

        public FileToSend(string filename, Stream content)
        {
            Filename = filename;
            Content = content;
        }
    }
}
