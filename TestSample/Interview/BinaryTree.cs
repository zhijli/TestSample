using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSample.Interview
{
    public class BinTreeNode<T>
    {
        public BinTreeNode<T> LeftChild;
        public BinTreeNode<T> RightChild;
        public T Data;
    }

    public class BinaryTree<T>
    {
        public BinTreeNode<T> Root { get; set; }

        public int Height { get; }

        public void PreVisit(Action<T> action)
        {
            BinTreeNode<T> current = Root;
            var stack = new Stack<BinTreeNode<T>>();

            while (current != null || stack.Count > 0)
            {
                while (current != null)
                {
                    action(current.Data);
                    stack.Push(current);
                    current = current.LeftChild;
                }

                current = stack.Pop();
                current = current.RightChild;
                while (current == null)
                {
                    current = stack.Pop();
                    current = current.RightChild;
                }
            }
        }

        public void InVisit() { }
    }
}
