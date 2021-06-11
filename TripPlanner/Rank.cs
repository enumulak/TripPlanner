using System;
using System.Collections.Generic;
using System.Text;

namespace TripPlanner
{
    public class Rank
    {
        private Node node;

        private int rank;

        public Rank(Node node, int rank)
        {
            this.node = node;
            this.rank = rank;
        }

        public Node GetNode()
        {
            return node;
        }

        public int GetRank()
        {
            return rank;
        }
    }
}
