﻿using System;
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
        Panel gamePanel = new Panel();
        Label timeLabel = new Label();
        Label flagLabel = new Label(); 
        RadioButton easyRB = new RadioButton();
        RadioButton normalRB = new RadioButton();
        RadioButton hardRB = new RadioButton();
        Form difficultyForm = new Form();
        Timer gameTimer = new Timer();
        
        bool gameOver = false;
        int second = 0;
        private int difficulty = 0;
        int flagCount;
        int gameCount = 0;
        int tilesToWin = 0;
        int tilesClicked = 0;

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

        /// <summary>
        /// Starts the game and clears components, so that new components can be added
        /// for a new game
        /// </summary>
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

        /// <summary>
        /// Opens a form where user chooses the difficulty
        /// </summary>
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

        /// <summary>
        /// Gets the difficulty depending on which radiobutton user clicked on the difficultyform created by chooseDifficulty()
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        
        /// <summary>
        /// Adds functionality to a timer and adds the time onto a label on the gamePanel
        /// </summary>
        private void getTime()
        {
            gameTimer.Interval = 1000;
            gameTimer.Tick += new EventHandler(this.t_Tick);
            gameTimer.Enabled = false;

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

        /// <summary>
        /// Tick method for getting the time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void t_Tick(object sender, EventArgs e)
        {
            if (gameOver == false)
            {
                string time = "0";
                second++;

                if (second / gameCount < 10)
                    time = "00" + second / gameCount;
                else if (second / gameCount < 100)
                    time = "0" + second / gameCount;
                else
                    time = (second / gameCount).ToString();

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

        

        /// <summary>
        /// Displays how many bombtiles there are left without a flag on them.
        /// </summary>
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

        /// <summary>
        /// Creates a panel where gamecomponents will be added on.
        /// </summary>
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

        /// <summary>
        /// Creates the tiles and adds them to a list
        /// </summary>
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
                    tilesToWin = tileCount - bombCount;

                    this.MaximumSize = new Size(216, 303);
                    this.MinimumSize = new Size(216, 303);
                    break;
                case 1:
                    bombCount = 40;
                    tileCount = 256;
                    rowLenght = 16;
                    tilesToWin = tileCount - bombCount;

                    this.MaximumSize = new Size(356, 443);
                    this.MinimumSize = new Size(356, 443);
                    break;
                case 2:
                    bombCount = 99;
                    tileCount = 480;
                    rowLenght = 30;
                    tilesToWin = tileCount - bombCount;

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

            for (int i = 0; i < mixedList.Count; i++)
            {
                gamePanel.Controls.Add(mixedList[i]);
            }

        }

        /// <summary>
        /// Mixes the tiles to random order from unMixedList and adds them to a list
        /// </summary>
        /// <param name="unMixedList">list of tiles created in createPanel()</param>
        /// <param name="rowLenght">determines how long a row on the gamepanel is and uses it to give the tiles a location</param>
        /// <returns></returns>
        private List<Tile> mixTiles(List<Tile> unMixedList, int rowLenght)
        {
            List<Tile> mixedList = new List<Tile>();
            List<int> nums = new List<int>();

            int left = 10;
            int top = 50;

            int row = 0;
            int column = 0;

            Random rnd = new Random();

            for (int i = 1; i <= unMixedList.Count; i++)
            {
                int j = rnd.Next(0, unMixedList.Count);

                if (nums.Contains(j))
                {
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

                if (column == rowLenght)
                {
                    column = 0;
                    row++;
                }

            }

            return mixedList;
        }

        /// <summary>
        /// Replaces a tile when it is clicked
        /// </summary>
        /// <param name="sender">a tile on the gamepanel which is clicked</param>
        /// <param name="e"></param>
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
                    checkAdjacent(clickedTile);
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

                    string lostText = "You Lost!" + "\r" + "Score = " + second/gameCount;
                    MessageBox.Show("You Lost!", "Game Over");
                }
                checkWin();
            }
        }

        /// <summary>
        /// Checks if adjacent tiles from the clicked tile are empty(not a bomb tile or next to one)
        /// </summary>
        /// <param name="checkTile">tile to be checked</param>
        private void checkAdjacent(Tile checkTile)
        {
            int[] rowIds = new int[3];
            int[] columnIds = new int[3];

            rowIds[0] = checkTile.RowId - 1;
            rowIds[1] = checkTile.RowId;
            rowIds[2] = checkTile.RowId + 1;

            columnIds[0] = checkTile.ColumnId - 1;
            columnIds[1] = checkTile.ColumnId;
            columnIds[2] = checkTile.ColumnId + 1;

            int bombcount = 0;

            //Tämän pitäisi tarkistaa lattaa ympäröivät laatat ja muuttaa ne tyhjiksi laatoiksi jos niissä ei ole miinaa
            //mutta muutaa laatat pelkästään klikatun laatan oikealta puolelta. Joskus debuggerilla katsoessa muuttaa myös vasemman ylälaatan.

            //HUOM BUGI LÖYDETTY, SYY SELVITETTÄVÄ VIELÄ!!!
            //Vaikuttaa vain joka toiseen laattaan. Mikäli jokin (joka toisesta) laatasta on laatta mitä klikattiin tai pommilaatta, vaikuttaa siitä seuraavaan laattaan oikealle päin.
            //EI JOHDU TIMERISTA

            foreach (Control x in gamePanel.Controls)
            {
                
                if (x is Tile)
                {
                    //MessageBox.Show("ID: " + ((Tile)x).ColumnId + "_" + ((Tile)x).RowId + "location: " + x.Location);

                    //Käy kaikki laatat läpi, mikäli tämä pätkä ei ole käytössä

                    for (int i = 0; i < 3; i++)
                    {
                        if (((Tile)x).ColumnId == columnIds[i] && ((Tile)x).RowId == rowIds[0])
                        {
                            //MessageBox.Show("x CID: " + ((Tile)x).ColumnId + " x RID: " + ((Tile)x).RowId + "\nCheck CID: " + columnIds[i] + " Check RID: " + rowIds[0]);
                            if (x is BombTile)
                            {
                                bombcount++;
                            }
                            else if (x is UncoveredTile)
                            {
                                checkAdjacent(x as Tile);
                            }
                        }
                        else if (((Tile)x).ColumnId == columnIds[i] && ((Tile)x).RowId == rowIds[1])
                        {
                            if (x is BombTile)
                            {
                                bombcount++;
                            }
                            else if (x is UncoveredTile)
                            {
                                checkAdjacent(x as Tile);
                            }
                        }
                        else if (((Tile)x).ColumnId == columnIds[i] && ((Tile)x).RowId == rowIds[2])
                        {
                            if (x is BombTile)
                            {
                                bombcount++;
                            }
                            else if (x is UncoveredTile)
                            {
                                checkAdjacent(x as Tile); //TÄMÄ MUUALLE
                            }
                        }
                    }
                }
            }

            if (bombcount == 1)
            {
                Tile1 replaceTile = new Tile1();
                replaceTile.Location = checkTile.Location;

                gamePanel.Controls.Remove(checkTile);
                gamePanel.Controls.Add(replaceTile);

                tilesClicked++;
            }
            else if (bombcount == 2)
            {
                Tile2 replaceTile = new Tile2();
                replaceTile.Location = checkTile.Location;

                gamePanel.Controls.Remove(checkTile);
                gamePanel.Controls.Add(replaceTile);

                tilesClicked++;
            }
            else if (bombcount == 3)
            {
                Tile3 replaceTile = new Tile3();
                replaceTile.Location = checkTile.Location;

                gamePanel.Controls.Remove(checkTile);
                gamePanel.Controls.Add(replaceTile);

                tilesClicked++;
            }
            else if (bombcount == 4)
            {
                Tile4 replaceTile = new Tile4();
                replaceTile.Location = checkTile.Location;

                gamePanel.Controls.Remove(checkTile);
                gamePanel.Controls.Add(replaceTile);

                tilesClicked++;
            }
            else if (bombcount == 5)
            {
                Tile5 replaceTile = new Tile5();
                replaceTile.Location = checkTile.Location;

                gamePanel.Controls.Remove(checkTile);
                gamePanel.Controls.Add(replaceTile);

                tilesClicked++;
            }
            else
            {
                EmptyTile replaceTile = new EmptyTile();
                replaceTile.Location = checkTile.Location;

                gamePanel.Controls.Remove(checkTile);
                gamePanel.Controls.Add(replaceTile);

                tilesClicked++;
            }
        }

        /// <summary>
        /// Checks if the user has won the game. Occurs when there are no undiscoverd tiles left, only bomb tiles and flag tiles
        /// </summary>
        private void checkWin()
        {       
            if (tilesClicked == tilesToWin && tilesToWin != 0)
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

        /// <summary>
        /// Starts a new game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuNewGame_Click(object sender, EventArgs e)
        {
            newGame();
        }
    }
}