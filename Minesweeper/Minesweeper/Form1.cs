using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class MainForm : Form
    {
        /*
        private Button btn1 = new Button();
        private Button btn2 = new Button();
        private Button btn3 = new Button();
        private Button btn4 = new Button();
        private Button btn5 = new Button();
        private Button btn6 = new Button();
        private Button btn7 = new Button();
        private Button btn8 = new Button();
        */
        private int gameCount = 0;
        Panel gamePanel;

        public MainForm()
        {
            InitializeComponent();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        

        private void MainForm_Load(object sender, EventArgs e)
        {

            newGame();

            /*
            btn1.BackgroundImage = Properties.Resources.tileEmpty;
            btn1.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#443830");
            btn1.FlatStyle = FlatStyle.Flat;
            btn1.TabStop = false;
            btn1.Enabled = false;
            btn1.Size = new System.Drawing.Size(20, 20);
            btn1.Location = new System.Drawing.Point(12, 27);

            this.Controls.Add(btn1);

            btn2.BackgroundImage = Properties.Resources.tile1;
            btn2.TabStop = false;
            btn2.Enabled = false;
            btn2.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#443830");
            btn2.FlatStyle = FlatStyle.Flat;
            btn2.Size = new System.Drawing.Size(20, 20);
            btn2.Location = new System.Drawing.Point(32, 27);

            this.Controls.Add(btn2);

            btn3.BackgroundImage = Properties.Resources.tile2;
            btn3.TabStop = false;
            btn3.Enabled = false;
            btn3.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#443830");
            btn3.FlatStyle = FlatStyle.Flat;
            btn3.Size = new System.Drawing.Size(20, 20);
            btn3.Location = new System.Drawing.Point(52, 27);

            this.Controls.Add(btn3);

            btn4.BackgroundImage = Properties.Resources.tile3;
            btn4.TabStop = false;
            btn4.Enabled = false;
            btn4.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#443830");
            btn4.FlatStyle = FlatStyle.Flat;
            btn4.Size = new System.Drawing.Size(20, 20);
            btn4.Location = new System.Drawing.Point(72, 27);

            this.Controls.Add(btn4);

            btn5.BackgroundImage = Properties.Resources.tile4;
            btn5.TabStop = false;
            btn5.Enabled = false;
            btn5.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#443830");
            btn5.FlatStyle = FlatStyle.Flat;
            btn5.Size = new System.Drawing.Size(20, 20);
            btn5.Location = new System.Drawing.Point(92, 27);

            this.Controls.Add(btn5);

            btn6.BackgroundImage = Properties.Resources.tile5;
            btn6.TabStop = false;
            btn6.Enabled = false;
            btn6.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#443830");
            btn6.FlatStyle = FlatStyle.Flat;
            btn6.Size = new System.Drawing.Size(20, 20);
            btn6.Location = new System.Drawing.Point(112, 27);

            this.Controls.Add(btn6);

            btn7.Tag = "Uncovered";
            btn7.BackgroundImage = Properties.Resources.tileUncovered;
            btn7.TabStop = false;
            btn7.Enabled = true;
            btn7.FlatStyle = FlatStyle.Flat;
            btn7.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#443830");
            btn7.Size = new System.Drawing.Size(20, 20);
            btn7.Location = new System.Drawing.Point(132, 27);
            btn7.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn7_MouseDown);

            this.Controls.Add(btn7);

            btn8.BackgroundImage = Properties.Resources.tileFlag;
            btn8.TabStop = false;
            btn8.Enabled = true;
            btn8.FlatStyle = FlatStyle.Flat;
            btn8.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#443830");
            btn8.Size = new System.Drawing.Size(20, 20);
            btn8.Location = new System.Drawing.Point(152, 27);

            this.Controls.Add(btn8);
            */
        }
        /*
        private void btn7_MouseDown(object sender, MouseEventArgs e)
        {
            
            if(e.Button == MouseButtons.Right)
            {
                if(btn7.Tag.ToString() == "Uncovered")
                {
                    btn7.BackgroundImage = Properties.Resources.tileFlag;
                    btn7.Tag = "Flag";
                }
                else if (btn7.Tag.ToString() == "Flag")
                {
                    btn7.BackgroundImage = Properties.Resources.tileUncovered;
                    btn7.Tag = "Uncovered";
                }
                
            }
        }
        */
        private void replaceTile(object sender, MouseEventArgs e)
        {
            Tile clickedTile = sender as Tile;
            Point repLocation = clickedTile.Location;
            string repTag = clickedTile.Tag.ToString();

            if (e.Button == MouseButtons.Right)
            {
                if (clickedTile.Name.ToString() == "Uncovered")
                { 
                    gamePanel.Controls.Remove(clickedTile);

                    FlagTile repTile = new FlagTile();
                    repTile.Location = repLocation;
                    repTile.Tag = repTag;
                    repTile.MouseDown += replaceTile;

                    gamePanel.Controls.Add(repTile);
                }
                else if (clickedTile.Name.ToString() == "Flag")
                {
                    gamePanel.Controls.Remove(clickedTile);

                    UncoveredTile repTile = new UncoveredTile();
                    repTile.Location = repLocation;
                    repTile.Tag = repTag;
                    repTile.MouseDown += replaceTile;

                    gamePanel.Controls.Add(repTile);
                }
            }
            else if(e.Button == MouseButtons.Left)
            {
                if(clickedTile.Tag.ToString() != "Bomb" && clickedTile.Name != "Flag")
                {
                    clickedTile.BackgroundImage = Properties.Resources.tileEmpty;
                    clickedTile.Name = "Empty";

                    gamePanel.Controls.Remove(clickedTile);

                    EmptyTile repTile = new EmptyTile();
                    repTile.Location = repLocation;
                    repTile.MouseDown += replaceTile;
                    gamePanel.Controls.Add(repTile);
                }
                else if (clickedTile.Tag.ToString() == "Bomb" && clickedTile.Name != "Flag")
                {
                    MessageBox.Show("lost");
                }
            }
        }

        private void createTiles(int difficulty)
        {
            

            int left = 10;
            int top = 50;
            int bombCount = 0;
            int tileCount = 0;
            int rowLenght = 0;

            switch (difficulty)
            {
                case 0:
                    bombCount = 10;
                    tileCount = 81;
                    rowLenght = 9;
                    break;
                case 1:
                    bombCount = 40;
                    tileCount = 256;
                    rowLenght = 16;
                    break;
                case 2:
                    bombCount = 99;
                    tileCount = 480;
                    rowLenght = 30;
                    break;
            }
            
            for (int i = 1; i <= tileCount; i++)
            {
                if (i <= bombCount)
                {
                    BombTile tileAdd = new BombTile();
                    tileAdd.Location = new Point(left, top);

                    tileAdd.MouseDown += replaceTile;
                    gamePanel.Controls.Add(tileAdd);
                    left += 20;
                }
                else
                {
                    UncoveredTile tileAdd = new UncoveredTile();
                    tileAdd.Location = new Point(left, top);

                    tileAdd.MouseDown += replaceTile;
                    gamePanel.Controls.Add(tileAdd);
                    left += 20;
                    tileAdd.Tag = "";
                }

                if (i != 0 && i % rowLenght == 0)
                {
                    top += 20;
                    left = 10;
                }
            }
        }

        private int chooseDifficulty()
        {
            MessageBox.Show("Choose difficulty:");

            int difficulty = 2;

            return difficulty;
        }

        private void newGame()
        {
            int difficulty = chooseDifficulty();

            if (gameCount == 0)
                createPanel(difficulty);
            else if (gameCount != 0)
                gamePanel.Controls.Clear();
            
            createTiles(difficulty);
            
        }

        private void createPanel(int difficulty)
        {
            int width = 0;
            int height = 0;

            switch (difficulty)
            {
                case 0:
                    width = 10 * 2 + 9 * 20;
                    height = 50 + 10 + 9 * 20;

                    this.MaximumSize = new Size(216, 303);
                    this.MinimumSize = new Size(216, 303);
                    break;
                case 1:
                    width = 10 * 2 + 16 * 20;
                    height = 50 + 10 + 16 * 20;

                    this.MaximumSize = new Size(356, 443);
                    this.MinimumSize = new Size(356, 443);
                    break;
                case 2:
                    width = 10 * 2 + 30 * 20;
                    height = 50 + 10 + 16 * 20;

                    this.MaximumSize = new Size(636, 443);
                    this.MinimumSize = new Size(636, 443);
                    break;
            }

            gamePanel = new Panel();
            gamePanel.Location = new Point(0, 24);
            gamePanel.Size = new Size(width, height);
            gamePanel.BackColor = Color.Red;
            
            this.Controls.Add(gamePanel);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            gameCount++;
            newGame();
        }
    }
}
