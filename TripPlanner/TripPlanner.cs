using System;
using System.Collections.Generic;
using System.Text;

namespace TripPlanner
{
    public class TripPlanner
    {
        
        
        // Solution Provider
        public int[] Solver(int K, int[] T)
        {
            int N = T.Length;

            int[] valueToRank = new int[N];
            int[] rankToIndex = new int[N];

            bool[] isLeaf = new bool[N];

            Node root = CreateGraph(T, 2);

            DeepestChild(root);

            Queue<Rank> rQueue = new Queue<Rank>();
            rQueue.Enqueue(new Rank(root, 0));

            List<List<int>> rank = new List<List<int>>(N);

            for (int i = 0; i < N; i++)
            {
                rank.Add(new List<int>());
            }

            while (rQueue.Count > 0)
            {
                Rank rankedNode = rQueue.Dequeue();
                Node currentNode = rankedNode.GetNode();

                int currentValue = currentNode.GetKey();
                int currentRank = rankedNode.GetRank();

                List<Node> children = currentNode.GetChildren();
                Node deepestChildren = currentNode.GetDeepestChild();

                int sizeOfChildren = children.Count;

                // Leaf - Level
                if (sizeOfChildren == 0)
                {
                    List<int> rankList = rank[currentRank];
                    rankList.Add(currentValue);

                    valueToRank[currentValue] = currentRank;
                    isLeaf[currentValue] = true;
                }

                for (int i = 0; i < sizeOfChildren; i++)
                {
                    Node currentChild = children[i];

                    int currentChildRank = 1 + ((currentChild == deepestChildren) ? currentRank : 0);

                    rQueue.Enqueue(new Rank(currentChild, currentChildRank));
                }
            }

            for (int i = N - 1, currentIndex = 0; i >= 0; i--)
            {
                List<int> rankList = rank[i];
                int rankListSize = rankList.Count;

                if (rankListSize == 0)
                {
                    continue;
                }

                rankToIndex[i] = currentIndex;
                currentIndex += rankListSize;
            }

            int[] answer = new int[N];
            int highestIndexSet = 0;

            for (int i = 0; i < N; i++)
            {

                // Move downward if node is a leaf

                if (!isLeaf[i])
                {
                    continue;
                }

                int rankI = valueToRank[i];
                int index = rankToIndex[rankI];

                rankToIndex[rankI]++;

                answer[index] = i;
                highestIndexSet = Math.Max(highestIndexSet, index);
            }

            int[] result = new int[highestIndexSet + 2];
            result[0] = K;

            for (int i = 0; i <= highestIndexSet; i++)
            {
                result[i + 1] = answer[i];
            }

            return result;
        }

        // Create a graph with the provided array and starting node
        public Node CreateGraph(int[] T, int K)
        {
            int N = T.Length;

            Node[] nodes = new Node[N];

            for (int i = 0; i < N; i++)
            {
                nodes[i] = new Node(i);
            }

            Node root = null;

            for (int i = 0; i < T.Length; i++)
            {
                if (T[i] == 1)
                {
                    root = nodes[i];
                }
                else
                {
                    nodes[T[i]].AddChild(nodes[i]);
                }
            }

            return root;
        }

        // Helper function to set deepest children for all nodes
        // If there are 2 nodes with the same depth, node with the lesser value will be set as the deepest node
        public int DeepestChildHelper(Node node, int depth)
        {
            int maxDepth = 0;
            Node maxChild = null;

            List<Node> children = node.GetChildren();
            int sizeOfChildren = children.Count;

            if (sizeOfChildren == 0)
            {
                return depth;
            }

            for (int i = 0; i < sizeOfChildren; i++)
            {

                Node currentChild = children[i];

                int currentChildDepth = DeepestChildHelper(currentChild, depth + 1);

                if (maxChild == null || currentChildDepth > maxDepth
                        || (currentChildDepth == maxDepth && currentChild.GetKey() < maxChild.GetKey()))
                {

                    maxDepth = currentChildDepth;
                    maxChild = currentChild;
                }
            }

            node.SetDeepestChild(maxChild);
            return maxDepth;
        }

        // Set deepest children for all nodes
        public int DeepestChild(Node root)
        {
            return DeepestChildHelper(root, 0);
        }


    }
}
