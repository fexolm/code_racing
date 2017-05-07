using Com.CodeGame.CodeRacing2015.DevKit.CSharpCgdk.Model;
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

    public static class DijkstraAlgorithm
    {
        private static void CalcWeights(WaypointNode p1, WaypointNode p2)
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
        private static WaypointNode GetNearestNeighbour(WaypointNode p)
        {
            double min = p.Ways.Min(w => w.node.Weight);
            return p.Ways.FirstOrDefault(w => w.node.Weight == min).node;
        }
        public static List<WaypointNode> FindWay(WaypointNode p1, WaypointNode p2)
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

    public static class MapParser
    {
        private static IDictionary<(int x, int y), WaypointNode> _map;
        private static WaypointNode GetNode(int x, int y)
        {
            if (!_map.ContainsKey((x, y)))
            {
                _map.Add((x, y), new WaypointNode());
            }
            return _map[(x, y)];
        }
        public static List<WaypointNode[]> GetWaypints(World world)
        {
            _map = new Dictionary<(int x, int y), WaypointNode>();
            var map = world.TilesXY;
            int xlen = map.Length;
            int ylen = map[0].Length;

            for (int i = 0; i < xlen; i++)
            {
                for (int j = 0; j < ylen; j++)
                {
                    switch (map[i][j])
                    {
                        case TileType.LeftTopCorner:
                            GetNode(i, j).Ways.Add((1, GetNode(i, j - 1)));
                            GetNode(i, j).Ways.Add((1, GetNode(i + 1, j)));
                            break;
                        case TileType.RightTopCorner:
                            GetNode(i, j).Ways.Add((1, GetNode(i, j - 1)));
                            GetNode(i, j).Ways.Add((1, GetNode(i - 1, j)));
                            break;
                        case TileType.LeftBottomCorner:
                            GetNode(i, j).Ways.Add((1, GetNode(i, j + 1)));
                            GetNode(i, j).Ways.Add((1, GetNode(i - 1, j)));
                            break;
                        case TileType.RightBottomCorner:
                            GetNode(i, j).Ways.Add((1, GetNode(i, j + 1)));
                            GetNode(i, j).Ways.Add((1, GetNode(i + 1, j)));
                            break;
                        case TileType.BottomHeadedT:
                            GetNode(i, j).Ways.Add((1, GetNode(i, j + 1)));
                            GetNode(i, j).Ways.Add((1, GetNode(i + 1, j)));
                            break;
                    }
                }
            }

            return null;
        }
    }
}
