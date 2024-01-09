using System;
using System.Collections.Generic;
using System.Linq;

namespace LifeSimulation11
{
    public class Nuke : MoveableEntity
    {
        public static Random random;
        public static object[,] map;
        public static List<Cell> objectsList;
        static public int eventPropability = 1200;
        public bool isLaunched = false;
        bool isChoosedDir = false;
        int dirX = World.MAP_SIZE_X;
        int dirY = World.MAP_SIZE_Y;
        public string state = "none";
        int explosionTime = 50;
        int speed = 6;
        public Nuke(int x, int y, Random randomm, List<Cell> objectsListt, object[,] mapp) : base(x, y, mapp)
        {
            random = randomm;
            objectsList = objectsListt;
        }

        public void Activate()
        {
            if (ActivationEventIsStarted() || isLaunched)
            {
                isLaunched = true;
                LaunchNuke();
            }
            if(state == "explosion")
            {
                ExplosionTimer();
            }
        }

        public virtual bool ActivationEventIsStarted()
        {
            int eventProb = random.Next(eventPropability);
            return eventProb == 1 ? true : false;
        }

        public virtual void LaunchNuke()
        {
            if (!isChoosedDir)
            {
                ChooseDir();
            }
            if(state == "flying")
            {
                if (Math.Abs(dirX - x) < speed * 2 && Math.Abs(dirY - y) < speed * 2)
                {
                    Explosion();
                }
                MoveTo();
            }
        }

        private void Explosion()
        {
            state = "explosion";
            for(int i = 0; i < objectsList.Count(); i++)
            {
                if(Math.Abs(objectsList[i].x - x) < 100 && Math.Abs(objectsList[i].y - y) < 100)
                {
                    if(objectsList[i] is Creature)
                    {
                        Creature creature = (Creature)objectsList[i];
                        creature.Death();
                        i--;
                    }
                    else if (objectsList[i] is Food)
                    {
                        Food food = (Food)objectsList[i];
                        food.SpawnFoodNearFood();
                        food.Death();
                        i--;
                    }
                }
            }
            
        }
        public virtual void ExplosionTimer()
        {
            if(explosionTime <= 0)
            {
                state = "none";
                isLaunched = false;
                isChoosedDir = false;
                explosionTime = 100;

                map[0, 0] = map[x, y];
                map[x, y] = new Cell(
                    x: x,
                    y: y
                    );
                x = 0;
                y = 0;
            }
            else
            {
                explosionTime--;
            }
        }

        private void ChooseDir()
        {
            for(int i = 0; i < objectsList.Count(); i++)
            {
                int amountOfNeighbours = 0;
                if(objectsList[i] is Creature)
                {
                    for (int j = 0; j < objectsList.Count(); j++)
                    {
                        if (objectsList[j] is Creature)
                        {
                            if (Math.Abs(objectsList[i].x - objectsList[j].x) <= 50
                            && Math.Abs(objectsList[i].x - objectsList[j].x) <= 50)
                            {
                                amountOfNeighbours++;
                                if (amountOfNeighbours >= 100)
                                {
                                    dirX = objectsList[i].x;
                                    dirY = objectsList[i].y;
                                    isChoosedDir = true;
                                    state = "flying";
                                    break;
                                }
                            }
                        }
                    }
                }
                if(state == "flying")
                {
                    break;
                }
            }
        }

        public void MoveTo()
        {
            if (dirX < x)
            {
                MoveByShift(-speed, 0);
            }
            if (dirX > x)
            {
                MoveByShift(speed, 0);
            }
            if (dirY < y)
            {
                MoveByShift(0, -speed);
            }
            if (dirY > y)
            {
                MoveByShift(0, speed);
            }
        }

    }
}
