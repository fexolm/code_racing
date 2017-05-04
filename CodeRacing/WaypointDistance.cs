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
            Up = Down = URight = ULeft = DRight = DLeft = Deadlock;
        }
        public double Weight { get; set; }
        public WaypointNode Up { get; set; }
        public WaypointNode ULeft { get; set; }
        public WaypointNode URight { get; set; }
        public WaypointNode Down { get; set; }
        public WaypointNode DLeft { get; set; }
        public WaypointNode DRight { get; set; }
        public static readonly WaypointNode Deadlock = new WaypointNode();
        public static readonly double FrontLen = 1;
        public static readonly double TurnLen = Math.Sqrt(2);
    }

    public class WaypointDistance
    {
        private void CalcWeights(WaypointNode p1, WaypointNode p2)
        {
            if (p1 == p2)
                return;
            if (p1.Up != WaypointNode.Deadlock)
            {
                if(p1.Weight + WaypointNode.FrontLen < p1.Up.Weight)
                {
                    p1.Up.Weight = p1.Weight + 1;
                    CalcWeights(p1.Up, p2);
                }
            }
            if (p1.Down != WaypointNode.Deadlock)
            {
                if (p1.Weight + WaypointNode.FrontLen < p1.Down.Weight)
                {
                    p1.Down.Weight = p1.Weight + 1;
                    CalcWeights(p1.Down, p2);
                }
            }
            if (p1.URight != WaypointNode.Deadlock)
            {
                if (p1.Weight + WaypointNode.TurnLen < p1.URight.Weight)
                {
                    p1.URight.Weight = p1.Weight + WaypointNode.TurnLen;
                    CalcWeights(p1.URight, p2);
                }
            }
            if (p1.ULeft != WaypointNode.Deadlock)
            {
                if (p1.Weight + WaypointNode.TurnLen < p1.ULeft.Weight)
                {
                    p1.ULeft.Weight = p1.Weight + WaypointNode.TurnLen;
                    CalcWeights(p1.ULeft, p2);
                }
            }
            if (p1.DRight != WaypointNode.Deadlock)
            {
                if (p1.Weight + WaypointNode.TurnLen < p1.DRight.Weight)
                {
                    p1.DRight.Weight = p1.Weight + WaypointNode.TurnLen;
                    CalcWeights(p1.DRight, p2);
                }
            }
            if (p1.DLeft != WaypointNode.Deadlock)
            {
                if (p1.Weight + WaypointNode.TurnLen < p1.DLeft.Weight)
                {
                    p1.DLeft.Weight = p1.Weight + WaypointNode.TurnLen;
                    CalcWeights(p1.DLeft, p2);
                }
            }
        }
        private WaypointNode GetNearestNeighbour(WaypointNode p)
        {
            var arr = new WaypointNode[] {p.Up, p.Down, p.DLeft, p.DRight, p.URight, p.ULeft };
            double min = arr.Min(x => x.Weight);
            return arr.FirstOrDefault(w => w.Weight == min);
        }
        public List<WaypointNode> FindWay(WaypointNode p1, WaypointNode p2)
        {
            CalcWeights(p1, p2);
            var result = new List<WaypointNode>();
            result.Add(p2);

            var nearest = GetNearestNeighbour(p2);
            while(nearest!=p1)
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
