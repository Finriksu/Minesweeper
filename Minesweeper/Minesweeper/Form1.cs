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
            int left = 12;
            int top = 27;
            
            for (int i = 1; i <= 81; i++){
                UncoveredTile tileAdd = new UncoveredTile();
                tileAdd.Location = new Point(left, top);
                
                tileAdd.MouseDown += flagTile;
                this.Controls.Add(tileAdd);
                left += 20;
                if (i != 0 && i % 9 == 0){
                    top += 20;
                    left = 12;
                }
            }

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
        private void flagTile(object sender, MouseEventArgs e)
        {
            Button clickedTile = sender as Button;

            if (e.Button == MouseButtons.Right)
            {
                if (clickedTile.Name == "Uncovered")
                {
                    clickedTile.BackgroundImage = Properties.Resources.tileFlag;
                    clickedTile.Name = "Flag";
                }
                else if (clickedTile.Name == "Flag")
                {
                    clickedTile.BackgroundImage = Properties.Resources.tileUncovered;
                    clickedTile.Name = "Uncovered";
                }
            }
        }

    }
}
