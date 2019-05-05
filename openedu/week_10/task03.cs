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

            string s = ReadLine();
            int[] d = new int[s.Length + 1];
            int[] from = new int[s.Length + 1];
            int[] length = new int[s.Length  + 1];
            for (int i = 0; i < s.Length + 1; i++)
            {
                d[i] = int.MaxValue;
            }
            d[0] = 0;

            for (int i = 0; i < s.Length; i++)
            {
                int[] p = new int[s.Length + 1];
                p[1] = 0;

                if (d[i + 1] > d[i] + 1)
                {
                    d[i + 1] = d[i] + 1;
                    from[i + 1] = i;
                    length[i + 1] = 1;
                }

                int k = 0;
                for (int j = 2; i + j - 1 < s.Length; j++)
                {
                    while (s[i + j - 1] != s[i + k] && k > 0)
                    {
                        k = p[k];
                    }

                    if (s[i + j - 1] == s[i + k])
                    {
                        k++;
                    }

                    p[j] = k;

                    if (j % (j - p[j]) == 0)
                    {
                        if (d[i + j] > d[i] + (j - p[j]))
                        {
                            d[i + j] = d[i] + (j - p[j]);
                            from[i + j] = i;
                            length[i + j] = j - p[j];
                        }
                    }
                }
            }

            StringBuilder sb = new StringBuilder();
            string[] parts = new string[s.Length];
            int[] partsCount = new int[s.Length];
            int al = 0;

            for (int i = s.Length; i > 0;)
            {
                parts[al] = s.Substring(from[i], length[i]);
                partsCount[al] = (i - from[i]) / length[i];
                al++;
                i = from[i];
            }

            int maxAl = al - 1;
            for (al--; al >= 0; al--)
            {
                if (al != maxAl)
                {
                    sb.Append("+");
                }
                sb.Append(parts[al]);
                if (partsCount[al] > 1)
                {
                    sb.Append("*");
                    sb.Append(partsCount[al]);
                }
            }

            File.WriteAllText("output.txt", sb.ToString());
        }
    }
}
