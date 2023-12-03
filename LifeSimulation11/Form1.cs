using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LifeSimulation11
{
    public partial class Form1 : Form
    {
        private GameGraphics gameGraphics;
        private World world;
        private int resolution = 3;

        public Form1()
        {
            InitializeComponent();       
        }

        private void StartGame()
        {
            if(timer1.Enabled)
            {
                return;
            }

            resolution = (int)numResolution.Value;
            pictureBox1.Image = new Bitmap(1000 * resolution, 1000 * resolution);
            numFood.Enabled = false;
            numMarios.Enabled = false;

            world = new World
            (
                amountOfPopulation: (int)numMarios.Value,
                amountOfFood: (int)numFood.Value
            );

            gameGraphics = new GameGraphics
            (
                graphics: Graphics.FromImage(pictureBox1.Image),
                world: world,
                resolution: resolution
            );            
            timer1.Start();
        }

        private void RefreshGraphics()
        {
            gameGraphics.Refresh();
            pictureBox1.Refresh();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            RefreshGraphics();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
            {
                return;
            }
            timer1.Stop();
            numFood.Enabled = true;
            numMarios.Enabled = true;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!timer1.Enabled)
            {
                return;
            }
            if (e.Button == MouseButtons.Left)
            {
                var x = (int)(e.Location.X / resolution);
                var y = (int)(e.Location.Y / resolution);
                int result = world.GetCreatureSatiety(x, y);
                if(result > 0)
                {
                    textCreatureSatiety.Text = result.ToString();
                }
                else
                {
                    textCreatureSatiety.Text = "";
                }
                
            }
        }

        private void numResolution_ValueChanged(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
            {
                return;
            }
            int newRes = (int)numResolution.Value;
            resolution = newRes;
            pictureBox1.Image = new Bitmap(1000 * newRes, 1000 * newRes);
            gameGraphics.graphics = Graphics.FromImage(pictureBox1.Image);
            gameGraphics.resolution = newRes;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
