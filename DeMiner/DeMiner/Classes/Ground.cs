using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeMiner.Classes
{
    class Ground
    {
        public List<Cell> Cells;
        public int CountMines = 0;
        public Ground(int x = 30, int y = 20, int probabilityMine = 40)
        {
            Cells = new List<Cell>();
            Random r = new Random();
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    bool HasMine = false;
                    int ver = r.Next(0, 100);
                    if (ver < probabilityMine)
                        HasMine = true;
                    Cells.Add(new Cell(j, i, HasMine));
                }
            }

            foreach (var cell_1 in Cells)
            {
                if (cell_1.HasMine)
                {
                    CountMines++;
                }
                foreach (var cell_2 in Cells)
                {
                    if (cell_2.x >= cell_1.x - 1 && cell_2.x <= cell_1.x + 1 && cell_2.y >= cell_1.y - 1 && cell_2.y <= cell_1.y + 1)
                    {
                        if (cell_2.HasMine)
                        {
                            cell_1.value++;
                        }
                    }
                }
            }
        }
    }
}
