﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tg_bot.response
{
    public class User
    {
        public bool ok { get; set; }
        public UserResult result { get; set; }
    }

    public class UserResult
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string username { get; set; }
    }
}
