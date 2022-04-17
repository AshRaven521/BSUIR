using System.Collections.Generic;
 

namespace ConsoleApp1
{
    public class TreeList<T> : List<T>
    {
        public T Left = default;
        public T Right = default;
        public TreeList<T> Parent = null;
        public TreeList<T> Block = null;
        public int Indentation;

        public TreeList(TreeList<T> treeList)
        {
            Block = treeList;
        }


    }
}