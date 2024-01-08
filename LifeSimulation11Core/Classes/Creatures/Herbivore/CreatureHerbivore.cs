using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LifeSimulation11
{
    abstract class CreatureHerbivore<TFood> : Creature, ICreatureHerbivore
    {
        public CreatureHerbivore(int x, int y, bool gender, Random randomm, List<Cell> objectsListt,
            object[,] mapp) : base(x, y, gender, randomm, objectsListt, mapp)
        {
            satiety = 450;
            timeAfterKids = 10;
            AfterKidsCooldown = 50;
            thresholdValue = 400;
            foodBonus = 250;
            maxSatiety = satiety;
        }

        public abstract void SearchLoveFactory();

        public abstract void CheckMyLoveFactory();

        public override abstract Creature FactoryMethod(bool babyGender, int razbrosX, int razbrosY);

        public override void Activate()
        {
            int randDirection = random.Next(4);
            int randMove = random.Next(5);
            
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
                    SearchLove<CreatureHerbivore<Food>>();
                }
                else if (!isAlone && timeAfterKids <= 0)
                {
                    CheckMyLove<CreatureHerbivore<Food>>();
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
                    SearchForFood<TFood>();
                }
            }
            satiety--;
            timeAfterKids--;
        }
    }
}
