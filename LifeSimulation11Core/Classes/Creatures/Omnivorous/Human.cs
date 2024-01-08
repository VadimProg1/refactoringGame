using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation11
{
    class Human<HFood, PFood, FFood> : CreatureOmnivorous<HFood, PFood, FFood>, ISmallAnimal
    {
        public House house = null;
        private int futureHouseX, futureHouseY;
        private bool movingToFutureHouse = false;
        private bool takenFoodForHouse = false;
        private int minRadiusOfHousing = 5;
        private int maxRadiusOfHousing = 30;
        public Human(int x, int y, bool gender, Random randomm, List<Cell> objectsListt,
             object[,] mapp) : base(x, y, gender, randomm, objectsListt, mapp)
        {
            satiety = 300;
            thresholdValue = 300;
            foodBonus = 380;
            timeAfterKids = 10;
            AfterKidsCooldown = 6;
        }

        public override void Activate()
        {
            if(gender == true)
            {
                ActivateMale();
            }
            else
            {
                ActivateFemale();
            }
        }

        private void ActivateMale()
        {
            if (satiety <= 0)
            {
                Death();
            }
            
            if(satiety > thresholdValue)//Сыт
            {
                if(isAlone)//Если нет пары
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
                        else if(house != null)
                        {
                            MoveTo(house.x, house.y);
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

        private void ActivateFemale()
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
                        if (house != null)
                        {
                            if (house.foodSupply <= 2)
                            {
                                if (takenFoodForHouse)
                                {
                                    if(Math.Abs(house.x - x) <= 3 && Math.Abs(house.y - y) <= 3)
                                    {
                                        takenFoodForHouse = false;
                                        house.IncrementFoodSupply();
                                    }
                                    else
                                    {
                                        MoveTo(house.x, house.y);
                                    }
                                }
                                else
                                {
                                    SearchFoodForHouse();
                                }
                            }
                            else
                            {
                                MoveTo(house.x, house.y);
                            }
                        }
                        else
                        {
                            DoRandomMove();
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

        private void DoRandomMove()
        {
            int randDirection = random.Next(4);
            RandomMove(randDirection);
        }

        private void HungryBehaviour()
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
        public void SearchFoodForHouse()
        {
            int nearestFoodX = 1000000;
            int nearestFoodY = 1000000;
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

                        if(distToNearestX <= 2 && distToNearestY <= 2)
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
            MoveTo(nearestFoodX, nearestFoodY);
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

        public void SearchPlaceForHouseAndBuild()
        {
            const int infCoord = 10000000;
            int closestHouseX = infCoord;
            int closestHouseY = infCoord;
            int distToNearestX, distToNearestY, distToObjX, distToObjY;
            for (int i = 0; i < objectsList.Count(); i++)
            {
                if(objectsList[i] is House)
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

            if(closestHouseX == infCoord && closestHouseY == infCoord) {
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
            foreach(Cell obj in objectsList)
            {
                if(obj is House)
                {
                    housesList.Add((House)obj);
                }
            }
            housesList.Sort(new HouseComparer(x: x, y: y));
            foreach(House neighbour in housesList)
            {
                bool check = true, checkRight = true, checkLeft = true, checkUp = true, checkDown = true; 
                int left = minRadiusOfHousing * -1, right = minRadiusOfHousing, up, down;
                up = left;
                down = right;
                foreach(House neighbourOfNeighbour in housesList)
                {
                    int distanceX = Math.Abs(neighbourOfNeighbour.x - neighbour.x);
                    int distanceY = Math.Abs(neighbourOfNeighbour.y - neighbour.y);
                    if (neighbourOfNeighbour.x != neighbour.x && neighbourOfNeighbour.y != neighbour.y)
                    {
                        if(Math.Sqrt((distanceX + left) * (distanceX + left) + distanceY * distanceY) < minRadiusOfHousing)
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
                        if(!checkLeft && !checkRight && !checkUp && !checkDown)
                        {
                            check = false;
                            break;
                        }
                    }
                }
                if (check)
                {
                    bool triedLeft = false, triedRight = false, triedUp = false, triedDown = false, quit = false;
                    int randMove;
                    while((!triedDown || !triedUp || !triedLeft || !triedRight))
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
        }

        private void MoveToFutureHouseAndBuildWhenHeDidIt()
        {
            if(Math.Abs(futureHouseX - x) <= 10 && Math.Abs(futureHouseY - y) <= 10)
            {
                movingToFutureHouse = false;
                BuildHouse();
            }
            else
            {
                MoveTo(futureHouseX, futureHouseY);
            }         
        }

        public void BuildHouse()
        {
            House newHouse = new House(x: x, y: y, randomm: random);
            objectsList.Add(newHouse);
            house = newHouse;
        }
        public override void SearchLoveFactory()
        {
            SearchLove<Human<HFood, PFood, FFood>>();
        }

        public override void CheckMyLoveFactory()
        {
            CheckMyLove<Human<HFood, PFood, FFood>>();
        }

        public override Brush GetCreatureColor()
        {
            return Brushes.Black;
        }
        public override Creature FactoryMethod(bool babyGender, int razbrosX, int razbrosY)
        {
            return new Human<HFood, PFood, FFood>(
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
