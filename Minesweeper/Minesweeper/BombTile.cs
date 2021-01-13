using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Minesweeper
{
    class BombTile : Tile
    {
        public BombTile()
        {
            this.Name = "Uncovered"; //Same name as uncoveredtile so that it will behave like it when clicked.
            this.Tag = "Bomb"; //Tag will determine if it actually is bomb and will be checked when clicked.
            this.BackgroundImage = Properties.Resources.tileUncovered;
            this.Enabled = true;
        }
    }
}
