using LifeSimulation11Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LifeSimulation11.Nuke;
using static System.Windows.Forms.AxHost;

namespace LifeSimulation11
{
    class House : Cell, IImageSprite
    {
        public int foodSupply = 0;
        private int maxFoodSupply = 10;
        public House(int x, int y) : base(x, y) { }
        public void IncrementFoodSupply()
        {
            if(foodSupply < maxFoodSupply)
            {
                foodSupply++;
            }
        }
        public Image GetImage()
        {
            return Image.FromFile("D:/Source/Repos/OOP-LifeSimulation/LifeSimulation11/house-0.png");
        }

        public int GetSpritePosX()
        {
            return x;
        }

        public int GetSpritePosY()
        {
            return y;
        }
    }
}
