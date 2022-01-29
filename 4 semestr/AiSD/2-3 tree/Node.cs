using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace proj3
{
    public class Node
    {
        public List<int> Values { get; set; }
        public List<Node> Children { get; set; }

        public Node(int value)
        {
            Values = new List<int>();
            Children = new List<Node>();
            Values.Add(value);
        }

        public bool HasSpace() 
        { 
            return Values.Count != 3; 
        }
        public int GetDegree() 
        {
            return Children.Count;
        }
        public bool IsLeaf() 
        {
            return Children.Count == 0; 
        }

      
        public int GetChildIndexToFollow(int val)
        {
            int numVals = Values.Count;

            if (GetDegree() == 0)
            {
                return -1; // Значит, не следуем за ребенком, цель устанавливается на текущий узел
            }

            int i;
            for (i = 0; i < Values.Count; i++)
            {
                if (val <= Values[i])
                {
                    return i;
                }
            }
            return i;
        }

        public int GetChildPredecessorIndex(int target)
        {
            return GetChildIndexToFollow(target - 1);
        }
        public int GetChildSuccessorIndex(int target)
        {
            return GetChildIndexToFollow(target + 1);
        }

        public void AddValue(int value)
        {
            if (Values.Contains(value))
            {
                return;
            }
            Values.Add(value);
            Values.Sort();
        }

        public void AddChild(Node child)//Добавляем одного
        {
            Children.Add(child);
            Children = Children.OrderBy(c => c.Values[0]).ToList();
        }

        public void AddChildren(List<Node> children)//Добавляем нескольких
        {
            foreach (Node child in children)
            {
                Children.Add(child);
            }
            Children = Children.OrderBy(c => c.Values[0]).ToList();
        }

        public int GetHeight()
        {
            int height = 0;

            foreach (Node child in Children)
            {
                int childHeight = child.GetHeight();
                if (childHeight > height)
                {
                    height = childHeight;
                }
            }

            return height + 1;
        }

        //Следующими двумя функциями формируем вывод узла
        public override string ToString()
        {
            string output = "[";

            for (int i = 0; i < Values.Count; i++)
            {
                int val = Values[i];
                string spaceAfter = "";

                if (i != Values.Count - 1)
                {
                    if (val < 10)
                    {
                        spaceAfter = "  ";
                    }
                    else
                    {
                        spaceAfter = " ";
                    }
                }
                else if (val < 10)
                {
                    spaceAfter = "";
                }

                output += val + spaceAfter;
            }

            return output + "] ";
        }

        public string ToPaddedString()
        {
            int childrenStringWidth = 0;
            childrenStringWidth = Children.Sum(child => child.ToPaddedString().Length);

            string spacedString = ToString();

            if (childrenStringWidth != 0)
            {
                int spaces = childrenStringWidth - spacedString.Length;
                int padLeft = spaces / 2 + spacedString.Length;
                spacedString = spacedString.PadLeft(padLeft).PadRight(childrenStringWidth);
            }

            return spacedString;
        }
    }
}