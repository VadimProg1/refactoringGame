﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation11
{
    class Wolf<TFood, UFood> : CreaturePredator<TFood, UFood>, IBigAnimal
    {
        public Wolf(int x, int y, bool gender, Random randomm, List<Cell> objectsListt,
             object[,] mapp) : base(x, y, gender, randomm, objectsListt, mapp)
        {

        }

        public override void SearchLoveFactory()
        {
            SearchLove<Wolf<TFood, UFood>>();
        }

        public override void CheckMyLoveFactory()
        {
            CheckMyLove<Wolf<TFood, UFood>>();
        }

        public override Brush GetCreatureColor()
        {
            return Brushes.DarkGray;
        }

        public override Creature FactoryMethod(bool babyGender, int razbrosX, int razbrosY)
        {
            return new Wolf<TFood, UFood>(
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
