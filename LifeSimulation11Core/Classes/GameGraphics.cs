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
        private World world;
        public int resolution;
        Image explosionImage = Image.FromFile("D:/Source/Repos/OOP-LifeSimulation/LifeSimulation11/nuclear.png");
        Image bombImage = Image.FromFile("D:/Source/Repos/OOP-LifeSimulation/LifeSimulation11/nuclearbomb.png");
        Image houseImage = Image.FromFile("D:/Source/Repos/OOP-LifeSimulation/LifeSimulation11/house-0.png");
        public GameGraphics(Graphics graphics, World world, int resolution)
        {
            this.graphics = graphics;
            this.world = world;
            this.resolution = resolution;          
        }

        public void Refresh()
        {
            RefreshGraphics();
            world.UpdateWorld();
        }

        private void RefreshGraphics()
        {
            Draw();          
        }

        private void Draw()
        {
            var objects = world.GetObjects();
            graphics.Clear(Color.Green);
            for(int i = 0; i < objects.Count(); i++)
            {
                if (objects[i] is Creature)
                {
                    Creature creature = (Creature)objects[i];
                    graphics.FillRectangle(creature.GetCreatureColor(), objects[i].x * resolution, objects[i].y * resolution, resolution, resolution);
                }
                else if (objects[i] is Food)
                {
                    Food food = (Food)objects[i];
                    graphics.FillRectangle(food.GetFoodColor(), objects[i].x * resolution, objects[i].y * resolution, resolution, resolution);
                }
                else if (objects[i] is Nuke)
                {
                    Nuke nuke = (Nuke)objects[i];
                    if (nuke.state == Nuke.NukeStates.flying)
                    {
                        graphics.DrawImage(bombImage, objects[i].x * resolution, objects[i].y * resolution, 100, 100);
                    }
                    else if (nuke.state == Nuke.NukeStates.explosion)
                    {
                        graphics.DrawImage(explosionImage, objects[i].x * resolution - 500, objects[i].y * resolution - 500, World.MAP_SIZE_X, World.MAP_SIZE_Y);
                    }
                }
                else if(objects[i] is House)
                {
                    graphics.DrawImage(houseImage, objects[i].x * resolution - 10, objects[i].y * resolution - 10, 20, 20);
                }
            }
        }
        
    }
}
