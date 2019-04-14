using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace ItmoAlgos
{
    public struct ModificationResult
    {
        public bool Modified { get; set; }
        public int HeightDelta { get; set; }
    }

    public sealed class Node<T> where T : IComparable<T>
    {
        public Node(T v, Node<T> left = null, Node<T> right = null, bool refreshProperties = true)
        {
            if (v == null) throw new ArgumentNullException(nameof(v));

            Value = v;
            SetLeftChild(left, refreshProperties);
            SetRightChild(right, refreshProperties);
        }

        public T Value { get; private set; }
        public Node<T> Left { get; private set; }
        public Node<T> Right { get; private set; }
        public Node<T> Parent { get; private set; }

        public int Height { get; private set; }

        public int Balance { get; private set; }

        public bool HasChildren => Left != null || Right != null;

        public void SetChild(Node<T> child, bool refreshProperties = true)
        {
            if (child is null) throw new InvalidOperationException("Use explicit methods for null values.");

            int comp = Value.CompareTo(child.Value);
            if (comp < 0)
                SetRightChild(child, refreshProperties);
            else if (comp > 0)
                SetLeftChild(child, refreshProperties);
            else
                throw new InvalidOperationException("Cannot set child with the same value");
        }

        public void RemoveChild(Node<T> child, bool refreshProperties = true)
        {
            if (child is null) throw new InvalidOperationException("Use explicit methods for null values.");

            int comp = Value.CompareTo(child.Value);
            if (comp < 0)
                SetRightChild(null, refreshProperties);
            else if (comp > 0)
                SetLeftChild(null, refreshProperties);
            else
                throw new InvalidOperationException("Cannot set child with the same value");
        }

        public void SetLeftChild(Node<T> left, bool refreshProperties = true)
        {
            if (left != null)
            {
                if (left.Value.CompareTo(Value) >= 0) throw new InvalidOperationException();

                left.Parent?.RemoveChild(left);
                left.Parent = this;
            }

            if (Left != null) Left.Parent = null;

            Left = left;
            if (refreshProperties) RefreshOwnAndParentProperties();
        }

        public void SetRightChild(Node<T> right, bool refreshProperties = true)
        {
            if (right != null)
            {
                if (right.Value.CompareTo(Value) <= 0) throw new InvalidOperationException();

                right.Parent?.RemoveChild(right);
                right.Parent = this;
            }

            if (Right != null) Right.Parent = null;

            Right = right;

            if (refreshProperties) RefreshOwnAndParentProperties();
        }

        public Node<T> LeftTurn()
        {
            if (Right?.Balance < 0) return BigLeftTurn();

            Node<T>
                a = this,
                b = Right,
                y = b.Left;

            if (a.Parent != null)
                a.Parent.SetChild(b);
            else
                b.Parent = a.Parent;

            a.SetRightChild(y);
            b.SetLeftChild(a);

            return b;
        }

        public Node<T> RightTurn()
        {
            if (Left?.Balance > 0) return BigRightTurn();

            Node<T>
                a = this,
                b = a.Left,
                y = b.Right;

            if (a.Parent != null)
                a.Parent.SetChild(b);
            else
                b.Parent = a.Parent;

            a.SetLeftChild(y);
            b.SetRightChild(a);

            return b;
        }

        public override string ToString()
        {
            return $"Node({Value}, ({Left}, {Right})";
        }

        public string ToPrintableString()
        {
            var sb = new StringBuilder();
            var queue = new Queue<Node<T>>();
            long counter = 1;
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                Node<T> current = queue.Dequeue();
                sb.Append(current.Value);

                if (current.Left != null)
                {
                    queue.Enqueue(current.Left);
                    sb.Append($" {++counter}");
                }
                else
                {
                    sb.Append(" 0");
                }


                if (current.Right != null)
                {
                    queue.Enqueue(current.Right);
                    sb.Append($" {++counter}");
                }
                else
                {
                    sb.Append(" 0");
                }

                sb.Append("\n");
            }

            return sb.ToString();
        }

        public Node<T> Insert(T x)
        {
            Node<T> current = this;
            var inserted = false;

            while (!inserted)
            {
                int comp = current.Value.CompareTo(x);
                if (comp == 0) throw new DuplicateNameException();

                if (comp < 0)
                {
                    if (current.Right != null)
                    {
                        current = current.Right;
                    }
                    else
                    {
                        current.SetRightChild(new Node<T>(x));
                        inserted = true;
                    }
                }
                else
                {
                    if (current.Left != null)
                    {
                        current = current.Left;
                    }
                    else
                    {
                        current.SetLeftChild(new Node<T>(x));
                        inserted = true;
                    }
                }
            }

            return current.BalanceUpward();
        }

        public Node<T> Remove(T x)
        {
            Node<T> current = this;
            var removed = false;

            while (!removed)
            {
                int comp = current.Value.CompareTo(x);
                if (comp == 0)
                {
                    current = RemoveFoundVertex(current);

                    removed = true;
                }
                else if (comp < 0)
                {
                    if (current.Right != null)
                        current = current.Right;
                    else
                        throw new InvalidOperationException();
                }
                else
                {
                    if (current.Left != null)
                        current = current.Left;
                    else
                        throw new InvalidOperationException();
                }
            }

            return current.BalanceUpward();
        }

        private static Node<T> RemoveFoundVertex(Node<T> current)
        {
            Node<T> toRemove = current;
            current = current.Parent;

            if (toRemove.HasChildren)
            {
                if (toRemove.Left == null)
                {
                    if (current is null)
                    {
                        current = toRemove.Right;
                        current.Parent = null;
                    }
                    else
                    {
                        current.SetChild(toRemove.Right);
                    }
                }
                else
                {
                    Node<T> maximum = toRemove.Left.Maximum();
                    current = maximum.Parent;
                    if (maximum.HasChildren)
                        current.SetChild(maximum.Left);
                    else
                        current.RemoveChild(maximum);

                    toRemove.Value = maximum.Value;
                }
            }
            else
            {
                current.RemoveChild(toRemove);
            }

            return current;
        }

        private Node<T> Maximum()
        {
            Node<T> current = this;
            while (current.Right != null) current = current.Right;

            return current;
        }

        private Node<T> BalanceUpward()
        {
            Node<T> current = this;
            Node<T> res = current;

            while (current != null)
                if (current.Balance > 1)
                {
                    current = current.LeftTurn();
                }
                else if (current.Balance < -1)
                {
                    current = current.RightTurn();
                }
                else
                {
                    res = current;
                    current = current.Parent;
                }

            return res;
        }

        public void RefreshTree()
        {
            Left?.RefreshTree();
            Right?.RefreshTree();

            RefreshOwnProperties();
        }

        public void RefreshOwnProperties()
        {
            Height = 1 + Math.Max(Left?.Height ?? 0, Right?.Height ?? 0);
            Balance = (Right?.Height ?? 0) - (Left?.Height ?? 0);
        }

        private void RefreshOwnAndParentProperties()
        {
            Node<T> node = this;

            while (node != null)
            {
                node.RefreshOwnProperties();
                node = node.Parent;
            }
        }

        private Node<T> BigLeftTurn()
        {
            Node<T>
                a = this,
                b = a.Right,
                c = b.Left,
                x = c.Left,
                y = c.Right;

            // replace a with c
            if (a.Parent != null)
                a.Parent.SetChild(c);
            else
                c.Parent = a.Parent;


            b.SetLeftChild(y);
            a.SetRightChild(x);

            c.SetLeftChild(a);
            c.SetRightChild(b);

            return c;
        }

        private Node<T> BigRightTurn()
        {
            Node<T>
                a = this,
                b = a.Left,
                c = b.Right,
                x = c.Left,
                y = c.Right;

            // replace a with c
            if (a.Parent != null)
                a.Parent?.SetChild(c);
            else
                c.Parent = a.Parent;

            b.SetRightChild(x);
            a.SetLeftChild(y);

            c.SetLeftChild(b);
            c.SetRightChild(a);

            return c;
        }

        public bool Contains(T x)
        {
            Node<T> current = this;
            
            while (true)
            {
                int comp = current.Value.CompareTo(x);
                if (comp == 0)
                {
                    return true;
                }
                
                if (comp < 0)
                {
                    if (current.Right != null)
                        current = current.Right;
                    else
                        return false;
                }
                else
                {
                    if (current.Left != null)
                        current = current.Left;
                    else
                        return false;
                }
            }
        }
    }

    public class Tree<T> where T : IComparable<T>
    {
        private Node<T> _root;
        public int Balance => _root?.Balance ?? 0;

        public void Insert(T x)
        {
            try
            {
                _root = _root == null ? new Node<T>(x) : _root.Insert(x);
            }
            catch (DuplicateNameException)
            {
                // nothing.
            }
        }

        public void Remove(T x)
        {
            if (_root is null) return;

            if (_root.Value.CompareTo(x) == 0 && !_root.HasChildren)
            {
                _root = null;
            }
            else
            {
                try
                {
                    _root = _root.Remove(x);
                }
                catch (InvalidOperationException)
                {
                    // nothing
                }
            }
        }

        public bool ContainsValue(T x)
        {
            return _root?.Contains(x) ?? false;
        }
    }

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
            _input = File.ReadAllLines("input.txt");

            int n = int.Parse(ReadLine());
            var t = new Tree<int>();


            using (var sw = new StreamWriter("output.txt"))
            {
                for (var i = 0; i < n; i++)
                {
                    string[] cmdParts = ReadLine().Split();
                    int x = int.Parse(cmdParts[1]);

                    switch (cmdParts[0][0])
                    {
                        case 'A':
                            t.Insert(x);
                            sw.WriteLine(t.Balance);
                            break;
                        
                        case 'D':
                            t.Remove(x);
                            sw.WriteLine(t.Balance);
                            break;
                        
                        case 'C':
                            sw.WriteLine(t.ContainsValue(x) ? "Y" : "N");
                            break;
                    }
                }
            }
        }
    }
}
