﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation11
{
    class Bear<HFood, PFood, FFood> : CreatureOmnivorous<HFood, PFood, FFood>, IBigAnimal
    {
        public Bear(int x, int y, bool gender, Random randomm, List<Cell> objectsListt,
             object[,] mapp) : base(x, y, gender, randomm, objectsListt, mapp)
        {

        }

        public override void SearchLoveFactory()
        {
            SearchLove<Bear<HFood, PFood, FFood>>();
        }

        public override void CheckMyLoveFactory()
        {
            CheckMyLove<Bear<HFood, PFood, FFood>>();
        }

        public override Brush GetCreatureColor()
        {
            return Brushes.Brown;
        }
        public override Creature FactoryMethod(bool babyGender, int razbrosX, int razbrosY)
        {
            return new Bear<HFood, PFood, FFood>(
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
