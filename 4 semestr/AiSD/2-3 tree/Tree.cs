using System;
using System.Collections.Generic;
using System.Text;

namespace proj3
{
    public class Tree234
    {
        private Node Root { get; set; }

        public void Insert(int value)
        {
            if (Root != null)
            {
                InsertInternal(value, Root);
            }
            else
            {
                Root = new Node(value);
            }
        }

        private void InsertInternal(int value, Node node)
        {

            if (node.Values.Count == 3)
            {
                node.Children = Split(node);
                node.Values.RemoveAt(2);
                node.Values.RemoveAt(0);

            }

            int childIndexToFollow = node.GetChildIndexToFollow(value);

            if (childIndexToFollow == -1)//Узел это пустой лист
            {
                node.AddValue(value);
            }
            else
            {
                Node childToFollow = node.Children[childIndexToFollow];

                if (childToFollow.Values.Count == 3)
                { //При 3 значениях в узле, нужно раделить узел
                    node.AddValue(childToFollow.Values[1]); //Среднее значение

                    var newChildren = Split(childToFollow);
                    node.Children.Remove(childToFollow);
                    node.AddChildren(newChildren);
                }

                childIndexToFollow = node.GetChildIndexToFollow(value);
                InsertInternal(value, node.Children[childIndexToFollow]);
            }

        }

        private List<Node> Split(Node node)
        {
            // Чтобы выполнить функцию разбиения узла в нем должно быть 3 значения

            var newChildren = new List<Node>();

            Node leftSubTree = new Node(node.Values[0]);
            newChildren.Add(leftSubTree);

            Node rightSubTree = new Node(node.Values[2]);
            newChildren.Add(rightSubTree);

            if (node.GetDegree() != 0)
            {
                leftSubTree.AddChild(node.Children[0]);
                leftSubTree.AddChild(node.Children[1]);
                rightSubTree.AddChild(node.Children[2]);
                rightSubTree.AddChild(node.Children[3]);
            }


            return newChildren;
        }

