using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation11
{
    public abstract class Creature : Cell
    {
        public int maxSatiety = 200;
        public int satiety = 200;
        public int timeAfterKids = 30;
        public int AfterKidsCooldown = 30;
        public int thresholdValue = 150;
        public int foodBonus = 60;
        public bool gender;
        public static object[,] map;
        public static List<Cell> objectsList;
        public bool isAlone = true;
        public Creature love;
        public static Random random;

        public Creature(int x, int y, bool gender, Random randomm, List<Cell> objectsListt,
            object[,] mapp) : base(x, y)
        {
            this.gender = gender;
            random = randomm;
            objectsList = objectsListt;
            map = mapp;
            maxSatiety = satiety;
        }

        public void Death()
        {          
            for(int i = 0; i < objectsList.Count; i++)
            {
                if(objectsList[i].x == x && objectsList[i].y == y)
                {
                    objectsList.RemoveAt(i);
                    break;
                }
            }
            
            map[x, y] = new Cell(
                    x: x,
                    y: y
                    );
        }

        public void SearchForFood<HFood, PFood, TFood>()
        {
            int nearestFoodX = 100000000;
            int nearestFoodY = 100000000;
            int nearestFoodIndex = 0;
            for (int i = 0; i < objectsList.Count(); i++)
            {
                if (objectsList[i] is TFood || objectsList[i] is HFood || objectsList[i] is PFood)
                {
                    if (GetType() != objectsList[i].GetType())
                    {
                        int distanceToObjX = Math.Abs(objectsList[i].x - x);
                        int distanceToObjY = Math.Abs(objectsList[i].y - y);
                        int distanceToNearestX = Math.Abs(nearestFoodX - x);
                        int distanceToNearestY = Math.Abs(nearestFoodY - y);
                        if ((Math.Abs(objectsList[i].x - x) + Math.Abs(objectsList[i].y - y))
                    < (Math.Abs(nearestFoodX - x) + Math.Abs(nearestFoodY - y)))
                            {
                            nearestFoodX = objectsList[i].x;
                            nearestFoodY = objectsList[i].y;
                            nearestFoodIndex = i;
                            if (Math.Abs(nearestFoodX - x) <= 2 && Math.Abs(nearestFoodY - y) <= 2)
                            {
                                if (objectsList[nearestFoodIndex] is TFood)
                                {
                                    EatFood<TFood>(nearestFoodIndex);
                                }
                                else if (objectsList[nearestFoodIndex] is PFood)
                                {
                                    EatFood<PFood>(nearestFoodIndex);
                                }
                                else if (objectsList[nearestFoodIndex] is HFood)
                                {
                                    EatFood<HFood>(nearestFoodIndex);
                                }
                                break;
                            }
                        }
                    }
                }
            }

            MoveTo(nearestFoodX, nearestFoodY);
        }

        public void SearchForFood<TFood, UFood>()
        {
            int nearestFoodX = 100000000;
            int nearestFoodY = 100000000;
            int nearestFoodIndex = 0;
            for (int i = 0; i < objectsList.Count(); i++)
            {
                if(objectsList[i] is TFood || objectsList[i] is UFood)
                {
                    if (GetType() != objectsList[i].GetType())
                    {
                        if ((Math.Abs(objectsList[i].x - x) + Math.Abs(objectsList[i].y - y))
                    < (Math.Abs(nearestFoodX - x) + Math.Abs(nearestFoodY - y)))
                        {
                            nearestFoodX = objectsList[i].x;
                            nearestFoodY = objectsList[i].y;
                            nearestFoodIndex = i;
                            if (Math.Abs(nearestFoodX - x) <= 2 && Math.Abs(nearestFoodY - y) <= 2)
                            {
                                if (objectsList[nearestFoodIndex] is TFood)
                                {
                                    EatFood<TFood>(nearestFoodIndex);
                                }
                                else if (objectsList[nearestFoodIndex] is UFood)
                                {
                                    EatFood<UFood>(nearestFoodIndex);
                                }
                                break;
                            }
                        }
                    }
                }
            }
            MoveTo(nearestFoodX, nearestFoodY);
        }

        public void SearchForFood<TFood>()
        {
            int nearestFoodX = 100000000;
            int nearestFoodY = 100000000;

            for (int i = 0; i < objectsList.Count(); i++)
            {
                if(objectsList[i] is TFood)
                {
                    if (GetType() != objectsList[i].GetType())
                    {
                        if ((Math.Abs(objectsList[i].x - x) + Math.Abs(objectsList[i].y - y))
                    < (Math.Abs(nearestFoodX - x) + Math.Abs(nearestFoodY - y)))

                        {
                            nearestFoodX = objectsList[i].x;
                            nearestFoodY = objectsList[i].y;
                            if (Math.Abs(objectsList[i].x - x) <= 2 && Math.Abs(objectsList[i].y - y) <= 2)
                            {
                                EatFood<TFood>(i);
                                break;
                            }
                        }
                    }             
                }
            }
            MoveTo(nearestFoodX, nearestFoodY);
        }

        void EatFood<TFood>(int index)
        {
            if (objectsList[index] is Food)
            {
                Food eatenFood = (Food)objectsList[index];
                eatenFood.SpawnFoodNearFood();
                eatenFood.Death();
                satiety += foodBonus;
                if(satiety > maxSatiety)
                {
                    satiety = maxSatiety;
                }
            }
            else if(objectsList[index] is Creature)
            {
                Creature eatenCreatire = (Creature)objectsList[index];
                eatenCreatire.Death();
                satiety += foodBonus;
            }
        }

        public void MoveTo(int nearestX, int nearestY)
        {
            if (nearestX < x)
            {
                MoveLeft();
            }
            if (nearestX > x)
            {
                MoveRight();
            }
            if (nearestY < y)
            {
                MoveUp();
            }
            if (nearestY > y)
            {
                MoveDown();
            }
        }

        public void RandomMove(int randDirection)
        {
            if(randDirection == 0)
            {
                MoveRight();
            }
            else if (randDirection == 1)
            {
                MoveLeft();
            }
            else if(randDirection == 2)
            {
                MoveDown();
            }
            else if(randDirection == 3)
            {
                MoveUp();
            }
        }

        public void MoveLeft()
        {
            if (x - 1 > 0)
            {                              
                if (map[x - 1, y] is Creature)
                {

                }
                else if(map[x - 1, y] is Food)
                {
                    Food food = (Food)map[x - 1, y];
                    food.Death();
                }
                else if (map[x - 1, y] is Cell)
                {
                    map[x - 1, y] = map[x, y];
                    map[x, y] = new Cell(
                        x: x,
                        y: y
                        );
                    x--;
                }
            }
        }

        public void MoveRight()
        {
            if (x + 1 < 1000)
            {              
                if (map[x + 1, y] is Creature)
                {

                }
                else if (map[x + 1, y] is Food)
                {
                    Food food = (Food)map[x + 1, y];
                    food.Death();
                }
                else if (map[x + 1, y] is Cell)
                {
                    map[x + 1, y] = map[x, y];
                    map[x, y] = new Cell(
                        x: x,
                        y: y
                        );
                    x++;
                }
            }
        }

        public void MoveUp()
        {
            if (y - 1 > 0)
            {              
                if (map[x, y - 1] is Creature)
                {

                }
                else if (map[x, y - 1] is Food)
                {
                    Food food = (Food)map[x, y - 1];
                    food.Death();
                }
                else if (map[x, y - 1] is Cell)
                {
                    map[x, y - 1] = map[x, y];
                    map[x, y] = new Cell(
                        x: x,
                        y: y
                        );
                    y--;
                }
            }
        }

        public void MoveDown()
        {
            if (y + 1 < 1000)
            {            
                if (map[x, y + 1] is Creature)
                {

                }
                else if (map[x, y + 1] is Food)
                {
                    Food food = (Food)map[x, y + 1];
                    food.Death();
                }
                else if (map[x, y + 1] is Cell)
                {
                    map[x, y + 1] = map[x, y];
                    map[x, y] = new Cell(
                        x: x,
                        y: y
                        );
                    y++;
                }
            }
        }

        public void SearchLove<TTarget>()
            where TTarget : Creature
        {
            int myIndex = -1;
            int loveIndex = -1;

            for (int i = 0; i < objectsList.Count(); i++)
            {
                if (objectsList[i] is TTarget)
                {
                    TTarget potentialLove = (TTarget)objectsList[i];
                    if (objectsList[i].x == x && objectsList[i].y == y)
                    {
                        myIndex = i;
                        if (loveIndex != -1)
                        {
                            isAlone = false;
                            break;
                        }
                    }
                    if (potentialLove.gender != gender &&
                        potentialLove.satiety > thresholdValue &&
                        potentialLove.isAlone == true &&
                        potentialLove.timeAfterKids <= 0)
                    {
                        if ((Math.Abs(objectsList[i].x - x) < 200
                            && Math.Abs(objectsList[i].y - y) < 200))
                        {
                            loveIndex = i;
                            if (myIndex != -1)
                            {
                                isAlone = false;
                                break;
                            }
                        }
                    }
                }
            }

            if (!isAlone)
            {
                TTarget potentialLove = (TTarget)objectsList[loveIndex];
                love = potentialLove;
                potentialLove.isAlone = false;
                potentialLove.love = (TTarget)objectsList[myIndex];
            }
        }

        public abstract Creature FactoryMethod(bool t, int a, int x);

        public abstract void Activate();

        public abstract Brush GetCreatureColor();

        public virtual void CheckMyLove<TLove>()
            where TLove : Creature
        {
            if (love.satiety <= thresholdValue)
            {
                love.isAlone = true;
                love.love = null;
                isAlone = true;
                love = null;
                SearchLove<TLove>();
            }
            else
            {
                if (Math.Abs(love.x - x) <= 2 && Math.Abs(love.y - y) <= 2)
                {
                    MakeBaby<TLove>();
                }
            }
        }

        public void MakeBaby<TBaby>()
            where TBaby : Creature
        {
            int randGender = random.Next(2);
            bool babyGender = false;
            if (randGender == 0)
            {
                babyGender = false;
            }
            else
            {
                babyGender = true;
            }
            int razbrosX = random.Next(-5, 5);
            int razbrosY = random.Next(-5, 5);
            bool check = true;
            while (check)
            {
                if ((x + razbrosX > 0 && x + razbrosX < 1000)
                    && (y + razbrosY > 0 && y + razbrosY < 1000))
                {
                    if (map[(x + razbrosX), (y + razbrosY)] is Creature)
                    {
                        razbrosX = random.Next(-50, 50);
                        razbrosY = random.Next(-50, 50);
                    }
                    else
                    {
                        check = false;
                        TBaby baby = (TBaby)FactoryMethod(babyGender, razbrosX, razbrosY);
                    
                        map[(x + razbrosX), (y + razbrosY)] = baby;
                        objectsList.Add(baby);
                        love.timeAfterKids = AfterKidsCooldown;
                        timeAfterKids = AfterKidsCooldown;
                    }

                }
                else
                {
                    razbrosX = random.Next(-50, 50);
                    razbrosY = random.Next(-50, 50);
                }
            }
        }
    }
}
