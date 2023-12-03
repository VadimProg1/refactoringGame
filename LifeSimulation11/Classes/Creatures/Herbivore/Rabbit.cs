using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation11
{
    class Rabbit<TFood> : CreatureHerbivore<TFood>, ISmallAnimal
    {
        public Rabbit(int x, int y, bool gender, Random randomm, List<Cell> objectsListt,
            object[,] mapp) : base(x, y, gender, randomm, objectsListt, mapp)
        {

        }

        public override void SearchLoveFactory()
        {
            SearchLove<Rabbit<TFood>>();
        }

        public override void CheckMyLoveFactory()
        {
            CheckMyLove<Rabbit<TFood>>();
        }

        public override Brush GetCreatureColor()
        {
            return Brushes.White;
        }

        public override Creature FactoryMethod(bool babyGender, int razbrosX, int razbrosY)
        {
            return new Rabbit<TFood>(
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
