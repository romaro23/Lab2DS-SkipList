using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class SkipList<T>
    {
        private const float Probability = 0.5f;
        private int probOption;
        private int MaxLevel = int.MaxValue / 100;
        private int level;
        private int capacity;
        public int Capacity => capacity;
        private readonly Node<T> head;

        public SkipList(int probOption = 1)
        {
            this.probOption = probOption;
            level = 0;
            capacity = 0;
            head = new Node<T>(-1, default(T), MaxLevel);
        }
        public void SetMaxLevel(string option, int maxLevel = 1)
        {
            if(option == "const")
            {
                MaxLevel = maxLevel;
            }
            else if (option == "func")
            {
                MaxLevel = (int)Math.Log(Capacity + 10, Probability + 1);
            }
        }
        private int GetRandomLevel()
        {
            int lvl = 0;
            Random rand = new Random();
            while (rand.NextDouble() < Probability && level < MaxLevel)
            {
                lvl++;
            }
            return lvl;
        }
        private int GetLevelBasedOnCondition(int nodeIndex)
        {
            int level = 0;
            while (nodeIndex % (1 << level) == 0 && level < MaxLevel)
            {
                level++;
            }

            return level - 1;
        }
        public void Add(int key, T value)
        {
            Node<T>[] update = new Node<T>[MaxLevel + 1];
            Node<T> current = head;
            int position = Capacity + 1;
            for (int i = level; i >= 0; i--)
            {
                while (current.Forward[i] != null && current.Forward[i].Key < key)
                {
                    current = current.Forward[i];

                }
                update[i] = current;
            }
            current = current.Forward[0];
            if (current == null || current.Key != key)
            {
                int newLevel;
                if(probOption == 1)
                {
                    newLevel = GetRandomLevel();
                }
                else
                {
                    newLevel = GetLevelBasedOnCondition(position);
                }
                
                if (newLevel > level)
                {
                    for (int i = level + 1; i <= newLevel; i++)
                    {
                        update[i] = head;
                    }
                    level = newLevel;
                }
                Node<T> newNode = new Node<T>(key, value, newLevel);
                for (int i = 0; i <= newLevel; i++)
                {
                    newNode.Forward[i] = update[i].Forward[i];
                    update[i].Forward[i] = newNode;
                }
                capacity++;
            }
        }
        public T Search(int key)
        {
            Node<T> current = head;
            for (int i = level; i >= 0; i--)
            {
                while (current.Forward[i] != null && current.Forward[i].Key < key)
                {
                    current = current.Forward[i];
                }
            }
            current = current.Forward[0];
            if (current != null && current.Key == key)
            {
                return current.Value;
            }
            throw new KeyNotFoundException("Key not found");
        }
        public void Remove(int key)
        {
            Node<T>[] update = new Node<T>[MaxLevel + 1];
            Node<T> current = head;

            for (int i = level; i >= 0; i--)
            {
                while (current.Forward[i] != null && current.Forward[i].Key < key)
                {
                    current = current.Forward[i];
                }
                update[i] = current;
            }
            current = current.Forward[0];
            if (current != null && current.Key == key)
            {
                for (int i = 0; i <= level; i++)
                {
                    if (update[i].Forward[i] != current) { break; }
                    update[i].Forward[i] = current.Forward[i];
                }

                while (level > 0 && head.Forward[level] == null)
                {
                    level--;
                }
                capacity--;
            }
        }
        public void Clear()
        {
            for (int i = 0; i <= level; i++)
            {
                head.Forward[i] = null;
            }
            level = 0;
            capacity = 0;
        }
        //public void Print()
        //{
        //    for (int i = level; i >= 0; i--)
        //    {
        //        Node<T> node = head.Forward[i];
        //        Console.Write("Level " + i + ": ");
        //        while (node != null)
        //        {
        //            Console.Write(node.Key + " ");
        //            node = node.Forward[i];
        //        }
        //        Console.WriteLine();
        //    }
        //}
        public SkipList<T> Clone()
        {
            SkipList<T> newSkipList = new SkipList<T>(probOption);
            Node<T> current = head.Forward[0];

            while (current != null)
            {
                newSkipList.Add(current.Key, current.Value);
                current = current.Forward[0];
            }

            return newSkipList;
        }
        public void Print()
        {
            List<int> allKeys = new List<int>();
            Node<T> current = head.Forward[0];
            while (current != null)
            {
                allKeys.Add(current.Key);
                current = current.Forward[0];
            }
            for (int i = level; i >= 0; i--)
            {
                Console.Write("Level " + i + ": ");
                current = head.Forward[i];
                int index = 0;
                while (index < allKeys.Count)
                {
                    if (current != null && current.Key == allKeys[index])
                    {
                        Console.Write($" [{current.Key}] ");
                        current = current.Forward[i];
                    }
                    else
                    {
                        Console.Write("     ");
                    }
                    index++;

                }
                Console.WriteLine();
            }
        }


    }
}
