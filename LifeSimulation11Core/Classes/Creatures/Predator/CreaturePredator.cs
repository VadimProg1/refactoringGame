using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation11
{
    public abstract class CreaturePredator<HFood, OFood> : Creature, ICreaturePredator
    {       
        public CreaturePredator(int x, int y, bool gender, Random randomm, List<Cell> objectsListt,
           object[,] mapp) : base(x, y, gender, randomm, objectsListt, mapp)
        {
            satiety = 350;
            timeAfterKids = 30;
            AfterKidsCooldown = 60;
            thresholdValue = 300;
            foodBonus = 350;
            maxSatiety = satiety;
        }

        public override abstract Creature FactoryMethod(bool babyGender, int razbrosX, int razbrosY);

        public abstract void SearchLoveFactory();

        public abstract void CheckMyLoveFactory();

        public override void Activate()
        {
            int randDirection = random.Next(4);
            int randMove = random.Next(10);
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
                    SearchLoveFactory();
                }
                else if (!isAlone && timeAfterKids <= 0)
                {
                    CheckMyLoveFactory();
                }

                if (satiety > thresholdValue && (isAlone || timeAfterKids > 0))
                {
                    RandomMove(randDirection);
                }
                else if (satiety > thresholdValue && !isAlone)
                {
                    MoveToPositionByOneStep(love.x, love.y);
                }
                else
                {
                    SearchForFood<HFood, OFood>();
                }
            }
            satiety--;
            timeAfterKids--;
        }
    }
}
