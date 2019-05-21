using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace DrawTest.Forms
{
    public partial class Form1 : Form
    {
        ArrayList al = new ArrayList();
        public Form1()
        {
            InitializeComponent();
        }

        private bool _canDraw;
        private int _startX, _startY;
        private Rectangle _rect;
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //The system is now allowed to draw rectangles
            _canDraw = true;
            //Initialize and keep track of the start position
            _startX = e.X;
            _startY = e.Y;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            //The system is no longer allowed to draw rectangles
            if (!_canDraw) return;

            //The x-value of our rectangle should be the minimum between the start x-value and the current x-position
            int x = Math.Min(_startX, e.X);
            //The y-value of our rectangle should also be the minimum between the start y-value and current y-value
            int y = Math.Min(_startY, e.Y);

            //The width of our rectangle should be the maximum between the start x-position and current x-position minus
            //the minimum of start x-position and current x-position
            int width = Math.Max(_startX, e.X) - Math.Min(_startX, e.X);

            //For the hight value, it's basically the same thing as above, but now with the y-values:
            int height = Math.Max(_startY, e.Y) - Math.Min(_startY, e.Y);
            _rect = new Rectangle(x, y, width, height);
            al.Add(_rect);
            
            _canDraw = false;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //If we are not allowed to draw, simply return and disregard the rest of the code
            if (!_canDraw) return;

            //The x-value of our rectangle should be the minimum between the start x-value and the current x-position
            int x = Math.Min(_startX, e.X);
            //The y-value of our rectangle should also be the minimum between the start y-value and current y-value
            int y = Math.Min(_startY, e.Y);

            //The width of our rectangle should be the maximum between the start x-position and current x-position minus
            //the minimum of start x-position and current x-position
            int width = Math.Max(_startX, e.X) - Math.Min(_startX, e.X);

            //For the hight value, it's basically the same thing as above, but now with the y-values:
            int height = Math.Max(_startY, e.Y) - Math.Min(_startY, e.Y);
            _rect = new Rectangle(x, y, width, height);
            //Refresh the form and draw the rectangle
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //Create a new 'pen' to draw our rectangle with, give it the color red and a width of 2
            using (Pen pen = new Pen(Color.Red, 2))
            {
                //Draw the rectangle on our form with the pen
                if (_canDraw)
                {
                    e.Graphics.DrawRectangle(pen, _rect);
                }
         
                    foreach (Rectangle rect in al)
                    {
                        e.Graphics.DrawRectangle(pen, rect);
                    }
         
                
                //e.Graphics.DrawRectangle(pen, _rect);
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //ctrl+z撤销
            if (e.KeyChar == 26)
            {
                if (al.Count > 0)
                {
                    al.RemoveAt(al.Count - 1);
                    Refresh();
                }
                
            }
            else
            {
                Close();
            }
            
        }
    }
}