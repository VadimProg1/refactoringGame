using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation11
{
    class Cucumber : Food
    {
        public Cucumber(int x, int y, Random randomm, List<Cell> objectsListt, object[,] mapp) : base(x, y, randomm, objectsListt, mapp)
        {
        }

        public override Food FactoryMethodForFood(int razbrosX, int razbrosY)
        {
            return new Cucumber(
                                x: razbrosX,
                                y: razbrosY,
                                randomm: random,
                                objectsListt: objectsList,
                                mapp: map
                                );
        }

        public override Brush GetColor()
        {
            return Brushes.DarkGreen;
        }
    }
}
