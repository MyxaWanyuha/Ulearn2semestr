using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTrees
{
    public class BinaryTree<T> where T : IComparable
    {
        class Node
        {
            public T Data { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public Node()
            {

            }
            public Node(T data)
            {
                Data = data;
            }
        }
        private Node _root;
        public BinaryTree()
        {
            _root = null;
        }
        public void Add(T data)
        {
            // 1. If the tree is empty, return a new, single node 
            if (_root == null)
            {
                _root = new Node(data);
                return;
            }
            // 2. Otherwise, recur down the tree 
            InsertRec(_root, new Node(data));
        }
        private void InsertRec(Node root, Node newNode)
        {
            if (root == null)
                root = newNode;

            if (newNode.Data < root.Data)
            {
                if (root.Left == null)
                    root.Left = newNode;
                else
                    InsertRec(root.Left, newNode);
            }
            else
            {
                if (root.Right == null)
                    root.Right = newNode;
                else
                    InsertRec(root.Right, newNode);
            }
        }

        public void A2dd(T key)
        {
            throw new NotImplementedException();
        }

        public bool Contains(T key)
        {
            throw new NotImplementedException();
        }
    }
}
