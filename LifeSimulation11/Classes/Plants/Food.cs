using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation11
{
    abstract class Food : Cell
    {
        public static Random random;
        public static List<Cell> objectsList;
        public static object[,] map;
        public Food(int x, int y, Random randomm, List<Cell> objectsListt, object[,] mapp) : base(x, y)
        {
            random = randomm;
            objectsList = objectsListt;
            map = mapp;
        }

        public abstract Brush GetFoodColor();

        public void Death()
        {
            for (int i = 0; i < objectsList.Count; i++)
            {
                if (objectsList[i].x == x && objectsList[i].y == y && objectsList[i] is Food)
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

        public abstract Food FactoryMethodForFood(int razbrosX, int razbrosY);
        
        public void SpawnFoodNearFood()
        {
            int randIndex = random.Next(objectsList.Count() - 1);
            int razbrosX = random.Next(-50, 50);
            int razbrosY = random.Next(-50, 50);
            bool check = true;
            while (check)
            {
                if ((objectsList[randIndex].x + razbrosX > 0 && objectsList[randIndex].x + razbrosX < 1000)
                    && (objectsList[randIndex].y + razbrosY > 0 && objectsList[randIndex].y + razbrosY < 1000))
                {
                    if ((map[(objectsList[randIndex].x + razbrosX), (objectsList[randIndex].y + razbrosY)] is Creature)
                            || (map[(objectsList[randIndex].x + razbrosX), (objectsList[randIndex].y + razbrosY)] is Food))
                    {
                        randIndex = random.Next(objectsList.Count() - 1);
                        razbrosX = random.Next(-50, 50);
                        razbrosY = random.Next(-50, 50);
                    }
                    else
                    {
                        check = false;                      
                        Food newFood = FactoryMethodForFood(objectsList[randIndex].x + razbrosX, objectsList[randIndex].y + razbrosY);
                        map[(objectsList[randIndex].x + razbrosX), (objectsList[randIndex].y + razbrosY)] = newFood;
                        objectsList.Add(newFood);
                    }

                }
                else
                {
                    randIndex = random.Next(objectsList.Count() - 1);
                    razbrosX = random.Next(-50, 50);
                    razbrosY = random.Next(-50, 50);
                }
            }
        }


    }
}
