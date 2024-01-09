﻿namespace LifeSimulation11
{
    public abstract class MoveableEntity : Cell
    {
        public static object[,] map;
        public MoveableEntity(int x, int y, object[,] mapObject) : base(x, y)
        {
            map = mapObject;
        }

        public void MoveByShift(int shiftX, int shiftY)
        {
            int newPosX = x + shiftX;
            int newPosY = y + shiftY;
            if (isBeyondMapBoundaries(newPosX, newPosY))
            {
                return;
            }

            var objectOnFront = map[newPosX, newPosY];

            if (objectOnFront is Food)
            {
                MoveTowardsFoodBehaviour((Food)objectOnFront);
            }
            else if (objectOnFront is Cell && !(objectOnFront is Creature))
            {
                map[x, y] = new Cell(x, y);
                x = newPosX;
                y = newPosY;
            }
        }
        public virtual void MoveTowardsFoodBehaviour(Food food) { }

        public static bool isBeyondMapBoundaries(int x, int y)
        {
            if (x <= 0 || x >= World.MAP_SIZE_X || y <= 0 || y >= World.MAP_SIZE_Y)
            {
                return true;
            }
            return false;
        }

    }
}
