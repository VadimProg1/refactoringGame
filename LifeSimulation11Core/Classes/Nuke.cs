using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation11
{
    public class Nuke : Cell
    {
        public static Random random;
        public static object[,] map;
        public static List<Cell> objectsList;
        static public int eventPropability = 1200;
        public bool isLaunched = false;
        bool isChoosedDir = false;
        int dirX = 1000;
        int dirY = 1000;
        public string state = "none";
        int explosionTime = 50;
        int speed = 6;
        public Nuke(int x, int y, Random randomm, object[,] mapp, List<Cell> objectsListt) : base(x, y)
        {
            random = randomm;
            objectsList = objectsListt;
            map = mapp;
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
                MoveLeft();
            }
            if (dirX > x)
            {
                MoveRight();
            }
            if (dirY < y)
            {
                MoveUp();
            }
            if (dirY > y)
            {
                MoveDown();
            }
        }

        public void MoveLeft()
        {
            if (x - speed > 0)
            {
                if (map[x - speed, y] is Creature)
                {

                }
                else if (map[x - speed, y] is Cell)
                {
                    map[x - speed, y] = map[x, y];
                    map[x, y] = new Cell(
                        x: x,
                        y: y
                        );
                    x -= speed;
                }
            }
        }

        public void MoveRight()
        {
            if (x + speed < 1000)
            {
                if (map[x + speed, y] is Creature)
                {

                }
                else if (map[x + speed, y] is Cell)
                {
                    map[x + speed, y] = map[x, y];
                    map[x, y] = new Cell(
                        x: x,
                        y: y
                        );
                    x += speed;
                }
            }
        }

        public void MoveUp()
        {
            if (y - speed > 0)
            {
                if (map[x, y - speed] is Creature)
                {

                }
                else if (map[x, y - speed] is Cell)
                {
                    map[x, y - speed] = map[x, y];
                    map[x, y] = new Cell(
                        x: x,
                        y: y
                        );
                    y -= speed;
                }
            }
        }

        public void MoveDown()
        {
            if (y + speed < 1000)
            {
                if (map[x, y + speed] is Creature)
                {

                }
                else if (map[x, y + speed] is Cell)
                {
                    map[x, y + speed] = map[x, y];
                    map[x, y] = new Cell(
                        x: x,
                        y: y
                        );
                    y += speed;
                }
            }
        }
    }
}
