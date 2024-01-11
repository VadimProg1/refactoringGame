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
using System.Windows.Forms.VisualStyles;

namespace LifeSimulation11
{
    class GameGraphics
    {
        public Graphics graphics;
        public int resolution;
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
                    Image image = nuke.GetImage();
                    if (image != null)
                    {
                        graphics.DrawImage(image, objectsToDraw[i].x * resolution - (image.Size.Width / 2), objectsToDraw[i].y * resolution - (image.Size.Height / 2), image.Size.Width, image.Size.Height);
                    }
                    
                }
                else if (objectsToDraw[i] is House)
                {
                    House house = (House)objectsToDraw[i];
                    Image image = house.GetImage();
                    if (image != null)
                    {
                        graphics.DrawImage(image, objectsToDraw[i].x * resolution - (image.Size.Width / 2), objectsToDraw[i].y * resolution - (image.Size.Height / 2), image.Size.Width, image.Size.Height);
                    }
                }
            }
        }
        
    }
}
