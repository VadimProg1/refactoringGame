using LifeSimulation11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation11
{
    class HumanMale<HFood, PFood, FFood> : Human<HFood, PFood, FFood>
    {
        public int minRadiusOfHousing = 5;
        public int maxRadiusOfHousing = 30;
        public bool movingToFutureHouse = false;
        public int futureHouseX, futureHouseY;
        public HumanMale(int x, int y, bool gender, Random randomm, List<Cell> objectsListt,
             object[,] mapp) : base(x, y, gender, randomm, objectsListt, mapp)
        {
            
        }

        public override void Activate()
        {
            if (satiety <= 0)
            {
                Death();
            }

            if (satiety > thresholdValue)//Сыт
            {
                if (isAlone)//Если нет пары
                {
                    SearchLove<Human<HFood, PFood, FFood>>();
                    DoRandomMove();
                }
                else//Если она есть
                {
                    CheckMyLove<Human<HFood, PFood, FFood>>();
                    if (!isAlone)
                    {
                        if (house == null)
                        {
                            SearchPlaceForHouseAndBuild();
                        }
                        else if (house == null && movingToFutureHouse)
                        {
                            MoveToFutureHouseAndBuildWhenHeDidIt();
                        }
                        else if (house != null)
                        {
                            MoveToPositionByOneStep(house.x, house.y);
                        }
                    }
                    else
                    {
                        DoRandomMove();
                    }
                }
            }
            else//Голоден
            {
                HungryBehaviour();
            }

            satiety--;
            timeAfterKids--;
        }

        private void MoveToFutureHouseAndBuildWhenHeDidIt()
        {
            if (Math.Abs(futureHouseX - x) <= 10 && Math.Abs(futureHouseY - y) <= 10)
            {
                movingToFutureHouse = false;
                BuildHouse();
            }
            else
            {
                MoveToPositionByOneStep(futureHouseX, futureHouseY);
            }
        }

        public void SearchPlaceForHouseAndBuild()
        {
            int closestHouseX = int.MaxValue;
            int closestHouseY = int.MaxValue;
            int distToNearestX, distToNearestY, distToObjX, distToObjY;
            for (int i = 0; i < objectsList.Count(); i++)
            {
                if (objectsList[i] is House)
                {
                    distToNearestX = Math.Abs(closestHouseX - x);
                    distToNearestY = Math.Abs(closestHouseY - y);
                    distToObjX = Math.Abs(objectsList[i].x - x);
                    distToObjY = Math.Abs(objectsList[i].y - y);
                    if (distToObjX + distToObjY < distToNearestX + distToNearestY)
                    {
                        closestHouseX = objectsList[i].x;
                        closestHouseY = objectsList[i].y;
                    }
                }
            }

            if (closestHouseX == int.MaxValue && closestHouseY == int.MaxValue)
            {
                BuildHouse();
            }
            else if ((Math.Abs(closestHouseX - x) > maxRadiusOfHousing || Math.Abs(closestHouseY - y) > maxRadiusOfHousing))
            {
                BuildHouse();
            }
            else
            {
                movingToFutureHouse = true;
                FindFutureHouseCoord();
            }
        }

        public void FindFutureHouseCoord()
        {
            List<House> housesList = new List<House>();
            foreach (Cell obj in objectsList)
            {
                if (obj is House)
                {
                    housesList.Add((House)obj);
                }
            }
            housesList.Sort(new HouseComparer(x: x, y: y));
            foreach (House neighbour in housesList)
            {
                bool checkRight = true, checkLeft = true, checkUp = true, checkDown = true;
                int left = minRadiusOfHousing * -1, right = minRadiusOfHousing, up, down;
                up = left;
                down = right;
                foreach (House neighbourOfNeighbour in housesList)
                {
                    int distanceX = Math.Abs(neighbourOfNeighbour.x - neighbour.x);
                    int distanceY = Math.Abs(neighbourOfNeighbour.y - neighbour.y);
                    if (neighbourOfNeighbour.x != neighbour.x && neighbourOfNeighbour.y != neighbour.y)
                    {
                        if (Math.Sqrt((distanceX + left) * (distanceX + left) + distanceY * distanceY) < minRadiusOfHousing)
                        {
                            checkLeft = false;
                        }
                        if (Math.Sqrt((distanceX + right) * (distanceX + right) + distanceY * distanceY) < minRadiusOfHousing)
                        {
                            checkRight = false;
                        }
                        if (Math.Sqrt(distanceX * distanceX + (distanceY + up) * (distanceY + up)) < minRadiusOfHousing)
                        {
                            checkUp = false;
                        }
                        if (Math.Sqrt(distanceX * distanceX + (distanceY + down) * (distanceY + down)) < minRadiusOfHousing)
                        {
                            checkDown = false;
                        }
                        if (!checkLeft && !checkRight && !checkUp && !checkDown)
                        {
                            return;
                        }
                    }
                }         

                bool triedLeft = false, triedRight = false, triedUp = false, triedDown = false, quit = false;
                int randMove;
                while ((!triedDown || !triedUp || !triedLeft || !triedRight))
                {
                    randMove = random.Next(4);
                    switch (randMove)
                    {
                        case 0:
                            triedLeft = true;
                            if (checkLeft)
                            {
                                futureHouseX = neighbour.x + left;
                                futureHouseY = neighbour.y;
                                quit = true;
                            }
                            break;
                        case 1:
                            triedRight = true;
                            if (checkRight)
                            {
                                futureHouseX = neighbour.x + right;
                                futureHouseY = neighbour.y;
                                quit = true;
                            }
                            break;
                        case 2:
                            triedDown = true;
                            if (checkDown)
                            {
                                futureHouseX = neighbour.x;
                                futureHouseY = neighbour.y + down;
                                quit = true;
                            }
                            break;
                        case 3:
                            triedUp = true;
                            if (checkUp)
                            {
                                futureHouseX = neighbour.x;
                                futureHouseY = neighbour.y + up;
                                quit = true;
                            }
                            break;
                    }
                }
            }
        }
        public void BuildHouse()
        {
            House newHouse = new House(x: x, y: y, randomm: random);
            objectsList.Add(newHouse);
            house = newHouse;
        }

        public override Creature FactoryMethod(bool babyGender, int razbrosX, int razbrosY)
        {
            return new HumanMale<HFood, PFood, FFood>(
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
