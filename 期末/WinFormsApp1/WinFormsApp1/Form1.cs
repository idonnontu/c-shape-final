using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        class plate
        {
            public string[] block = new string[] { "", "", "", "" };
            public List<PictureBox> brick = new List<PictureBox>();
            public void showBrick()
            {
                for(int i = 0; i < 4; i++)
                {
                    brick[i].SizeMode = PictureBoxSizeMode.StretchImage;
                    brick[i].Image = new Bitmap("..\\..\\..\\image\\brick\\" + block[i]);
                }
            }
            public int sameColorNum(string color)
            {
                int sameColorNum = 0;
                for(int i = 0; i < 4; i++)
                {
                    if (block[i] == color)
                    {
                        sameColorNum += 1;
                    }
                }
                return sameColorNum;
            }
            public void takeAway(string color, List<PictureBox> tempList)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (block[i] == color)
                    {
                        block[i] = "";
                    }
                }
                for (int i = 3; i >= 0; i--)
                {
                    try
                    {
                        if (brick[i].Tag.ToString().Split(',')[3] == color)
                        {
                            tempList.Add(brick[i]);
                        }
                    }
                    catch
                    {

                    }
                }
                for (int i = 3; i >= 0; i--)
                {
                    try
                    {
                        if (brick[i].Tag.ToString().Split(',')[3] == color)
                        {
                            brick.RemoveAt(i);
                        }
                    }
                    catch
                    {

                    }
                }
            }
        }
        class bigBoard
        {
            public PictureBox boardImage;
            public List<PictureBox> brickList = new List<PictureBox>();
            public int[] xAxis = { 417, 350, 284, 224, 160};
            public int[] yAxis = { 525, 590, 655, 720, 785};
            
            public string[][] leftColor = new string[][]
            {
                new string[]{""},
                new string[]{"", ""},
                new string[]{"", "", ""},
                new string[]{"", "", "", ""},
                new string[]{"", "", "", "", ""},
            };
            public void addColor(int row, List<PictureBox> tempBrick)
            {
                string tempBrickColor = tempBrick[0].Tag.ToString().Split(',')[3];
                string rowColor = leftColor[row][row];
                int tempBrickCount = tempBrick.Count;
                if (rowColor == "" || rowColor == tempBrickColor)
                {
                    for(int i = 0; i < tempBrickCount; i++)
                    {
                        pushOneColor(row, tempBrick);
                    }
                }
            }
            public void pushOneColor(int row, List<PictureBox> tempBrick)
            {
                int targetId = 10;
                for(int i = 0; i < leftColor[row].Length; i++)
                {
                    if(leftColor[row][i] == "")
                    {
                        targetId = i;
                        break;
                    }
                }
                if(targetId < 9)
                {
                    leftColor[row][targetId] = tempBrick[0].Tag.ToString().Split(',')[3];
                    tempBrick[0].Location = new Point(xAxis[targetId], yAxis[row]);
                    tempBrick[0].Enabled = false;
                    brickList.Add(tempBrick[0]);
                    tempBrick.RemoveAt(0);
                }
            }
        }
        List<plate> plateList = new List<plate>();
        PictureBox dragColor;
        bigBoard board = new bigBoard();
        int posX, posY;
        bool mouseDrag = false;
        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;
            setInitPicture();
            setAllPlate();
        }
        public void setInitPicture()
        {
            Bitmap map = new Bitmap("..\\..\\..\\image\\mid_plate.PNG");
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox7.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = map;
            pictureBox2.Image = map;
            pictureBox3.Image = map;
            pictureBox4.Image = map;
            pictureBox5.Image = map;
            pictureBox6.Image = map;
            pictureBox7.Image = map;
            map = new Bitmap("..\\..\\..\\image\\big_plate.PNG");
            pictureBox8.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox8.Image = map;
        }
        public void setBackgroundToBack()
        {
            pictureBox1.SendToBack();
            pictureBox2.SendToBack();
            pictureBox3.SendToBack();
            pictureBox4.SendToBack();
            pictureBox5.SendToBack();
            pictureBox6.SendToBack();
            pictureBox7.SendToBack();
            pictureBox8.SendToBack();
        }
        public int[] plateLocation(int id)
        {
            switch (id)
            {
                case  0:
                    return new int[2] {0, 0};
                case 1:
                    return new int[2] {255, 0};
                case 2:
                    return new int[2] { 255*2, 0 };
                case 3:
                    return new int[2] { 255*3, 0 };
                case 4:
                    return new int[2] { 255*4, 0 };
                case 5:
                    return new int[2] { 255*5, 0 };
                default:
                    return new int[2] { 255*6, 0 };

            }
        }
        public void setAllPlate()
        {
            string[] tempColor = new string[] { "red.PNG", "yellow.PNG", "blue.PNG", "black.PNG", "white.PNG"};
            Random rand = new Random();
            for(int i = 0; i < 7; i++)
            {
                int[] plateLoc = plateLocation(i);
                plate tempPlate = new plate();
                for(int j = 0; j < 4; j++)
                {
                    tempPlate.block[j] = tempColor[rand.Next(0, 5)];
                    PictureBox tempBrick = new PictureBox();                    
                    tempBrick.Size = new Size(55, 55);
                    if (j == 0)
                    {
                        tempBrick.Location = new Point(plateLoc[0] + 65, plateLoc[1] + 65);
                    }
                    else if (j==1)
                    {
                        tempBrick.Location = new Point(plateLoc[0] + 126, plateLoc[1] + 65);
                    }
                    else if (j==2)
                    {
                        tempBrick.Location = new Point(plateLoc[0] + 65, plateLoc[1] + 126);
                    }
                    else
                    {
                        tempBrick.Location = new Point(plateLoc[0] + 126, plateLoc[1] + 126);
                    }
                    tempBrick.Tag = i.ToString() + ',' + tempBrick.Location.X.ToString() + ',' + tempBrick.Location.Y.ToString() + ',' + tempPlate.block[j];
                    tempPlate.brick.Add(tempBrick);
                    tempBrick.MouseDown += new MouseEventHandler(changeMouseDrag);
                    tempBrick.MouseMove += new MouseEventHandler(brickMove);
                    tempBrick.MouseUp += new MouseEventHandler(releaseMouse);
                    this.Controls.Add(tempBrick);
                }
                plateList.Add(tempPlate);
                tempPlate.showBrick();
                setBackgroundToBack();
            }            
        }
        public void changeMouseDrag(object obj, MouseEventArgs e)
        {
            mouseDrag = true;
            dragColor = (PictureBox)obj;
            label1.Text = dragColor.Tag.ToString();
            label2.Text = "press";
            posX = Cursor.Position.X;
            posY = Cursor.Position.Y;
        }
        public void releaseMouse(object obj, MouseEventArgs e)
        {
            string[]  tempPos = dragColor.Tag.ToString().Split(',');
            bool tempbool = checkReleaseRange(obj, MousePosition.X, MousePosition.Y);
            if (!tempbool)
            {
                dragColor.Location = new Point(int.Parse(tempPos[1]), int.Parse(tempPos[2]));
            }
            mouseDrag = false;
            dragColor = null;
            label1.Text = "";
            label2.Text = "release";
        }
        public void brickMove(object obj, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left && mouseDrag == true)
            {
                int deltaX =  Cursor.Position.X - posX;
                int deltaY = Cursor.Position.Y - posY;
                //dragColor.Location = new Point(dragColor.Location.X + deltaX, dragColor.Location.Y + deltaY);
                dragColor.Location = new Point(MousePosition.X-27, MousePosition.Y-55);
                posX = Cursor.Position.X;
                posY = Cursor.Position.Y;
                label3.Text = MousePosition.X.ToString() + "," + MousePosition.Y.ToString();
            }
            
        }
        public bool checkReleaseRange(object obj, int mouseX, int mouseY)
        {
            PictureBox temp = (PictureBox)obj;
            string color = temp.Tag.ToString().Split(',')[3];
            int plateId = int.Parse(temp.Tag.ToString().Split(',')[0]);
            label5.Text = plateList.Count.ToString();
            int sameColorNum = plateList[plateId].sameColorNum(color);
            List<PictureBox> tempBrick = new List<PictureBox>();
            if (mouseX > 417 && mouseX < 470 && mouseY > 553 && mouseY < 606)
            {
                plateList[plateId].takeAway(color, tempBrick);
                board.addColor(0, tempBrick);
                return true;
            }
            else if(Cursor.Position.X > 350 && Cursor.Position.X < 470 && Cursor.Position.Y > 616 && Cursor.Position.Y < 670)
            {
                plateList[plateId].takeAway(color, tempBrick);
                board.addColor(1, tempBrick);
                return true;
            }
            else if (Cursor.Position.X > 284 && Cursor.Position.X < 470 && Cursor.Position.Y > 687 && Cursor.Position.Y < 741)
            {
                plateList[plateId].takeAway(color, tempBrick);
                board.addColor(2, tempBrick);
                return true;
            }
            else if (Cursor.Position.X > 216 && Cursor.Position.X < 470 && Cursor.Position.Y > 748 && Cursor.Position.Y < 805)
            {
                plateList[plateId].takeAway(color, tempBrick);
                board.addColor(3, tempBrick);
                return true;
            }
            else if (Cursor.Position.X > 152 && Cursor.Position.X < 470 && Cursor.Position.Y > 822 && Cursor.Position.Y < 873)
            {
                plateList[plateId].takeAway(color, tempBrick);
                board.addColor(4, tempBrick);
                return true;
            }
            
            for(int i = 0; i < board.brickList.Count; i++)
            {
                this.Controls.Add(board.brickList[i]);
            }
            
            setBackgroundToBack();
            return false;
        }

    }
}
