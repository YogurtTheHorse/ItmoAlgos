using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ItmoAlgos
{
    public class Program
    {
        private static string[] _input;
        private static int _currentLineIndex;
        private const long E15 = 1000000000000000;

        private static string ReadLine()
        {
            return _input[_currentLineIndex++];
        }

        public static void Main(string[] args)
        {
            _input = File.ReadAllLines("input.txt");

            string map = ReadLine().Replace(" ", "");
            int[] z = new int[map.Length];

            int L = 0, R = 0;
            for (int i = 1; i < map.Length; i++)
            {
                if (i > R)
                {
                    L = R = i;
                    while (R < map.Length && map[R - L] == map[R])
                    {
                        R++;
                    }

                    z[i] = R - L;
                    R--;
                }
                else
                {
                    int k = i - L;
                    if (z[k] < R - i + 1)
                    {
                        z[i] = z[k];
                    }
                    else
                    {
                        L = i;
                        while (R < map.Length && map[R - L] == map[R]) R++;
                        z[i] = R - L; R--;
                    }
                }
            }

            File.WriteAllText("output.txt", string.Join(" ", z.Skip(1)));
        }
    }
}
