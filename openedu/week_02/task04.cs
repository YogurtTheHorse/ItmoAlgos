using System;
using System.IO;
using System.Linq;

namespace Temp
{
    public static class Program
    {
        private static StreamReader sr;
        private static StreamWriter sw;
        private static int[] a;
        private static int n, k1, k2, A, B, C;

        public static void Quicksort(int[] elements, int left, int right)
        {
            while (true)
            {
                if (left > k2 || right < k1) return;

                int i = left, j = right;
                int pivot = elements[(left + right) / 2];

                while (i <= j)
                {
                    while (elements[i].CompareTo(pivot) < 0)
                    {
                        i++;
                    }

                    while (elements[j].CompareTo(pivot) > 0)
                    {
                        j--;
                    }

                    if (i > j) continue;

                    int tmp = elements[i];
                    elements[i] = elements[j];
                    elements[j] = tmp;

                    i++;
                    j--;
                }

                if (left < j)
                {
                    Quicksort(elements, left, j);
                }

                if (i < right)
                {
                    left = i;
                    continue;
                }

                break;
            }
        }

        public static void Main(string[] args)
        {
            sr = new StreamReader("input.txt");
            sw = new StreamWriter("output.txt", false);

            var inp = sr.ReadLine().Split().Select(int.Parse).ToArray();
            n = inp[0];
            k1 = inp[1] - 1;
            k2 = inp[2] - 1;
            
            inp = sr.ReadLine().Split().Select(int.Parse).ToArray();

            A = inp[0];
            B = inp[1];
            C = inp[2];
            a = new int[n];
            a[0] = inp[3];
            a[1] = inp[4];
            sr.Close();

            for (var i = 2; i < n; i++)
            {
                a[i] = A * a[i - 2] + B * a[i - 1] + C;
            }

            Quicksort(a, 0, a.Length - 1);
            for (int i = k1; i <= k2; i++)
            {
                sw.Write($"{a[i]} ");
            }

            sw.Close();
        }
    }
}
