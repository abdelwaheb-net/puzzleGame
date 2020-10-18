using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzle
{
    public partial class Form1 : Form
    {
        Point EmptyPoint;
        ArrayList images = new ArrayList();
        public Form1()
        {
            InitializeComponent();
            EmptyPoint.X = 180;
            EmptyPoint.Y = 180;
        }

        private void btnstratgame_Click(object sender, EventArgs e)
        {
            foreach (Button b  in panel1.Controls)
            
                b.Enabled = true;
                Image original = Image.FromFile(@"C:\Users\Public\Pictures\Sample Pictures\Koala.jpg");

                CropImageToImages(original, 270, 270);
                AddImagesToButtons(images);

            
        }

        private void AddImagesToButtons(ArrayList images)
        {
            int i=0;
            int [] arr={0,1,2,3,4,5,6,7};
            arr=Suffle(arr);
            foreach (Button b in panel1.Controls)
            {
                if (i < arr.Length)
                {
                    b.Image = (Image)images[arr[i]];
                    i++;
                }
           
            }

        }

        private int[] Suffle(int[] arr)
        {
            Random rand = new Random();
            arr = arr.OrderBy(x => rand.Next()).ToArray();
            return arr;
        }

        private void CropImageToImages(Image original, int W, int H)
        {
            Bitmap bmp = new Bitmap(W, H);
            Graphics graphic = Graphics.FromImage(bmp);
            graphic.DrawImage(original, 0, 0, W, H);
            graphic.Dispose();
            int mov_r = 0, mov_d = 0;  
            for (int x = 0; x < 8; x++)
            {
                Bitmap piece = new Bitmap(90, 90);
                for (int i = 0; i < 90; i++)
                
                    for (int j = 0; j < 90; j++)

                        piece.SetPixel(i, j, bmp.GetPixel(i + mov_r, j + mov_d));
                images.Add(piece);
                mov_r += 90;
                if (mov_r == 270)
                {
                    mov_r = 0;
                    mov_d += 90;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MoveButton((Button)sender);
        }

        private void MoveButton(Button btn)
        {
            if ((((btn.Location.X == EmptyPoint.X - 90 || btn.Location.X == EmptyPoint.X + 90)
                && btn.Location.Y == EmptyPoint.Y)) || ((btn.Location.Y == EmptyPoint.Y - 90 || btn.Location.Y == EmptyPoint.Y + 90)
                && btn.Location.X == EmptyPoint.X))
            {
                Point swap = btn.Location;
                btn.Location = EmptyPoint;
                EmptyPoint = swap;
            }

            if (EmptyPoint.X == 180 && EmptyPoint.Y == 180)
                checkvalid();
        }

        private void checkvalid()
        {
            int count = 0, index;
            foreach (Button btn  in panel1.Controls)
            {
                index = (btn.Location.Y / 90) * 3 + btn.Location.X / 90;
                if (images[index] == btn.Image)
                    count++;

            }
            if (count == 8)
                MessageBox.Show("Well done you win !");
        }

    

    }
}
