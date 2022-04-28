using System;
using System.Collections.Generic;
using System.Text;

namespace _3d_in_Console
{
    static class Scene
    {
        public static List<IFigure> objects = new List<IFigure>();

        //public Scene()
        //{
        //    objects = new List<Figure>();
        //}

        public static void Add(IFigure f)
        {
            objects.Add(f);
        }

        public static void Clear()
        {
            objects.Clear();
        }
    }
}
