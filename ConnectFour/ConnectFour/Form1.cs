using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnectFour
{
    public partial class Form1 : Form
    {

        //columns in board
        private Rectangle[] boardColumns;
        //overall board dimensions
        private int[,] board;
        //for players to take turns
        private int turn;
        LinkedList<String> redPlayerScore = new LinkedList<String>();
        LinkedList<String> yellowPlayerScore = new LinkedList<String>();
        int r = 0;
        int y = 0;


        public Form1()
        {
            InitializeComponent();
            this.boardColumns = new Rectangle[7];
            this.board = new int[6, 7];
            this.Paint += new PaintEventHandler(Form1_Paint);
            this.turn = 1;
            lblRScore.Text = "";
            lblYScore.Text = "";
            DoubleBuffered = true;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //starting at 45 x&y to give a 5pxl margin
            //width/height calculated by sizing of index, so 66x7 + 5 for margin = 467, 66x6 + 15 for bottom margin = 411 (bottom is purposely bigger)
            e.Graphics.FillRectangle(Brushes.Blue, 25, 30, 467, 411);
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (i == 0)
                    {
                        // x = getting multiple columns going down in line with white empty coins
                        //y = inital board start margin
                        // 60 for coin size, 411 for overall width of board
                        this.boardColumns[j] = new Rectangle(30 + 66 * j, 35, 60, 411);
                    }
                    //66 because margin of 6 inbetween each coin of 60wxh
                    //66 * j - defines overall width of "empty" coin drawn to going horizontal
                    //66 * i - defines overall width of "empty" coin drawn to going vertical
                    // + 30 for margin of game wall (allows for 5pxl of blue)
                    e.Graphics.FillEllipse(Brushes.White, 30 + 66 * j, 35 + 66 * i, 60, 60);
                }
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            //getting location of mouse and seeing which column it's in
            int columnIndex = this.ColumnNumber(e.Location);

            //attempt to make a reset button on message box so that users cant continue to click a won game but couldn't figure it out
            //bool check = false;

            Graphics g = this.CreateGraphics();
            if (columnIndex != -1)
            {
                //if row is empty, allow for coin to be put in
                int rowIndex = this.EmptyRow(columnIndex);
                if (rowIndex != -1)
                {
                    //check which players turn it is
                    this.board[rowIndex, columnIndex] = this.turn;
                    if (this.turn == 1)
                    {
                        lblPlayer.Text = "Yellow Players Turn";
                        lblPlayer.ForeColor = System.Drawing.Color.Yellow;
                        lblPlayer.BackColor = System.Drawing.Color.Black;
                        //places red coin at the next empty position in column
                        g.FillEllipse(Brushes.Red, 30 + 66 * columnIndex, 35 + 66 * rowIndex, 60, 60);
                    }
                    else if (this.turn == 2)
                    {


                        lblPlayer.Text = "Red Players Turn";
                        lblPlayer.ForeColor = System.Drawing.Color.Red;
                        lblPlayer.BackColor = System.Drawing.Color.Black;
                        //places yellow coin at the next empty position in column
                        g.FillEllipse(Brushes.Yellow, 30 + 66 * columnIndex, 35 + 66 * rowIndex, 60, 60);
                    }

                    int winner = this.winnerPlayer(this.turn);

                    if (winner != -1)
                    {
                        String player = (winner == 1) ? "Red" : "Yellow";
                        //MessageBox.Show("Congratulations " + player + " Player!");

                        if (player == "Red")
                        {
                            redPlayerScore.Add("I");
                            lblRScore.Text = lblRScore.Text + redPlayerScore.Get(r);
                            r++;
                            lblPlayer.Text = "Red WINS!";
                            lblPlayer.ForeColor = System.Drawing.Color.Red;
                            lblPlayer.BackColor = System.Drawing.Color.Black;

                        }
                        else
                        {
                            yellowPlayerScore.Add("I");
                            lblYScore.Text = lblYScore.Text + yellowPlayerScore.Get(y);
                            y++;
                            lblPlayer.Text = "Yellow WINS!";
                            lblPlayer.ForeColor = System.Drawing.Color.Yellow;
                            lblPlayer.BackColor = System.Drawing.Color.Black;
                        }

                        switch (MessageBox.Show("Do you wish to continue the game?", "Congratulations " + player + " Player!", MessageBoxButtons.YesNo))
                        {
                            case DialogResult.Yes:
                                //check = true;
                                MessageBox.Show("Click the 'Reset Board' button to play the next round", "Reset the board");
                                ;
                                break;
                            case DialogResult.No:
                                Application.Exit();
                                ;
                                break;
                        }
                        //BoardReset();
                    }
                    if (this.turn == 1)
                    {
                        this.turn = 2;
                    }
                    else
                    {
                        this.turn = 1;
                    }
                }
            }
            /*if (check == true)
            {
                BoardReset();
            }*/
        }


        private int winnerPlayer(int playerToCheck)
        {
            //vertical win check (|)
            for (int row = 0; row < this.board.GetLength(0) - 3; row++)
            {
                for (int col = 0; col < this.board.GetLength(1); col++)
                {
                    if (this.allNumbersEqual(playerToCheck, this.board[row, col], this.board[row + 1, col], this.board[row + 2, col], this.board[row + 3, col]))
                    {
                        return playerToCheck;
                    }
                }
            }
            //horizontal win check (-)
            for (int row = 0; row < this.board.GetLength(0); row++)
            {
                for (int col = 0; col < this.board.GetLength(1) - 3; col++)
                {
                    if (this.allNumbersEqual(playerToCheck, this.board[row, col], this.board[row, col + 1], this.board[row, col + 2], this.board[row, col + 3]))
                        return playerToCheck;
                }
            }

            // top-left diagonal win check (\)
            for (int row = 0; row < this.board.GetLength(0) - 3; row++)
            {
                for (int col = 0; col < this.board.GetLength(1) - 3; col++)
                {
                    if (this.allNumbersEqual(playerToCheck, this.board[row, col], this.board[row + 1, col + 1], this.board[row + 2, col + 2], this.board[row + 3, col + 3]))
                        return playerToCheck;
                }
            }

            //top-right diagonal win check (/)
            for (int row = 0; row < this.board.GetLength(0) - 3; row++)
            {
                for (int col = 3; col < this.board.GetLength(1); col++)
                {
                    if (this.allNumbersEqual(playerToCheck, this.board[row, col], this.board[row + 1, col - 1], this.board[row + 2, col - 2], this.board[row + 3, col - 3]))
                        return playerToCheck;
                }
            }
            return -1;
        }

        private bool allNumbersEqual(int toCheck, params int[] numbers)
        {
            foreach (int num in numbers)
            {
                if (num != toCheck)
                {
                    return false;
                }
            }
            return true;
        }


        private int ColumnNumber(Point mouse)
        {
            for (int i = 0; i < this.boardColumns.Length; i++)
            {
                if ((mouse.X >= this.boardColumns[i].X) && (mouse.Y >= this.boardColumns[i].Y))
                {
                    if ((mouse.X <= this.boardColumns[i].X + this.boardColumns[i].Width) && (mouse.Y <= this.boardColumns[i].Y + this.boardColumns[i].Height))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        private int EmptyRow(int col)
        {
            for (int i = 5; i >= 0; i--)
            {
                if (this.board[i, col] == 0)
                {
                    return i;
                }
            }
            return -1;
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This program was created by Zachary Carless. It is a multiplayer connect 4 game.");
        }

        private void BoardReset()
        {
            Refresh();
            this.boardColumns = new Rectangle[7];
            this.board = new int[6, 7];
            this.Paint += new PaintEventHandler(Form1_Paint);
            this.turn = 1;
            lblPlayer.Text = "Red Players Turn";
            lblPlayer.ForeColor = System.Drawing.Color.Red;
            lblPlayer.BackColor = System.Drawing.Color.Black;

        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            BoardReset();
        }

        private void BtnRedPlayer_Click(object sender, EventArgs e)
        {
            lblRedPlayer.Text = tbPlayerName.Text;
            btnRedPlayer.Visible = false;
            btnYellowPlayer.Visible = true;
            tbPlayerName.Text = "Enter yellow players name";
        }

        private void BtnYellowPlayer_Click(object sender, EventArgs e)
        {
            lblYellowPlayer.Text = tbPlayerName.Text;
            btnRedPlayer.Visible = false; btnYellowPlayer.Visible = false;
            tbPlayerName.Visible = false;
            tbPlayerName.Text = "";
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnRedPlayer.Visible = true;
            tbPlayerName.Visible = true;
            tbPlayerName.Text = "Enter red players name";
        }

        private void BtnRestart_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SaveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            /*using (FileStream stream = new FileStream("C:\\mysecretfile.txt", FileMode.Create))
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    int x =0, y = 0;
                    int[,] M;
                    byte[,] M = new byte[6,7];
                    for (int i = 0; i < 7; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            BinaryWriter.Write(M[i, j], 0, 1);
                        }
                    }
                }
            }*/
        }
        


    }
}

