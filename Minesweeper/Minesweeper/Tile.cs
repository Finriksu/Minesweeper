using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace Minesweeper
{
    class Tile : Button
    {
        public Tile()
        {
            this.Size = new Size(20, 20);
            this.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#443830");
            this.FlatStyle = FlatStyle.Flat;
            this.TabStop = false;
            this.Tag = "";
        }

        private int rowId;
        private int columnId;

        public int RowId
        {
            get { return rowId; } 
            set { rowId = value; } 
        }

        public int ColumnId
        {
            get { return columnId; }
            set { columnId = value; }
        }

        public void Disable()
        {
            this.Enabled = false;
        }
    }
}
