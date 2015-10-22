using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tg_bot
{
    public abstract class services
    {
        protected string mcaller;
        protected string mdescription;
        protected botx4150 mparent;

        public string Caller
        { get { return mcaller; } }

        public string Description
        { get { return mdescription; } }

        public services(botx4150 parent, string caller, string description)
        {
            this.mparent = parent;
            this.mcaller = caller;
            this.mdescription = description;
        }

        public abstract bool Execute(int id, string[] message);
    }
}
