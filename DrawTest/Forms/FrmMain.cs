using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace DrawTest.Forms
{
    public partial class Form1 : Form
    {
        ArrayList al = new ArrayList();
        ArrayList sharpes = new ArrayList();
        public Form1()
        {
            InitializeComponent();
        }

        private bool _canDraw;
        private int _startX, _startY;
        private Rectangle _rect;
        private string _drawType="Rectangle";
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

            int x, y, width, height;
            switch (_drawType)
            {
                case "Oval":
                    //The x-value of our rectangle should be the minimum between the start x-value and the current x-position
                    x = Math.Min(_startX, e.X);
                    //The y-value of our rectangle should also be the minimum between the start y-value and current y-value
                    y = Math.Min(_startY, e.Y);

                    //The width of our rectangle should be the maximum between the start x-position and current x-position minus
                    //the minimum of start x-position and current x-position
                    width = Math.Max(_startX, e.X) - Math.Min(_startX, e.X);

                    //For the hight value, it's basically the same thing as above, but now with the y-values:
                    height = Math.Max(_startY, e.Y) - Math.Min(_startY, e.Y);
                    _rect = new Rectangle(x, y, width, height);
                    break;
                default:
                    //The x-value of our rectangle should be the minimum between the start x-value and the current x-position
                    x = Math.Min(_startX, e.X);
                    //The y-value of our rectangle should also be the minimum between the start y-value and current y-value
                    y = Math.Min(_startY, e.Y);

                    //The width of our rectangle should be the maximum between the start x-position and current x-position minus
                    //the minimum of start x-position and current x-position
                    width = Math.Max(_startX, e.X) - Math.Min(_startX, e.X);

                    //For the hight value, it's basically the same thing as above, but now with the y-values:
                    height = Math.Max(_startY, e.Y) - Math.Min(_startY, e.Y);
                    _rect = new Rectangle(x, y, width, height);
                    break;

            }
            sharpes.Add(_drawType);
            al.Add(_rect);
            
            _canDraw = false;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //If we are not allowed to draw, simply return and disregard the rest of the code
            if (!_canDraw) return;
            int x, y, width, height;
            switch (_drawType)
            {
                case "Oval":
                    //The x-value of our rectangle should be the minimum between the start x-value and the current x-position
                     x = Math.Min(_startX, e.X);
                    //The y-value of our rectangle should also be the minimum between the start y-value and current y-value
                     y = Math.Min(_startY, e.Y);

                    //The width of our rectangle should be the maximum between the start x-position and current x-position minus
                    //the minimum of start x-position and current x-position
                     width = Math.Max(_startX, e.X) - Math.Min(_startX, e.X);

                    //For the hight value, it's basically the same thing as above, but now with the y-values:
                     height = Math.Max(_startY, e.Y) - Math.Min(_startY, e.Y);
                    _rect = new Rectangle(x, y, width, height);
                    break;
                default:
                    //The x-value of our rectangle should be the minimum between the start x-value and the current x-position
                     x = Math.Min(_startX, e.X);
                    //The y-value of our rectangle should also be the minimum between the start y-value and current y-value
                     y = Math.Min(_startY, e.Y);

                    //The width of our rectangle should be the maximum between the start x-position and current x-position minus
                    //the minimum of start x-position and current x-position
                     width = Math.Max(_startX, e.X) - Math.Min(_startX, e.X);

                    //For the hight value, it's basically the same thing as above, but now with the y-values:
                     height = Math.Max(_startY, e.Y) - Math.Min(_startY, e.Y);
                    _rect = new Rectangle(x, y, width, height);
                    break;

            }
       
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
                    switch (_drawType)
                    {
                        case "Oval":
                            e.Graphics.DrawEllipse(pen, _rect);
                            break;
                        default:
                            e.Graphics.DrawRectangle(pen, _rect);
                            break;
                    }
                }
                if (al.Count == 0 | sharpes.Count == 0)
                    return;
                for(int i=0;i<al.Count;i++)
                {
                    string drawType =Convert.ToString(sharpes[i]);
                    Rectangle rect = (Rectangle)al[i];
                    switch (drawType)
                    {
                        case "Oval":
                            e.Graphics.DrawEllipse(pen, rect);
                            break;
                        default:
                            e.Graphics.DrawRectangle(pen, rect);
                            break;
                    }
                }
         
                    //foreach (Rectangle rect in al)
                    //{
                    //    e.Graphics.DrawRectangle(pen, rect);
                    //}
         
                
                //e.Graphics.DrawRectangle(pen, _rect);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _drawType = "Recttangle";
                }

        private void button2_Click(object sender, EventArgs e)
        {
            _drawType = "Oval";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (al.Count > 0)
            {
                al.RemoveAt(al.Count - 1);
                sharpes.RemoveAt(sharpes.Count - 1);
                Refresh();
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
                    sharpes.RemoveAt(sharpes.Count - 1);
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