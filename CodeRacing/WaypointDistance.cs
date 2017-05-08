//#define DEBUG_GRAPH
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
        public WaypointNode(int x, int y) : this()
        {
            X = x;
            Y = y;
        }
        public WaypointNode()
        {
            Weight = 9999999;
            Ways = new List<(double, WaypointNode)>();
        }
        public double Weight { get; set; }
        public List<(double weight, WaypointNode node)> Ways { get; set; }
        public readonly int X;
        public readonly int Y;
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
            p1.Weight = 0;
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
                _map.Add((x, y), new WaypointNode(x, y));
            }
            return _map[(x, y)];
        }
        public static WaypointNode GetWaypoint(int x, int y)
        {
            return _map[(x, y)];
        }
        public static void InitMap(World world, Game game)
        {
            var map = world.TilesXY;
            int xlen = map.Length;
            int ylen = map[0].Length;
            if (_map == null)
            {
                _map = new Dictionary<(int x, int y), WaypointNode>();
                for (int i = 0; i < xlen; i++)
                {
                    for (int j = 0; j < ylen; j++)
                    {
                        switch (map[i][j])
                        {
                            case TileType.LeftTopCorner:
                                GetNode(i, j).Ways.Add((1, GetNode(i, j + 1)));
                                GetNode(i, j).Ways.Add((1, GetNode(i + 1, j)));
                                break;
                            case TileType.RightTopCorner:
                                GetNode(i, j).Ways.Add((1, GetNode(i, j + 1)));
                                GetNode(i, j).Ways.Add((1, GetNode(i - 1, j)));
                                break;
                            case TileType.LeftBottomCorner:
                                GetNode(i, j).Ways.Add((1, GetNode(i, j - 1)));
                                GetNode(i, j).Ways.Add((1, GetNode(i + 1, j)));
                                break;
                            case TileType.RightBottomCorner:
                                GetNode(i, j).Ways.Add((1, GetNode(i, j - 1)));
                                GetNode(i, j).Ways.Add((1, GetNode(i - 1, j)));
                                break;
                            case TileType.BottomHeadedT:
                                GetNode(i, j).Ways.Add((1, GetNode(i, j + 1)));
                                GetNode(i, j).Ways.Add((1, GetNode(i + 1, j)));
                                GetNode(i, j).Ways.Add((1, GetNode(i - 1, j)));
                                break;
                            case TileType.TopHeadedT:
                                GetNode(i, j).Ways.Add((1, GetNode(i, j - 1)));
                                GetNode(i, j).Ways.Add((1, GetNode(i + 1, j)));
                                GetNode(i, j).Ways.Add((1, GetNode(i - 1, j)));
                                break;
                            case TileType.Crossroads:
                                GetNode(i, j).Ways.Add((1, GetNode(i, j + 1)));
                                GetNode(i, j).Ways.Add((1, GetNode(i + 1, j)));
                                GetNode(i, j).Ways.Add((1, GetNode(i - 1, j)));
                                GetNode(i, j).Ways.Add((1, GetNode(i, j - 1)));
                                break;
                            case TileType.Horizontal:
                                GetNode(i, j).Ways.Add((1, GetNode(i + 1, j)));
                                GetNode(i, j).Ways.Add((1, GetNode(i - 1, j)));
                                break;
                            case TileType.Vertical:
                                GetNode(i, j).Ways.Add((1, GetNode(i, j + 1)));
                                GetNode(i, j).Ways.Add((1, GetNode(i, j - 1)));
                                break;
                            case TileType.RightHeadedT:
                                GetNode(i, j).Ways.Add((1, GetNode(i, j + 1)));
                                GetNode(i, j).Ways.Add((1, GetNode(i, j - 1)));
                                GetNode(i, j).Ways.Add((1, GetNode(i + 1, j)));
                                break;
                            case TileType.LeftHeadedT:
                                GetNode(i, j).Ways.Add((1, GetNode(i, j + 1)));
                                GetNode(i, j).Ways.Add((1, GetNode(i, j - 1)));
                                GetNode(i, j).Ways.Add((1, GetNode(i - 1, j)));
                                break;

                        }
                    }
                }
            }
            else
            {
                foreach (var element in _map)
                {
                    element.Value.Weight = 9999999;
                }
            }
#if DEBUG_GRAPH
            Random rnd = new Random();
            foreach (var element in _map)
            {
                var x = element.Key.x * 800 + 400;
                var y = element.Key.y * 800 + 400;
                float red = (float)rnd.NextDouble();
                float green = (float)rnd.NextDouble();
                float blue = (float)rnd.NextDouble();

                Visualizer.Client.FillCircle(x, y, 20, red, green, blue);
                foreach (var neighbour in element.Value.Ways)
                {
                    Visualizer.Client.Line(x + 25 * (red + green + blue), y + 25 * (red + green + blue), neighbour.node.X * 800 + 400, neighbour.node.Y * 800 + 400, red, green, blue);
                }
            }
            for (int i = 0; i < xlen; i++)
            {
                for (int j = 0; j < ylen; j++)
                {
                    Visualizer.Client.Text(i * game.TrackTileSize + 400, j * game.TrackTileSize + 400, map[i][j].ToString());
                }
            }
#endif
        }
    }
}
