using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common
{
    class HashtableClass : Hashtable
    {
        public HashtableClass(string name)
        {
            Name = name;
        }
        public HashtableClass(string name, IEqualityComparer equalityComparer) : base(equalityComparer)
        {
            Name = name;
        }

        public string Name { get; set; } = "";
    }
}
