using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeRacing
{
    public class WaypointNode
    {
        public WaypointNode()
        {
            Weight = 9999999;
            Ways = new List<(double, WaypointNode)>();
        }
        public double Weight { get; set; }
        public List<(double weight, WaypointNode node)> Ways { get; set; }
        public static readonly double FrontLen = 1;
        public static readonly double TurnLen = Math.Sqrt(2);
    }

    public class WaypointDistance
    {
        private void CalcWeights(WaypointNode p1, WaypointNode p2)
        {
            if (p1 == p2)
                return;
            foreach (var way in p1.Ways)
            {
                if (p1.Weight + way.weight < way.node.Weight)
                {
                    way.node.Weight = p1.Weight + way.weight;
                    CalcWeights(way.node, p2);
                }
            }
        }
        private WaypointNode GetNearestNeighbour(WaypointNode p)
        {
            double min = p.Ways.Min(w => w.node.Weight);
            return p.Ways.FirstOrDefault(w => w.node.Weight == min).node;
        }
        public List<WaypointNode> FindWay(WaypointNode p1, WaypointNode p2)
        {
            CalcWeights(p1, p2);
            var result = new List<WaypointNode>();
            result.Add(p2);

            var nearest = GetNearestNeighbour(p2);
            while (nearest != p1)
            {
                result.Add(nearest);
                nearest = GetNearestNeighbour(nearest);
            }
            result.Add(p1);
            result.Reverse();
            return result;
        }
    }
}
