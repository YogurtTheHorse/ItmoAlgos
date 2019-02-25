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

        private static int Partition(int f, int l)
        {
            int pivotValue = a[f];
            int leftMark = f + 1, rightMark = l;

            while (rightMark >= leftMark)
            {
                while (leftMark <= rightMark && a[leftMark] <= pivotValue)
                {
                    leftMark++;
                }

                while (rightMark >= leftMark && a[rightMark] >= pivotValue)
                {
                    rightMark--;
                }

                if (rightMark >= leftMark)
                {
                    Swap(leftMark, rightMark);
                }
            }

            Swap(f, rightMark);

            return rightMark;
        }

        private static void Swap(int left, int right)
        {
            if (left == right) return;

            if (left > right)
            {
                int tmo = left;
                left = right;
                right = left;
            }

            sw.WriteLine($"Swap elements at indices {left + 1} and {right + 1}.");

            int tmp = a[left];
            a[left] = a[right];
            a[right] = tmp;
        }

        private static void Qsort(int f, int l)
        {
            while (f < l)
            {
                int splitPoint = Partition(f, l);

                if (splitPoint - f < l - splitPoint)
                {
                    Qsort(f, splitPoint - 1);
                    f = splitPoint + 1;
                }
                else
                {
                    Qsort(splitPoint + 1, l);
                    l = splitPoint - 1;
                }
            }
            
        }

        static void Main(string[] args)
        {
            sr = new StreamReader("input.txt");
            sw = new StreamWriter("output.txt", false);

            int n = int.Parse(sr.ReadLine());
            a = sr.ReadLine()
                .Split()
                .Select(s => int.Parse(s))
                .ToArray();
            sr.Close();

            Qsort(0, a.Length - 1);
            sw.WriteLine("No more swaps needed.");
            sw.WriteLine(string.Join(" ", a));

            sw.Close();
        }
    }
}
