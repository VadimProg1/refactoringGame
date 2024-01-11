using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation11
{
    class House : Cell
    {
        public int foodSupply = 0;
        private int maxFoodSupply = 10;
        public static Random random;
        public House(int x, int y, Random randomm) : base(x, y)
        {
            random = randomm;
        }
        public void IncrementFoodSupply()
        {
            if(foodSupply < maxFoodSupply)
            {
                foodSupply++;
            }
        }

    }
}
