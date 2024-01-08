using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation11
{
    class Pair<T, U>
    {
        public T first { get; set; }

        public U second { get; set; }

        public Pair(T first, U second)
        {
            this.first = first;
            this.second = second;
        }
    }
}
