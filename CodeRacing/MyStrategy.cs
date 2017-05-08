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
            double nextWaypointX = (car.NextWaypointX + 0.5D) * game.TrackTileSize;
            double nextWaypointY = (car.NextWaypointY + 0.5D) * game.TrackTileSize;

            double angleToWaypoint = car.GetAngleTo(nextWaypointX, nextWaypointY);
            double speedModule = Math.Sqrt(car.SpeedX * car.SpeedX + car.SpeedY * car.SpeedY);

            move.WheelTurn = angleToWaypoint * 32.0D / Math.PI;
            move.EnginePower = 0.75D;

            if (speedModule * speedModule * Math.Abs(angleToWaypoint) > 2.5D * 2.5D * Math.PI)
            {
                move.IsBrake = true;
            }
            Visualizer.Client.EndPost();
        }
    }
}