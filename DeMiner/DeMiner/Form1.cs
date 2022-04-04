using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DeMiner.Classes;

namespace DeMiner
{
    public partial class Form1 : Form
    {
        static Dictionary<int, Color> Colors = new Dictionary<int, Color> {
            { 1, Color.YellowGreen},
            { 2, Color.Green},
            { 3, Color.DarkRed},
            { 4, Color.Red},
            { 5, Color.Red},
            { 6, Color.Red},
            { 7, Color.Red},
            { 8, Color.Red},
            { 9, Color.Red}
        };

        Ground ground;
        bool Loose = false;
        bool Win = false;
        public Form1()
        {
            NewGame();
        }

        public void NewGame()
        {
            this.Controls.Clear();
            InitializeComponent();
            Loose = false;
            Win = false;
            this.Size = new Size(35 + (GameParams.x * 30), 80 + (GameParams.y * 30));
            if (ground != null)
            {
                ground.Cells.Clear();
            }
            Cell.Count = 0;
            ground = new Ground(GameParams.x, GameParams.y, GameParams.probabilityMine);
            label1.Text += " " + ground.CountMines.ToString();
            int Tag = 0;
            for (int i = 0; i < GameParams.y; i++)
            {
                for (int j = 0; j < GameParams.x; j++)
                {
                    Button b = new Button();
                    b.Size = new Size(30, 30);
                    b.Location = new Point(10 + (30 * j), 30 + (30 * i));
                    b.MouseUp += button1_MouseUp;
                    b.Tag = Tag++;
                    b.ForeColor = Color.Gray;
                    b.Font = new Font(b.Font, FontStyle.Bold);

                    //if (ground.Cells[Tag - 1].HasMine)
                    //{
                    //    b.ForeColor = Color.Red;//дебаговый вывод положения бомб
                    //    b.Text = "b";
                    //}
                    //b.Text = ground.Cells[Tag - 1].id.ToString();

                    Controls.Add(b);
                }
            }
        }

        private void CheckWein()
        {
            int countShowed = 0;
            foreach (var c in ground.Cells)
            {
                switch (c.Status)
                {
                    case statusCell.Showed:
                        countShowed++;
                        break;
                    default:
                        break;
                }
            }
            if (countShowed == (GameParams.x * GameParams.y - ground.CountMines))
            {
                Win = true;
                var res = MessageBox.Show("Поздравляем с победой! Вы не ошиблись с выбором ячейки на этом поле ни одного раза!","Победа!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (res == DialogResult.OK)
                {
                    NewGame();
                }
            }
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            SwichBtn(sender as Button, e.Button);
            CheckWein();
        }

        private void ShowAllMines()
        {
            foreach (var itemElem in Controls)
            {
                try
                {
                    Button btn = itemElem as Button;
                    if (btn is Button)
                    {
                        int tag = Convert.ToInt32(btn.Tag);
                        if (ground.Cells[tag].HasMine)
                        {
                            btn.ForeColor = Color.Red;
                            btn.Text = "O";
                        }
                    }
                }
                catch { }
            }


            foreach (var cell in ground.Cells)
            {
                //кликнуть все кнопки, которые контактируют с общими кнопками с нулевым value
                foreach (var itemElem in Controls)
                {
                    try
                    {
                        Button btn = itemElem as Button;
                        if (btn is Button)
                        {
                            int tag = Convert.ToInt32(btn.Tag);
                            if (btn.Enabled && cell.id == tag)
                            {
                                SwichBtn(btn);
                            }
                        }
                    }
                    catch { }
                }                
            }
        }

        private void SwichBtn(Button btn, MouseButtons mouseBtn = MouseButtons.Left)
        {
            if (!Loose && !Win)
            {
                int currTag = Convert.ToInt32(btn.Tag);
                var currStat = ground.Cells[currTag].Status;
                if (mouseBtn == MouseButtons.Left)
                {
                    if (currStat == statusCell.Hided || currStat == statusCell.Warning)
                    {
                        if (ground.Cells[currTag].HasMine)
                        {
                            btn.BackColor = Color.Red;
                            ground.Cells[currTag].Status = statusCell.BOOOOOOM;
                            Loose = true;
                            ShowAllMines();
                        }
                        else
                        {
                            if (currStat == statusCell.Warning)
                            {
                                btn.BackColor = SystemColors.Control;
                            }
                            btn.ForeColor = Colors.FirstOrDefault(x => x.Key == ground.Cells[currTag].value).Value;
                            ground.Cells[currTag].Status = statusCell.Showed;
                            btn.BackColor = Color.DarkGray;
                            if (ground.Cells[currTag].value != 0)
                            {
                                btn.Text = ground.Cells[currTag].value.ToString();
                            }
                            else
                            {
                                ClickNeighboringNullValueBtn(currTag);
                            }
                        }
                    }
                }

                if (mouseBtn == MouseButtons.Right && currStat != statusCell.Showed)
                {
                    if (currStat == statusCell.Warning)
                    {
                        btn.BackColor = SystemColors.Control;
                        ground.Cells[currTag].Status = statusCell.Hided;
                    }
                    else
                    {
                        btn.BackColor = Color.Gold;
                        ground.Cells[currTag].Status = statusCell.Warning;
                    }
                }
            }
        }

        private void ClickNeighboringNullValueBtn(int currTag)
        {
            int x = ground.Cells[currTag].x;
            int y = ground.Cells[currTag].y;
            foreach (var cell in ground.Cells)
            {
                if (!cell.HasMine && (cell.Status == statusCell.Hided || cell.Status == statusCell.Warning)
                    && cell.x >= x - 1 && cell.x <= x + 1 && cell.y >= y - 1 && cell.y <= y + 1)
                {
                    //кликнуть все кнопки, которые контактируют с общими кнопками с нулевым value
                    foreach (var itemElem in Controls)
                    {
                        try
                        {
                            Button btn = itemElem as Button;
                            if (btn is Button)
                            {
                                int tag = Convert.ToInt32(btn.Tag);
                                if (btn.Enabled && cell.id == tag)
                                {
                                    SwichBtn(btn);
                                }
                            }
                        }
                        catch { }
                    }
                }
            }
        }

        private void форматПоляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form paramForm = new Params();
            paramForm.ShowDialog();
        }

        private void новаяИграToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void тёмнаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.DimGray;
        }

        private void светлаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = SystemColors.Control;
        }
    }
}
