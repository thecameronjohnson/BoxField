using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoxField
{
    public partial class GameScreen : UserControl
    {
        //player1 button control keys
        Boolean leftArrowDown, rightArrowDown;

        //used to draw boxes on screen
        SolidBrush boxBrush = new SolidBrush(Color.White);

        //create a list to hold a column of boxes        
        List<Box> leftBoxes = new List<Box>();
        List<Box> rightBoxes = new List<Box>();

        Box player;

        Random randGen = new Random();

        int boxCounter;
        int boxSpeed = 5;

        public GameScreen()
        {
            InitializeComponent();
            OnStart();
        }



        public Color randomColour()
        {
            /*
            int red = randGen.Next(0, 255);
            int blue = randGen.Next(0, 255);
            int green = randGen.Next(0, 255);
            return Color.FromArgb(red, blue, green);*/
            int red = 0;
            int blue = 0;
            int green = 0;
            int randomSet = randGen.Next(0, 4);
            string chosenColour = Convert.ToString(randomSet);

            switch (chosenColour)
            {
                case "0":
                    red = 0;
                    green = 29;
                    blue = 255;
                    break;
                case "1":
                    red = 12;
                    green = 109;
                    blue = 232;
                    break;
                case "2":
                    red = 0;
                    green = 196;
                    blue = 255;
                    break;
                case "3":
                    red = 12;
                    green = 232;
                    blue = 210;
                    break;
                case "4":
                    red = 0;
                    green = 255;
                    blue = 140;
                    break;
            }

            return Color.FromArgb(red, green, blue);
            }

        /// <summary>
        /// Set initial game values here
        /// </summary>
        public void OnStart()
        {
            Color randomized = randomColour();

            //set game start values
            Box b1 = new Box(25, 24, 20, randomized);
            leftBoxes.Add(b1);

            Box b2 = new Box(leftBoxes[0].x + this.Height/3 , 24, 20, randomized);
            rightBoxes.Add(b2);

            Color playerColour = Color.FromArgb(255, 153, 0);

            player = new Box(400, 450, 20, playerColour);
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //player 1 button presses
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;           
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //player 1 button releases
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
            }
        }

        private void gameLoop_Tick(object sender, EventArgs e)
        {
            //update location of all boxes (drop down screen)
            foreach (Box b in leftBoxes)
            {
                b.Move(boxSpeed);
            }
            foreach (Box b in rightBoxes)
            {
                b.Move(boxSpeed);
            }

            if (leftArrowDown == true && player.x > 0)
            {
                player.Move(5, "left");
            }

            if (rightArrowDown == true && player.y <= this.Width - player.size)
            {
                player.Move(5, "right");
            }

            foreach(Box b in leftBoxes.Union(rightBoxes))
            {
                if (player.Collision(b))
                {
                    gameLoop.Stop();
                }
            }

            //remove box if it has gone of screen

            if (leftBoxes[0].y > this.Height - leftBoxes[0].size)
        {
                leftBoxes.RemoveAt(0);
                rightBoxes.RemoveAt(0);
        }
           
            //add new box if it is time
            boxCounter++;
            if (boxCounter % 5 == 0)
            {
                Color randomized = randomColour();

                Box b1 = new Box(25, 24, 20, randomized);
                leftBoxes.Add(b1);

                Box b2 = new Box(b1.x + this.Height / 3, 24, 20, randomized);
                rightBoxes.Add(b2);
                boxCounter = 0;
            }

            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            //draw boxes to screen
            foreach(Box b in leftBoxes)
            {
                boxBrush.Color = b.colour;
                e.Graphics.FillRectangle(boxBrush, b.x, b.y, b.size, b.size);
            }
            foreach (Box b in rightBoxes)
            {
                boxBrush.Color = b.colour;
                e.Graphics.FillRectangle(boxBrush, b.x, b.y, b.size, b.size);
            }

            boxBrush.Color = player.colour;
            e.Graphics.FillEllipse(boxBrush, player.x, player.y, player.size, player.size);
        }
    }
}
