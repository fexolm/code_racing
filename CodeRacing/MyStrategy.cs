using System;
using Com.CodeGame.CodeRacing2015.DevKit.CSharpCgdk.Model;
using System.Collections.Generic;
using Com.CodeGame.CodeWizards2016.DevKit.CSharpCgdk;

namespace Com.CodeGame.CodeRacing2015.DevKit.CSharpCgdk
{
    public sealed class MyStrategy : IStrategy
    {
        private VisualClient vc = new VisualClient("127.0.0.1", 13579);
        public void Move(Car car, World world, Game game, Move move)
        {
            vc.BeginPost();
            vc.FillCircle(car.X, car.Y, 100);
            vc.EndPost();
        }
    }
}