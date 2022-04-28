using System;
using System.Collections.Generic;
using System.Text;

namespace _3d_in_Console
{
    class DirectionLigth
    {
        public float Intensity;
        public Direction Dir;

        public DirectionLigth(Direction dir, float intensity)
        {
            this.Dir = dir;
            this.Intensity = intensity;
        }
    }
}
