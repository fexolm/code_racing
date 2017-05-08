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
            MapParser.GetWaypints(world, game);
            Visualizer.Client.EndPost();
        }
    }
}