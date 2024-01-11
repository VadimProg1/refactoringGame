using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation11
{
    class Camel<TFood> : CreatureHerbivore<TFood>, IBigAnimal
    {
        public Camel(int x, int y, bool gender, Random randomm, List<Cell> objectsListt,
            object[,] mapp) : base(x, y, gender, randomm, objectsListt, mapp)
        {

        }

        public override void SearchLoveFactory()
        {
            SearchLove<Camel<TFood>>();
        }

        public override void CheckMyLoveFactory()
        {
            CheckMyLove<Camel<TFood>>();
        }

        public override Brush GetColor()
        {
            return Brushes.SandyBrown;
        }
        public override Creature FactoryMethod(bool babyGender, int razbrosX, int razbrosY)
        {
            return new Camel<TFood>(
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
