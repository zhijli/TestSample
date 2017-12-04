using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSample.Interview
{
    class BinarySearchTree<T> : BinaryTree<T> where T : IComparable<T>
    {
        public void Insert(T data)
        {
            if (Root == null)
            {
                Root = new BinTreeNode<T> { Data = data };
            }

            BinTreeNode<T> current = Root;
            BinTreeNode<T> parent =null;

            while (current != null)
            {
                if (data.CompareTo(current.Data) >= 0)
                {
                    parent = current;
                    current = current.RightChild;
                }
                else
                {
                    parent = current;
                    current = current.LeftChild;
                }
            }
        }
    }
}
