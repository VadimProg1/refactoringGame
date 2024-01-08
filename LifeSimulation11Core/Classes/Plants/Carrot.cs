using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation11
{
    class Carrot : Food
    {
        public Carrot(int x, int y, Random randomm, List<Cell> objectsListt, object[,] mapp) : base(x, y, randomm, objectsListt, mapp)
        {
        }

        public override Food FactoryMethodForFood(int razbrosX, int razbrosY)
        {
            return new Carrot(
                                x: razbrosX,
                                y: razbrosY,
                                randomm: random,
                                objectsListt: objectsList,
                                mapp: map
                                );
        }

        public override Brush GetFoodColor()
        {
            return Brushes.Yellow;
        }
    }
}
