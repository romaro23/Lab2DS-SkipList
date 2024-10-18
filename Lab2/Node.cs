using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class Node<T>
    {
        public int Key { get; set; }
        public T Value { get; set; }
        public Node<T>[] Forward { get; set; }

        public Node(int key, T value, int level)
        {
            Key = key;
            Value = value;
            Forward = new Node<T>[level + 1];
        }
    }
}
