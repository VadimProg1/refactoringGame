using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation11
{
    abstract class CreatureOmnivorous<HFood, PFood, FFood> : Creature, ICreatureOmnivorous
    {
        public CreatureOmnivorous(int x, int y, bool gender, Random randomm, List<Cell> objectsListt,
           object[,] mapp) : base(x, y, gender, randomm, objectsListt, mapp)
        {
            satiety = 330;
            timeAfterKids = 30;
            AfterKidsCooldown = 60;
            thresholdValue = 250;
            foodBonus = 300;
            maxSatiety = satiety;
        }

        public abstract void SearchLoveFactory();

        public abstract void CheckMyLoveFactory();

        public override abstract Creature FactoryMethod(bool babyGender, int razbrosX, int razbrosY);
      
        public override void Activate()
        {
            int randDirection = random.Next(4);
            int randMove = random.Next(7);
            if (satiety <= 0)
            {
                Death();
            }

            if (randMove == 0)
            {
                RandomMove(randDirection);
            }
            else
            {
                if (isAlone && timeAfterKids <= 0)
                {
                    SearchLove<CreatureOmnivorous<HFood, PFood, FFood>>();
                }
                else if (!isAlone && timeAfterKids <= 0)
                {
                    CheckMyLove<CreatureOmnivorous<HFood, PFood, FFood>>();
                }

                if (satiety > thresholdValue && (isAlone || timeAfterKids > 0))
                {
                    RandomMove(randDirection);
                }
                else if (satiety > thresholdValue && !isAlone)
                {
                    MoveTo(love.x, love.y);
                }
                else
                {
                    SearchForFood<HFood, PFood, FFood>();
                }
            }
            satiety--;
            timeAfterKids--;
        }
    }
}
