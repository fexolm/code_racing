using System;
using Com.CodeGame.CodeRacing2015.DevKit.CSharpCgdk.Model;
using System.Collections.Generic;
using Com.CodeGame.CodeWizards2016.DevKit.CSharpCgdk;
using CodeRacing;

namespace Com.CodeGame.CodeRacing2015.DevKit.CSharpCgdk
{
    public sealed class MyStrategy : IStrategy
    {
        public void Move(Car car, World world, Game game, Move move)
        {
            Visualizer.Client.BeginPost();
            MapParser.InitMap(world, game);
            var start = MapParser.GetWaypoint((int)(car.X / game.TrackTileSize), (int)(car.Y / game.TrackTileSize));
            var finish = MapParser.GetWaypoint(car.NextWaypointX, car.NextWaypointY);
            var way = DijkstraAlgorithm.FindWay(start, finish);

            foreach(var wp in way)
            {
                Visualizer.Client.FillCircle((wp.X + 0.5) * 800, (wp.Y + 0.5) * 800, 10);
            }

            double nextWaypointX = (way[1].X + 0.5D) * game.TrackTileSize;
            double nextWaypointY = (way[1].Y + 0.5D) * game.TrackTileSize;

            double angleToWaypoint = 2 * car.GetAngleTo(nextWaypointX, nextWaypointY);
            double speedModule = Math.Sqrt(car.SpeedX * car.SpeedX + car.SpeedY * car.SpeedY);

            move.WheelTurn = angleToWaypoint * 32.0D / Math.PI;
            move.EnginePower = 0.3D;

            if (speedModule * speedModule * Math.Abs(angleToWaypoint) > 2.5D * 2.5D * Math.PI)
            {
                move.IsBrake = true;
            }
            Visualizer.Client.EndPost();
        }
    }
}