using LifeSimulation11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation11
{
    class HumanFemale<HFood, PFood, FFood> : Human<HFood, PFood, FFood>
    {
        public bool takenFoodForHouse = false;
        public HumanFemale(int x, int y, bool gender, Random randomm, List<Cell> objectsListt,
             object[,] mapp) : base(x, y, gender, randomm, objectsListt, mapp)
        {

        }

        public override void Activate()
        {
            if (satiety <= 0)
            {
                Death();
            }

            if (satiety > thresholdValue) //Сыт
            {
                WellFedBehaviour();
            }
            else//Голоден
            {
                HungryBehaviour();
            }

            satiety--;
            timeAfterKids--;
        }
        private void WellFedBehaviour()
        {
            if (isAlone)//Если нет пары
            {
                SearchLove<Human<HFood, PFood, FFood>>();
                DoRandomMove();
                return;
            }

            CheckMyLove<Human<HFood, PFood, FFood>>();
            if (!isAlone && house != null)
            {
                if (house.foodSupply <= 2)
                {
                    if (takenFoodForHouse)
                    {
                        if (Math.Abs(house.x - x) <= 3 && Math.Abs(house.y - y) <= 3)
                        {
                            takenFoodForHouse = false;
                            house.IncrementFoodSupply();
                        }
                        else
                        {
                            MoveToPositionByOneStep(house.x, house.y);
                        }
                    }
                    else
                    {
                        SearchFoodForHouse();
                    }
                }
                else
                {
                    MoveToPositionByOneStep(house.x, house.y);
                }
            }
            else
            {
                DoRandomMove();
            }
        }

        public void SearchFoodForHouse()
        {
            int nearestFoodX = int.MaxValue;
            int nearestFoodY = int.MaxValue;
            int distToNearestX, distToNearestY, distToObjX, distToObjY;
            foreach (Cell obj in objectsList)
            {
                if (obj is Food)
                {
                    if (GetType() != obj.GetType())
                    {
                        distToNearestX = Math.Abs(nearestFoodX - x);
                        distToNearestY = Math.Abs(nearestFoodY - y);
                        distToObjX = Math.Abs(obj.x - x);
                        distToObjY = Math.Abs(obj.y - y);

                        if (distToNearestX <= 2 && distToNearestY <= 2)
                        {
                            takenFoodForHouse = true;
                            Food food = (Food)obj;
                        }
                        else if (distToObjX + distToObjY < distToNearestX + distToNearestY)
                        {
                            nearestFoodX = obj.x;
                            nearestFoodY = obj.y;
                        }
                    }
                }
            }
            MoveToPositionByOneStep(nearestFoodX, nearestFoodY);
        }

        public override Creature FactoryMethod(bool babyGender, int razbrosX, int razbrosY)
        {
            return new HumanFemale<HFood, PFood, FFood>(
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
