using LifeSimulation11Core.Interfaces;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

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
                if (objectsToDraw[i] is IColorSprite)
                {
                    IColorSprite sprite = (IColorSprite)objectsToDraw[i];
                    graphics.FillRectangle(sprite.GetColor(), sprite.GetSpritePosX() * resolution, sprite.GetSpritePosY() * resolution, resolution, resolution);
                }
                else if (objectsToDraw[i] is IImageSprite)
                {
                    IImageSprite sprite = (IImageSprite)objectsToDraw[i];
                    Image image = sprite.GetImage();
                    if (image != null)
                    {
                        graphics.DrawImage(image, sprite.GetSpritePosX() * resolution - (image.Size.Width / 2), sprite.GetSpritePosY() * resolution - (image.Size.Height / 2), image.Size.Width, image.Size.Height);
                    }
                }
            }
        }
        
    }
}
