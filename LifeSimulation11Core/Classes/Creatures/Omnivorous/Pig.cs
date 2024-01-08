using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation11
{
    class Pig<HFood, PFood, FFood> : CreatureOmnivorous<HFood, PFood, FFood>, ISmallAnimal
    {
        public Pig(int x, int y, bool gender, Random randomm, List<Cell> objectsListt,
             object[,] mapp) : base(x, y, gender, randomm, objectsListt, mapp)
        {

        }

        public override void SearchLoveFactory()
        {
            SearchLove<Pig<HFood, PFood, FFood>>();
        }

        public override void CheckMyLoveFactory()
        {
            CheckMyLove<Pig<HFood, PFood, FFood>>();
        }


        public override Brush GetCreatureColor()
        {
            return Brushes.Pink;
        }
        public override Creature FactoryMethod(bool babyGender, int razbrosX, int razbrosY)
        {
            return new Pig<HFood, PFood, FFood>(
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
