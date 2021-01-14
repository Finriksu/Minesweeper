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
        private Panel gamePanel = new Panel();
        Label timeLabel = new Label();
        int hour;
        int minute = 58;
        int second = 50;
        private int difficulty = 0;
        RadioButton easyRB = new RadioButton();
        RadioButton normalRB = new RadioButton();
        RadioButton hardRB = new RadioButton();
        Form difficultyForm = new Form();

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
        }
        
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

        private void createTiles()
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

                    this.MaximumSize = new Size(216, 303);
                    this.MinimumSize = new Size(216, 303);
                    break;
                case 1:
                    bombCount = 40;
                    tileCount = 256;
                    rowLenght = 16;

                    this.MaximumSize = new Size(356, 443);
                    this.MinimumSize = new Size(356, 443);
                    break;
                case 2:
                    bombCount = 99;
                    tileCount = 480;
                    rowLenght = 30;

                    this.MaximumSize = new Size(636, 443);
                    this.MinimumSize = new Size(636, 443);
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

        private void chooseDifficulty()
        {
            GroupBox difficultyGroup = new GroupBox();
            Button getValueBtn = new Button();
            Label difficultyText = new Label();

            easyRB.Location = new Point(10, 40);
            easyRB.Text = "Easy";
            easyRB.Checked = true;
            easyRB.Name = "RadioButtonEasy";

            normalRB.Location = new Point(10, 70);
            normalRB.Text = "Normal";
            normalRB.Checked = false;
            normalRB.Name = "RadioButtonNormal";

            hardRB.Location = new Point(10, 100);
            hardRB.Text = "Hard";
            hardRB.Checked = false;
            hardRB.Name = "RadioButtonHard";

            difficultyText.Text = "Choose difficulty:";
            difficultyText.Location = new Point(10, 15);

            difficultyGroup.Dock = DockStyle.Fill;

            getValueBtn.Location = new Point(25, 130);
            getValueBtn.Text = "Start";
            getValueBtn.Click += getDifficulty;

            difficultyForm.Text = "Minesweeper";
            difficultyForm.Size = new Size(100, 200);

            difficultyGroup.Controls.Add(easyRB);
            difficultyGroup.Controls.Add(normalRB);
            difficultyGroup.Controls.Add(hardRB);
            difficultyGroup.Controls.Add(getValueBtn);
            difficultyGroup.Controls.Add(difficultyText);
            difficultyForm.Controls.Add(difficultyGroup);
            
            difficultyForm.ShowDialog();
        }

        private void newGame()
        {
            chooseDifficulty();
            difficultyForm.Controls.Clear();
            gamePanel.Controls.Clear();
            createPanel();
            createTiles();
            getTime();
        }

        private void createPanel()
        {
            int width = 0;
            int height = 0;

            switch (difficulty)
            {
                case 0:
                    width = 10 * 2 + 9 * 20;
                    height = 50 + 10 + 9 * 20;
                    break;
                case 1:
                    width = 10 * 2 + 16 * 20;
                    height = 50 + 10 + 16 * 20;
                    break;
                case 2:
                    width = 10 * 2 + 30 * 20;
                    height = 50 + 10 + 16 * 20;
                    break;
            }

            gamePanel.Location = new Point(0, 24);
            gamePanel.Size = new Size(width, height);
            gamePanel.BackColor = Color.Red;
            
            this.Controls.Add(gamePanel);
        }

        private void getDifficulty(object sender, EventArgs e)
        {
            if (easyRB.Checked)
                difficulty = 0;
            else if (normalRB.Checked)
                difficulty = 1;
            else
                difficulty = 2;

            difficultyForm.Close();
        }

        private void getTime()
        {
            Timer gameTimer = new Timer();
            gameTimer.Interval = 1000;
            gameTimer.Tick += new EventHandler(this.t_Tick);

            timeLabel.AutoSize = true;
            timeLabel.Font = new Font("Microsoft Sans Serif", 16);
            timeLabel.ForeColor = Color.Red;
            timeLabel.BackColor = Color.Black;

            if (difficulty == 0)
                timeLabel.Location = new Point(52, 12);
            else if (difficulty == 1)
                timeLabel.Location = new Point(122, 12);
            else
                timeLabel.Location = new Point(262, 12);

            gamePanel.Controls.Add(timeLabel);
            gameTimer.Start();
        }

        private void t_Tick(object sender, EventArgs e)
        {
            string time = "";

            if (second == 59)
            {
                second = 0;
                minute += 1;
            }
            else
                second++;

            if (minute == 59 && second == 59)
            {
                minute = 0;
                hour++;
            }

            if (hour < 10)
                time += "0" + hour;
            else
                time += hour;

            time += ":";

            if (minute < 10)
                time += "0" + minute;
            else
                time += minute;

            time += ":";

            if (second < 10)
                time += "0" + second;
            else
                time += second;

            timeLabel.Text = time;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            newGame();
        }
    }
}
