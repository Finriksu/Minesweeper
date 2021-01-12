using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Minesweeper
{
    class UncoveredTile : Tile
    {
        public UncoveredTile()
        {
            this.Name = "Uncovered";
            this.BackgroundImage = Properties.Resources.tileUncovered;
            this.Enabled = true;
        }
    }
}
