using System;
using System.Collections.Generic;
using System.Text;

namespace Pacman
{
    static class Map
    {
        public static string[] Grounds;
        public static void InitGrounds()
        {
            Grounds = new string[4];
            int c = 0;
            Grounds[c] =
                        "OOOOOOOOOOOOOOOOOOOOOOOOOOOOO" +
                        "O     O+  O       O   OO    O" +
                        "O OOO   O   OOOOO   O  OOOO O" +
                        "O O O OOOOOOOOOOOOOOOO   +  O" +
                        "O O O O             OO OOOO O" +
                        "O     O OOOOOOOOOOO  O      O" +
                        "O OOO  + O   ☺    OO   OOO  O" +
                        "O O   O OOOOO OOOOO  O O O OO" +
                        "O O O O             OO O O  O" +
                        "O O O OOOOOOOOOOOOOOOO O OO O" +
                        "O O O                    O  O" +
                        "O O OOOOOOO OOOOOOOOOOOOOO OO" +
                        "O      O            O       O" +
                        "O O OO O OOOOOOOOOO O OOOOOOO" +
                        "O O         O         +     O" +
                        "OOOOOOOOOOOOOOOOOOOOOOOOOOOOO";
            c++;
            Grounds[c] =
                        "OOOOOOOOOOOOOOOOOOOOOOOOOOOOO" +
                        "O+++++++++++++++++++++++++++O" +
                        "O+++++++++++++++++++++++++++O" +
                        "O+++++++++++++++++++++++++++O" +
                        "O+++++++              ++++++O" +
                        "O+++++++              ++++++O" +
                        "O+++++++              ++++++O" +
                        "O+++++++      ☺       ++++++O" +
                        "O+++++++              ++++++O" +
                        "O+++++++              ++++++O" +
                        "O+++++++              ++++++O" +
                        "O+++++++++++++++++++++++++++O" +
                        "O+++++++++++++++++++++++++++O" +
                        "O++++++++++++++OOOOO++++++++O" +
                        "O+++++++++++++++++++++++++++O" +
                        "OOOOOOOOOOOOOOOOOOOOOOOOOOOOO";
            c++;
            Grounds[c] =
                        "OOOOOOOOOOOOOOOOOOOOOOOOOOOOO" +
                        "OOOOOOOOOO OOOOOOOOOOOOOOOOOO" +
                        "OOOOOOOOOO OOOOOOOOOOOOOOOOOO" +
                        "OOOOOOOOOO OOOOOOOOOOOOOOOOOO" +
                        "OOOOOOOOOO OOOOOOOOOOOOOOOOOO" +
                        "OOOOOOOOOO OOOOOOOOOOOOOOOOOO" +
                        "OOOOOOOOOO OOOOOOOOOOOOOOOOOO" +
                        "O             ☺             O" +
                        "OOOOOOOOOO OOOOOOO OOOOOOOOOO" +
                        "OOOOOOOOOO O++++++++++++++++O" +
                        "OOOOOOOOOO O++++++++++++++++O" +
                        "OOOOOOOOOO O++++++++++++++++O" +
                        "OOOOOOOOOO O++++++++++++++++O" +
                        "OOOOOOOOOO O++++++++++++++++O" +
                        "OOOOOOOOOO O++++++++++++++++O" +
                        "OOOOOOOOOOOOOOOOOOOOOOOOOOOOO";
            c++;
            Grounds[c] =
                        "OOOOOOOOOOOOOOOOOOOOOOOOOOOOO" +
                        "OOOOOOOOOO OOOOOOOOOOOOOOOOOO" +
                        "OOOOOOOOOO OOOOOOOOOOOOOOOOOO" +
                        "OOOOOOOOOO OOOOOOOOOOOOOOOOOO" +
                        "OOOOOOOOOO OOOOOOOOOOOOOOOOOO" +
                        "OOOOOOOOOO OOOOOOOOOOOOOOOOOO" +
                        "OOOOOOOOOO OOOOOOOOOOOOOOOOOO" +
                        "O             ☺             O" +
                        "OOOOOOOOOO OOOOOOO OOOOOOOOOO" +
                        "OOOOOOOOOO O++++++++++++++++O" +
                        "OOOOOOOOOO O++++++++++++++++O" +
                        "OOOOOOOOOO O++++++++++++++++O" +
                        "OOOOOOOOOO O++++++++++++++++O" +
                        "OOOOOOOOOO O++++++++++++++++O" +
                        "OOOOOOOOOO O++++++++++++++++O" +
                        "OOOOOOOOOOOOOOOOOOOOOOOOOOOOO";
        }
    }
}
