using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeMiner.Classes
{
    enum statusCell
    {
        Hided,
        Showed,
        Warning,
        BOOOOOOM
    }

    class Cell
    {
        public static int Count = 0;
        public int id;
        public int x;
        public int y;
        public int value = 0;
        public bool HasMine = false;
        public statusCell Status = statusCell.Hided;

        public Cell(int x, int y, bool HasMine)
        {
            this.id = Count++;
            this.x = x;
            this.y = y;
            this.HasMine = HasMine;
        }
    }
}
