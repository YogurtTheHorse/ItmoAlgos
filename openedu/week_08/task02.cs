using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ItmoAlgos
{
    public class Program
    {
        private static string[] _input;
        private static int _currentLineIndex;

        private static string ReadLine()
        {
            return _input[_currentLineIndex++];
        }

        public static void Main(string[] args)
        {
            var dictionary = new Dictionary<string, LinkedListNode<string>>();
            var linkedList = new LinkedList<string>();
            var output = new StringBuilder();
            var emptyNode = new LinkedListNode<string>("<none>");
            LinkedListNode<string> node;

            _input = File.ReadAllLines("input.txt");

            long n = long.Parse(ReadLine());


            for (long i = 0; i < n; i++)
            {
                string[] strings = ReadLine().Split();
                string key = strings[1];
                string value = strings.Length == 3 ? strings[2] : null;

                switch (strings[0])
                {
                    case "put":
                        if (dictionary.TryGetValue(key, out node))
                        {
                            node.Value = value;
                        }
                        else
                        {
                            dictionary[key] = linkedList.AddLast(value);
                        }
                        break;

                    case "get":
                        output.AppendLine((dictionary.GetValueOrDefault(key) ?? emptyNode).Value);
                        break;

                    case "prev":
                        output.AppendLine((dictionary[key].Previous ?? emptyNode).Value);
                        break;

                    case "next":
                        output.AppendLine((dictionary[key].Next ?? emptyNode).Value);
                        break;

                    case "delete":
                        if (dictionary.Remove(key, out node))
                        {
                            linkedList.Remove(node);
                        }
                        break;
                }
            }

            File.WriteAllText("output.txt", output.ToString());
        }
    }
}
