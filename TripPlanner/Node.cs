using System;
using System.Collections.Generic;
using System.Text;

namespace TripPlanner
{
    public class Node
    {
        private int key;

        private List<Node> children;

        private Node deepestChild;


        public Node(int key)
        {
            this.key = key;
            children = new List<Node>();
        }

        public int GetKey()
        {
            return key;
        }

        public void AddChild(Node child)
        {
            children.Add(child);
        }

        public List<Node> GetChildren()
        {
            return children;
        }

        public Node GetDeepestChild()
        {
            return deepestChild;
        }

        public void SetDeepestChild(Node child)
        {
            deepestChild = child;
        }
    }
}
