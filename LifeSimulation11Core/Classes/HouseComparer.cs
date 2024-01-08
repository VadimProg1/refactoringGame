using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation11
{
    class HouseComparer : IComparer<House>
    {
        private int humanX, humanY;

        public HouseComparer(int x, int y)
        {
            humanX = x;
            humanY = y;
        }
        public int Compare(House p1, House p2)
        {
            double dist1 = Math.Abs(p1.x - humanX) + Math.Abs(p1.y - humanY);
            double dist2 = Math.Abs(p1.x - humanX) + Math.Abs(p1.y - humanY);
            if (dist1 > dist2)
            {
                return 1;
            }
            else if (dist1 == dist2)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
    }
}