        public void Delete(int target)
        {
            if (Root != null)
            {
                DeleteInternal(target, Root);
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        private void DeleteInternal(int target, Node node)
        {

            if (node.Values.Contains(target))
            {
                if (node.IsLeaf())
                {
                    node.Values.Remove(target);
                    return;
                }
                else
                {
                    int predecessorIndex = node.GetChildPredecessorIndex(target);
                    Node predecessorNode = node.Children[predecessorIndex];
                    int predecessorValue = predecessorNode.Values[predecessorNode.Values.Count - 1];

                    int successorIndex = node.GetChildSuccessorIndex(target);
                    Node successorNode = node.Children[successorIndex];
                    int successorValue = successorNode.Values[0]; 






                    if (predecessorNode.Values.Count > 1)
                    {
                        node.Values[node.Values.IndexOf(target)] = predecessorValue;
                        DeleteInternal(predecessorValue, predecessorNode);


                    }
                    else if (successorNode.Values.Count > 1)
                    {
                        node.Values[node.Values.IndexOf(target)] = successorValue;
                        DeleteInternal(successorValue, successorNode);


                    }
                    else
                    { 
                        
                        Node mergedNode = new Node(target);
                        mergedNode.AddValue(predecessorValue);
                        mergedNode.AddValue(successorValue);

                        
                        mergedNode.AddChildren(predecessorNode.Children);
                        mergedNode.AddChildren(successorNode.Children);

                        
                        node.Values.Remove(target);
                        node.Children.Remove(predecessorNode);
                        node.Children.Remove(successorNode);

                        node.AddChild(mergedNode);

                        DeleteInternal(target, mergedNode); //Вызываем рекурсивно, чтобы удалить target
                    }
                }
            }
            else
            { // Ищем узел со значениями

                int childIndexToFollow = node.GetChildIndexToFollow(target);
                Node childToFollow = node.Children[childIndexToFollow];

                Node sibling;
                int siblingValueIndex;

                if (childToFollow.Values.Count == 1)
                {
                    if (childIndexToFollow > 0 && node.Children[childIndexToFollow - 1].Values.Count >= 2)
                    { 

                        int pivotValueIndex = childIndexToFollow - 1;
                        childToFollow.AddValue(node.Values[pivotValueIndex]); // Rot value to child

                        sibling = node.Children[childIndexToFollow - 1];
                        siblingValueIndex = sibling.Values.Count - 1; // Индекс правого значения левого поддерева

                        node.Values[pivotValueIndex] = sibling.Values[siblingValueIndex]; // Возвращаем значение sibling в узел
                        sibling.Values.RemoveAt(siblingValueIndex);                       //
                        if (sibling.GetDegree() != 0)
                        {
                            childToFollow.AddChild(sibling.Children[siblingValueIndex + 1]);    // Возвращаем ребенка
                            sibling.Children.RemoveAt(siblingValueIndex + 1);                   
                        }

                    }
                    else if (childIndexToFollow < node.GetDegree() - 1 && node.Children[childIndexToFollow + 1].Values.Count >= 2)
                    { // Правое поддерево существует и имеет хотя бы 2 значения
                       

                        int pivotValueIndex = childIndexToFollow;
                        childToFollow.AddValue(node.Values[pivotValueIndex]); 

                        sibling = node.Children[childIndexToFollow + 1];
                        siblingValueIndex = 0; 

                        node.Values[pivotValueIndex] = sibling.Values[siblingValueIndex]; 
                        sibling.Values.RemoveAt(siblingValueIndex);                       
                        if (sibling.GetDegree() != 0)
                        {
                            childToFollow.AddChild(sibling.Children[siblingValueIndex]);    
                            sibling.Children.RemoveAt(siblingValueIndex);                   
                        }

                    }
                    else
                    { // merge case

                        if (node == Root && node.GetDegree() == 2)
                        {
                            PromoteChildren(node);
                        }
                        else
                        {

                            if (childIndexToFollow > 0 && node.Children[childIndexToFollow - 1].Values.Count == 1)
                            {
                                DemoteValue(node, childIndexToFollow - 1, childIndexToFollow, childIndexToFollow - 1);  // demote value and merge with left sibling

                            }
                            else if (childIndexToFollow < node.Children.Count - 1 && node.Children[childIndexToFollow + 1].Values.Count == 1)
                            {

                                DemoteValue(node, childIndexToFollow, childIndexToFollow, childIndexToFollow + 1);  // demote value and merge with right sibling

                            }
                            else
                            {
                                throw new Exception("Bruh");
                            }
                        }
                    }
                }

                childIndexToFollow = node.GetChildIndexToFollow(target);
                DeleteInternal(target, node.Children[childIndexToFollow]);
            }
        }

        public void PromoteChildren(Node node)
        {

            foreach (Node child in node.Children)
            {
                node.AddValue(child.Values[0]); 
            }

            List<Node> children = node.Children;
            node.Children = null;                 
            foreach (Node child in children)
            {
                node.AddChildren(child.Children); // Добавляем правнуков
            }
        }

        public void DemoteValue(Node node, int parentValueIndex, int childToFollowIndex, int siblingIndex)
        {

            Node childA = node.Children[childToFollowIndex];
            Node childB = node.Children[siblingIndex];

            Node mergedNode = new Node(node.Values[parentValueIndex]);
            mergedNode.AddValue(childA.Values[0]);
            mergedNode.AddValue(childB.Values[0]);

            node.Values.RemoveAt(parentValueIndex);

            mergedNode.AddChildren(childA.Children);
            mergedNode.AddChildren(childB.Children);

            node.Children.Remove(childA);
            node.Children.Remove(childB);
            node.AddChild(mergedNode);
        }


        //Следующие три функции формируют вывод дерева
        public string Preorder(Node node)
        {
            string output = "";

            if (node.Children.Count > 0)
            {
                output += Preorder(node.Children[0]) + " ";
            }
            if (node.Values.Count > 0)
            {
                output += node.Values[0].ToString() + " ";
            }

            if (node.Children.Count > 1)
            {
                output += Preorder(node.Children[1]) + " ";
            }
            if (node.Values.Count > 1)
            {
                output += node.Values[1].ToString() + " ";
            }

            if (node.Children.Count > 2)
            {
                output += Preorder(node.Children[2]) + " ";
            }
            if (node.Values.Count > 2)
            {
                output += node.Values[2].ToString() + " ";
            }

            if (node.Children.Count > 3)
            {
                output += Preorder(node.Children[3]) + " ";
            }

            return "[ " + output + "]";
        }

        public override string ToString()
        {

            StringBuilder sb = new StringBuilder("");

            for (int i = 1; i <= Root.GetHeight(); i++)
            {
                AppendLevelString(Root, i, sb);
                sb.Append("\n\n");
            }

            return sb.ToString();
        }

        public void AppendLevelString(Node node, int level, StringBuilder sb)
        {
            if (level == 1)
            {
                sb.Append(node.ToPaddedString());
            }
            else if (level > 1)
            {
                foreach (Node child in node.Children)
                {
                    AppendLevelString(child, level - 1, sb);
                }
            }
        }
    }
}