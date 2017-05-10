using System;
using Com.CodeGame.CodeRacing2015.DevKit.CSharpCgdk.Model;
using System.Collections.Generic;
using Com.CodeGame.CodeWizards2016.DevKit.CSharpCgdk;
using CodeRacing;

namespace Com.CodeGame.CodeRacing2015.DevKit.CSharpCgdk
{
    public sealed class MyStrategy : IStrategy
    {
        static List<(double, double)> _carMoves = new List<(double, double)>();
        public void Move(Car car, World world, Game game, Move move)
        {
            Visualizer.Client.BeginPost();
            MapParser.InitMap(world, game);
            var start = MapParser.GetWaypoint((int)(car.X / game.TrackTileSize), (int)(car.Y / game.TrackTileSize));
            var finish = MapParser.GetWaypoint(car.NextWaypointX, car.NextWaypointY);
            var way = DijkstraAlgorithm.FindWay(start, finish);

            foreach (var wp in way)
            {
                Visualizer.Client.FillCircle((wp.X + 0.5) * 800, (wp.Y + 0.5) * 800, 10);
            }

            double nextWaypointX = (way[1].X + 0.5D) * game.TrackTileSize;
            double nextWaypointY = (way[1].Y + 0.5D) * game.TrackTileSize;

            double angleToWaypoint = car.GetAngleTo(nextWaypointX, nextWaypointY);
            double speedModule = Math.Sqrt(car.SpeedX * car.SpeedX + car.SpeedY * car.SpeedY);

            move.EnginePower = 0;

            //if (speedModule * speedModule * Math.Abs(angleToWaypoint) > 2.5D * 2.5D * Math.PI)
            //{
            //    move.IsBrake = true;
            //}
            if (world.Tick >= game.InitialFreezeDurationTicks)
            {
                move.EnginePower = 0.4D;
                move.WheelTurn = angleToWaypoint * 32.0D / Math.PI;
                PhysicsCar pcar = new PhysicsCar();
                pcar.WheelTurn = 0;
                pcar.EnginePower = 0;
                pcar.Speed = new Vector2(0, 0);
                pcar.Angle = -Math.PI / 2;
                pcar.Position = new Vector2(166, 2800);
                _carMoves.Add((move.WheelTurn, move.EnginePower));
                var simulation = Physics.CalcStateRecursive(game, pcar, world.Tick - game.InitialFreezeDurationTicks + 1, _carMoves.ToArray());
                var simulationTick = simulation[world.Tick - game.InitialFreezeDurationTicks];
                //Console.WriteLine("{0:0:00}= {1:0:00}         {2:0.00}={3:0.00}      {4:0.00}={5:0.00}, {6:0.00}={7:0.00}",
                //    car.WheelTurn, simulationTick.WheelTurn,
                //    car.SpeedX, simulationTick.Speed.X,
                //    car.SpeedY, simulationTick.Speed.Y,
                //    car.Angle, simulationTick.Angle);
                Console.WriteLine("tick {4:0000}:    {0:0.00}:{1:0.00}    {2:0.00}:{3:0.00}", car.X, car.Y, simulationTick.Position.X, simulationTick.Position.Y, world.Tick - game.InitialFreezeDurationTicks);
            }

            Visualizer.Client.EndPost();
        }
    }
}