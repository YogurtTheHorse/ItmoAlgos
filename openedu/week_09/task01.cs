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

        public static int Hash(string s, int m)
        {
            int rv = 0;
            foreach (char c in s)
            {
                rv = m * rv + c;
            }
            return rv;
        }

        public static void Main(string[] args)
        {
            _input = File.ReadAllLines("input.txt");

            string 
                p = ReadLine(),
                t = ReadLine();

            var occurences = new List<int>();

            for (int i = 0; i <= t.Length - p.Length; i++)
            {
                if (t.Substring(i).StartsWith(p))
                {
                    occurences.Add(i + 1);
                }
            }



            File.WriteAllText("output.txt", $"{occurences.Count}\n{string.Join(" ", occurences.Select(o => o.ToString()))}");
        }
    }
}
