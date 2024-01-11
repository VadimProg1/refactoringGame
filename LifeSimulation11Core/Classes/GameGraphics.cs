using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LifeSimulation11
{
    class GameGraphics
    {
        public Graphics graphics;
        public int resolution;
        Image explosionImage = Image.FromFile("D:/Source/Repos/OOP-LifeSimulation/LifeSimulation11/nuclear.png");
        Image bombImage = Image.FromFile("D:/Source/Repos/OOP-LifeSimulation/LifeSimulation11/nuclearbomb.png");
        Image houseImage = Image.FromFile("D:/Source/Repos/OOP-LifeSimulation/LifeSimulation11/house-0.png");
        public GameGraphics(Graphics graphics, int resolution)
        {
            this.graphics = graphics;
            this.resolution = resolution;          
        }
        public void DrawGameObjects(List<Cell> objectsToDraw)
        {
            graphics.Clear(Color.Green);
            for(int i = 0; i < objectsToDraw.Count(); i++)
            {
                if (objectsToDraw[i] is Creature)
                {
                    Creature creature = (Creature)objectsToDraw[i];
                    graphics.FillRectangle(creature.GetCreatureColor(), objectsToDraw[i].x * resolution, objectsToDraw[i].y * resolution, resolution, resolution);
                }
                else if (objectsToDraw[i] is Food)
                {
                    Food food = (Food)objectsToDraw[i];
                    graphics.FillRectangle(food.GetFoodColor(), objectsToDraw[i].x * resolution, objectsToDraw[i].y * resolution, resolution, resolution);
                }
                else if (objectsToDraw[i] is Nuke)
                {
                    Nuke nuke = (Nuke)objectsToDraw[i];
                    if (nuke.state == Nuke.NukeStates.flying)
                    {
                        graphics.DrawImage(bombImage, objectsToDraw[i].x * resolution, objectsToDraw[i].y * resolution, 100, 100);
                    }
                    else if (nuke.state == Nuke.NukeStates.explosion)
                    {
                        graphics.DrawImage(explosionImage, objectsToDraw[i].x * resolution - 500, objectsToDraw[i].y * resolution - 500, World.MAP_SIZE_X, World.MAP_SIZE_Y);
                    }
                }
                else if(objectsToDraw[i] is House)
                {
                    graphics.DrawImage(houseImage, objectsToDraw[i].x * resolution - 10, objectsToDraw[i].y * resolution - 10, 20, 20);
                }
            }
        }
        
    }
}
