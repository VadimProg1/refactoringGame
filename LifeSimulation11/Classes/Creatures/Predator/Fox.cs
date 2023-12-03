using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation11
{
    class Fox<TFood, UFood> : CreaturePredator<TFood, UFood>, ISmallAnimal
    {
        public Fox(int x, int y, bool gender, Random randomm, List<Cell> objectsListt,
             object[,] mapp) : base(x, y, gender, randomm, objectsListt, mapp)
        {

        }

        public override void SearchLoveFactory()
        {
            SearchLove<Fox<TFood, UFood>>();
        }

        public override void CheckMyLoveFactory()
        {
            CheckMyLove<Fox<TFood, UFood>>();
        }

        public override Brush GetCreatureColor()
        {
            return Brushes.Orange;
        }
        public override Creature FactoryMethod(bool babyGender, int razbrosX, int razbrosY)
        {
            return new Fox<TFood, UFood>(
                            x: x + razbrosX,
                            y: y + razbrosY,
                            gender: babyGender,
                            randomm: random,
                            objectsListt: objectsList,
                            mapp: map
                            );
        }

    }
}
