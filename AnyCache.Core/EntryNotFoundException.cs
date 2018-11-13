using System;
using System.Collections.Generic;
using System.Text;

namespace AnyCache.Core
{
    public class EntryNotFoundException : ApplicationException
    {
        public EntryNotFoundException()
            :base("Entry not exists in the cache.")
        {

        }
    }
}
