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
        Label flagLabel = new Label();
        int second = 0;
        private int difficulty = 0;
        RadioButton easyRB = new RadioButton();
        RadioButton normalRB = new RadioButton();
        RadioButton hardRB = new RadioButton();
        Form difficultyForm = new Form();
        bool gameOver = false;
        Timer gameTimer = new Timer();
        int flagCount;
        int gameCount = 0;
        List<Tile> tileList;

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
            Tile repTile = new Tile();
            Point repLocation = clickedTile.Location;
            string repTag = clickedTile.Tag.ToString();
            int repRowId = clickedTile.RowId;
            int repColumnId = clickedTile.ColumnId;

            if (e.Button == MouseButtons.Right)
            {
                if (clickedTile.Name.ToString() == "Uncovered")
                { 
                    gamePanel.Controls.Remove(clickedTile);

                    repTile = new FlagTile();
                    repTile.Location = repLocation;
                    repTile.Tag = repTag;
                    repTile.RowId = repRowId;
                    repTile.ColumnId = repColumnId;
                    repTile.MouseDown += replaceTile;

                    gamePanel.Controls.Add(repTile);

                    flagCount--;
                }
                else if (clickedTile.Name.ToString() == "Flag" && clickedTile.Tag.ToString() != "Bomb")
                {
                    gamePanel.Controls.Remove(clickedTile);

                    repTile = new UncoveredTile();
                    repTile.Location = repLocation;
                    repTile.Tag = repTag;
                    repTile.RowId = repRowId;
                    repTile.ColumnId = repColumnId;
                    repTile.MouseDown += replaceTile;

                    gamePanel.Controls.Add(repTile);

                    flagCount++;
                }
                else if (clickedTile.Name.ToString() == "Flag" && clickedTile.Tag.ToString() == "Bomb")
                {
                    gamePanel.Controls.Remove(clickedTile);

                    repTile = new BombTile();
                    repTile.Location = repLocation;
                    repTile.Tag = repTag;
                    repTile.RowId = repRowId;
                    repTile.ColumnId = repColumnId;
                    repTile.MouseDown += replaceTile;

                    gamePanel.Controls.Add(repTile);

                    flagCount++;
                }

                flagLabel.Text = flagCount.ToString();
            }
            else if(e.Button == MouseButtons.Left)
            {
                if(clickedTile.Tag.ToString() != "Bomb" && clickedTile.Name != "Flag")
                {
                    clickedTile.BackgroundImage = Properties.Resources.tileEmpty;
                    clickedTile.Name = "Empty";

                    gamePanel.Controls.Remove(clickedTile);

                    repTile = new EmptyTile();
                    repTile.Location = repLocation;
                    repTile.RowId = repRowId;
                    repTile.ColumnId = repColumnId;
                    repTile.MouseDown += replaceTile;
                    gamePanel.Controls.Add(repTile);
                }
                else if (clickedTile.Tag.ToString() == "Bomb" && clickedTile.Name != "Flag")
                {
                    gamePanel.Controls.Remove(clickedTile);

                    repTile = new BombTile();
                    repTile.Location = repLocation;
                    repTile.Tag = repTag;
                    repTile.RowId = repRowId;
                    repTile.ColumnId = repColumnId;
                    repTile.BackgroundImage = Properties.Resources.tileBombHit;
                    repTile.MouseDown += replaceTile;

                    gamePanel.Controls.Add(repTile);

                    gameOver = true;
                    
                    foreach(Control x in gamePanel.Controls)
                    {
                        if(x is Tile)
                        {
                            ((Tile)x).Disable();
                        }
                    }
                    
                    MessageBox.Show("lost");

                }
                checkAdjacent(repTile);
                checkWin();
            }
        }
        private void checkWin()
        {
            int tilesToWin = 0;
            int tilesClicked = -2;
            int tilesLeft = 0;
            
            foreach(Control x in gamePanel.Controls)
            {
                if(!(x is BombTile || x is FlagTile || x is UncoveredTile))
                {
                    tilesClicked++;
                }
            }

            switch (difficulty)
            {
                case 0:
                    tilesToWin = 10;
                    tilesLeft = 81;
                    break;
                case 1:
                    tilesToWin = 40;
                    tilesLeft = 256;
                    break;
                case 2:
                    tilesToWin = 99;
                    tilesLeft = 480;
                    break;
            }

            if (tilesLeft - tilesClicked == tilesToWin && tilesToWin != 0)
            {
                foreach (Control x in gamePanel.Controls)
                {
                    if (x is Tile)
                    {
                        ((Tile)x).Disable();
                    }
                }
                gameOver = true;
                MessageBox.Show("Victory");
            }
                
        }

        private void createTiles()
        {
            int bombCount = 0;
            int tileCount = 0;
            int rowLenght = 0;

            List<Tile> unMixedBombList = new List<Tile>();

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

            flagCount = bombCount;
            flagLabel.Text = bombCount.ToString();

            for (int i = 1; i <= tileCount; i++)
            {
                if (i <= bombCount)
                {
                    BombTile tileAdd = new BombTile();
                    unMixedBombList.Add(tileAdd);
                    tileAdd.MouseDown += replaceTile;
                }
                else
                {
                    UncoveredTile tileAdd = new UncoveredTile();
                    unMixedBombList.Add(tileAdd);
                    tileAdd.MouseDown += replaceTile;
                }

                
            }
            List<Tile> mixedList = mixTiles(unMixedBombList, rowLenght);

            for(int i = 0; i < mixedList.Count; i++)
            {
                gamePanel.Controls.Add(mixedList[i]);
            }

        }

        private List<Tile> mixTiles(List<Tile> unMixedList, int rowLenght)
        {
            List<Tile> mixedList = new List<Tile>();
            List<int> nums = new List<int>();

            int left = 10;
            int top = 50;

            int row = 0;
            int column = 0;

            Random rnd = new Random();

            for(int i = 1; i <= unMixedList.Count; i++)
            {
                int j = rnd.Next(0, unMixedList.Count);

                if (nums.Contains(j)){
                    i--;
                }
                else
                {
                    nums.Add(j);
                    Tile replace = unMixedList.ElementAt(j);
                    replace.Location = new Point(left, top);
                    replace.RowId = row;
                    replace.ColumnId = column;
                    column++;
                    left += 20;
                    mixedList.Add(replace);

                    if (i != 0 && i % rowLenght == 0)
                    {
                        top += 20;
                        left = 10;
                    }
                }

                if(column == rowLenght)
                {
                    column = 0;
                    row++;
                }

            }

            return mixedList;
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
            gameOver = false;
            
        }

        private void newGame()
        {
            gameCount++;
            chooseDifficulty();
            difficultyForm.Controls.Clear();
            gamePanel.Controls.Clear();
            getTime();
            displayFlagCount();
            createPanel();
            createTiles();
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
            gamePanel.BackColor = Color.LightGray;
            
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

            gameOver = false;
            second = 0;
            difficultyForm.Close();
        }

        private void getTime()
        {
            gameTimer.Interval = 1000;
            gameTimer.Tick += new EventHandler(this.t_Tick);
            gameTimer.Enabled = true;

            timeLabel.AutoSize = true;
            timeLabel.Font = new Font("Microsoft Sans Serif", 16);
            timeLabel.ForeColor = Color.Red;
            timeLabel.BackColor = Color.Black;
            timeLabel.Text = "000";

            if (difficulty == 0)
                timeLabel.Location = new Point(142, 12);
            else if (difficulty == 1)
                timeLabel.Location = new Point(282, 12);
            else
                timeLabel.Location = new Point(562, 12);

            gamePanel.Controls.Add(timeLabel);
            gameTimer.Start();
        }

        private void t_Tick(object sender, EventArgs e)
        {
            if (gameOver == false)
            {
                string time = "0";
                second++;

                if (second/gameCount < 10)
                    time += "0" + second/gameCount;
                else
                    time += second/gameCount;
                if (second < 1000)
                    timeLabel.Text = time;
                else
                    timeLabel.Text = "999";
            }
            else
            {
                gameTimer.Stop();
                gameTimer.Enabled = false;
            }
                
            
                
        }

        private void displayFlagCount()
        {
            flagLabel.AutoSize = true;
            flagLabel.Font = new Font("Microsoft Sans Serif", 16);
            flagLabel.ForeColor = Color.Red;
            flagLabel.BackColor = Color.Black;

            if (difficulty == 0)
                flagLabel.Location = new Point(10, 12);
            else if (difficulty == 1)
                flagLabel.Location = new Point(10, 12);
            else
                flagLabel.Location = new Point(10, 12);

            flagLabel.Text = flagCount.ToString();

            gamePanel.Controls.Add(flagLabel);
        }

        private void checkAdjacent(Tile clickedTile)
        {           
            int clickedRow = clickedTile.RowId;
            int clickedColumn = clickedTile.ColumnId;

            int[] rowIds = new int[3];
            int[] columnIds = new int[3];

            rowIds[0] = clickedRow - 1;
            rowIds[1] = clickedRow;
            rowIds[2] = clickedRow + 1;

            columnIds[0] = clickedColumn - 1;
            columnIds[1] = clickedColumn;
            columnIds[2] = clickedColumn + 1;

            int j = 0;

            

            foreach (Control x in gamePanel.Controls)
            {
                if(x is Tile && !(x is BombTile))
                {
                    bool isAdjacent = false;

                    for (int i = 0; i < 3; i++)
                    {
                        if(((Tile)x).RowId == rowIds[i] && ((Tile)x).ColumnId == columnIds[j])
                        {
                            isAdjacent = true;
                        }

                        if (isAdjacent)
                        {
                                EmptyTile repTile = new EmptyTile();
                                Point repLocation = x.Location;
                                repTile.Location = repLocation;

                                gamePanel.Controls.Remove(x);

                                gamePanel.Controls.Add(repTile);
                        }

                        if (i == 2 && j != 2)
                        {
                            i = 0;
                            j++;
                        }
                    }

                    
                }
            }


        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            newGame();
        }
    }
}