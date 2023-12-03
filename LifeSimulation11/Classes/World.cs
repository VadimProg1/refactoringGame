using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation11
{
    class World
    {
        int amountOfPopulation;
        int amountOfFood;
        private object[,] map;
        private List<Cell> objectsList = new List<Cell>();
        Random random = new Random();
        Nuke nuke;

        public World(int amountOfPopulation, int amountOfFood)
        {
            this.amountOfPopulation = amountOfPopulation;
            this.amountOfFood = amountOfFood;
            WorldGeneration();
        }

        void WorldGeneration()
        {
            map = new object[1000, 1000];
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for(int x = 0; x < map.GetLength(1); x++)
                {
                    map[x, y] = new Cell(
                        x: x,
                        y: y
                        );
                }
            }
            SpawnCreatures();
            SpawnFood();
            SpawnNuke();
        }

        public Food CreateRandomFood(int randX, int randY)
        {
            int randFood = random.Next(3);

            switch (randFood)
            {
                case 0:
                    return new Cucumber(
                                x: randX,
                                y: randY,
                                randomm: random,
                                objectsListt: objectsList,
                                mapp: map
                                );

                case 1:
                    return new Apple(
                                x: randX,
                                y: randY,
                                randomm: random,
                                objectsListt: objectsList,
                                mapp: map
                                );

                case 2:
                    return new Carrot(
                                x: randX,
                                y: randY,
                                randomm: random,
                                objectsListt: objectsList,
                                mapp: map
                                );
            }
            return null;
        }

        void SpawnFood()
        {
            for (int i = 0; i < 5; i++) 
            {
                bool check = false;
                while (!check)
                {
                    int randX = random.Next(1000);
                    int randY = random.Next(1000);
                    if (map[randX, randY] is Cell)
                    {
                        check = true;
                        Food newFood = CreateRandomFood(randX, randY);
                        
                        map[randX, randY] = newFood;
                        objectsList.Add(newFood);
                    }
                }
            }
            for(int i = 0; i < amountOfFood - 5; i++)
            {
                Food newFood = CreateRandomFood(1, 1);
                newFood.SpawnFoodNearFood();
            }
            
        }

        Creature CreateRandomCreature(int randX, int randY)
        {
            int randGender = random.Next(2);
            bool gender;
            if (randGender == 0)
            {
                gender = false;
            }
            else
            {
                gender = true;
            }
            int randCreature = random.Next(10);

            switch (randCreature)
            {
                case 0:
                    return new Lion<IBigAnimal, ISmallAnimal>(
                                 x: randX,
                                 y: randY,
                                 gender: gender,
                                 randomm: random,
                                 objectsListt: objectsList,
                                 mapp: map
                                 );

                case 1:
                    return new Wolf<IBigAnimal, ISmallAnimal>(
                                 x: randX,
                                 y: randY,
                                 gender: gender,
                                 randomm: random,
                                 objectsListt: objectsList,
                                 mapp: map
                                 );

                case 2:
                    return new Fox<ISmallAnimal, ICreatureHerbivore>(
                                 x: randX,
                                 y: randY,
                                 gender: gender,
                                 randomm: random,
                                 objectsListt: objectsList,
                                 mapp: map
                                 );

                case 3:
                    return new Rat<ISmallAnimal, ICreatureHerbivore, Food>(
                                 x: randX,
                                 y: randY,
                                 gender: gender,
                                 randomm: random,
                                 objectsListt: objectsList,
                                 mapp: map
                                 );

                case 4:
                    return new Pig<ICreaturePredator, IBigAnimal, Food>(
                                 x: randX,
                                 y: randY,
                                 gender: gender,
                                 randomm: random,
                                 objectsListt: objectsList,
                                 mapp: map
                                 );

                case 5:
                    return new Human<ISmallAnimal, IBigAnimal, Food>(
                                  x: randX,
                                  y: randY,
                                  gender: gender,
                                  randomm: random,
                                  objectsListt: objectsList,
                                  mapp: map
                                  );

                case 6:
                    return new Bear<ISmallAnimal, ICreatureOmnivorous, Food>(
                                 x: randX,
                                 y: randY,
                                 gender: gender,
                                 randomm: random,
                                 objectsListt: objectsList,
                                 mapp: map
                                 );

                case 7:
                    return new Horse<Food>(
                                 x: randX,
                                 y: randY,
                                 gender: gender,
                                 randomm: random,
                                 objectsListt: objectsList,
                                 mapp: map
                                 );

                case 8:
                    return new Rabbit<Food>(
                                 x: randX,
                                 y: randY,
                                 gender: gender,
                                 randomm: random,
                                 objectsListt: objectsList,
                                 mapp: map
                                 );
                case 9:
                    return new Camel<Food>(
                                 x: randX,
                                 y: randY,
                                 gender: gender,
                                 randomm: random,
                                 objectsListt: objectsList,
                                 mapp: map
                                 );
            }
            return null;
        }
        void SpawnCreatures()
        {
            for (int i = 0; i < amountOfPopulation; i++) 
            {
                bool check = false;
                while (!check)
                {
                    int randX = random.Next(1000);
                    int randY = random.Next(1000);
                    if (map[randX, randY] is Cell)
                    {
                        check = true;
                        Creature newCreature = CreateRandomCreature(randX, randY);
                        map[randX, randY] = newCreature;
                        objectsList.Add(newCreature);
                    }
                }
            }

        }

        void SpawnNuke()
        {
            nuke = new Nuke(
                        x: 0,
                        y: 0,
                        randomm: random,
                        objectsListt: objectsList,
                        mapp: map
                        );
            objectsList.Add(nuke);
        }

        public void UpdateWorld()
        {
            for(int i = 0; i < objectsList.Count(); i++)
            {
                if(objectsList[i] is Creature)
                {
                    Creature creature = (Creature)objectsList[i];
                    creature.Activate();
                }
            }
            nuke.Activate();
        }

        public int GetCreatureSatiety(int x, int y)
        {
            int r = 3;
            for (int y1 = y - r; y1 < y + r; y1++)
            {
                for (int x1 = x - r; x1 < x + r; x1++)
                {
                    if(x1 >= 0 && x1 < 1000 && y1 >= 0 && y1 < 1000)
                    {
                        if (map[x1, y1] is Creature)
                        {
                            Creature tempC = (Creature)map[x1, y1];
                            return tempC.satiety;
                        }
                    }            
                }
            }
            return 0;
        }

        public List<Cell> GetObjects()
        {
            return objectsList;
        }
    }
}
