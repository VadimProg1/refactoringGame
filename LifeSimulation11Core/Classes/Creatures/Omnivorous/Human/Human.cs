using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation11
{
    abstract class Human<HFood, PFood, FFood> : CreatureOmnivorous<HFood, PFood, FFood>, ISmallAnimal
    {
        public House house = null;
        
        public Human(int x, int y, bool gender, Random randomm, List<Cell> objectsListt,
             object[,] mapp) : base(x, y, gender, randomm, objectsListt, mapp)
        {
            satiety = 300;
            thresholdValue = 300;
            foodBonus = 380;
            timeAfterKids = 10;
            AfterKidsCooldown = 6;
        }

        public void DoRandomMove()
        {
            int randDirection = random.Next(4);
            RandomMove(randDirection);
        }
        public void HungryBehaviour()
        {
            if (house == null)
            {
                SearchForFood<HFood, PFood, FFood>();
            }
            else
            {
                if (house.foodSupply > 0)
                {
                    satiety += foodBonus;
                    house.foodSupply--;
                }
                else
                {
                    SearchForFood<HFood, PFood, FFood>();
                }
            }
        }
        
        public override void CheckMyLove<TLove>()
        {
            if(house == null)
            {
                Human<HFood, PFood, FFood> humanLove = (Human<HFood, PFood, FFood>)love;
                if(humanLove.house != null)
                {
                    house = humanLove.house;
                }
            }
            if (love.satiety <= 0)
            {
                love.isAlone = true;
                love.love = null;
                isAlone = true;
                love = null;
                SearchLove<TLove>();
            }
            else
            {
                if(timeAfterKids <= 0 && love.timeAfterKids <= 0)
                {
                    if(Math.Abs(love.x - x) <= 3 && Math.Abs(love.y - y) <= 3)
                    {
                        MakeBaby<Human<HFood, PFood, FFood>>();
                    }
                }
            }
        }

        public override void SearchLoveFactory()
        {
            SearchLove<Human<HFood, PFood, FFood>>();
        }

        public override void CheckMyLoveFactory()
        {
            CheckMyLove<Human<HFood, PFood, FFood>>();
        }

        public override Brush GetColor()
        {
            return Brushes.Black;
        }
     
    }
}
