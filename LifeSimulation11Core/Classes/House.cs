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
        Image houseImage;
        public static Random random;
        public House(int x, int y, Random randomm) : base(x, y)
        {
            random = randomm;
            houseImage = GenerateRandomImage();
        }
        public void IncrementFoodSupply()
        {
            if(foodSupply < maxFoodSupply)
            {
                foodSupply++;
            }
        }

        public Image GenerateRandomImage()
        {
            int rand = random.Next(0, 1);

            switch (rand)
            {
                case 0:
                    houseImage = Image.FromFile("D:/Source/Repos/OOP-LifeSimulation/LifeSimulation11/house-0.png");
                    break;
                case 1:
                    houseImage = Image.FromFile("D:/Source/Repos/OOP-LifeSimulation/LifeSimulation11/house-0.png");
                    break;
            }
            return houseImage;
        }
    }
}
