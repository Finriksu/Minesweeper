using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Minesweeper
{
    class EmptyTile : Tile
    {
        public EmptyTile()
        {
            this.Name = "Empty";
            this.BackgroundImage = Properties.Resources.tileEmpty;
            this.Enabled = false;
        }
    }
}
