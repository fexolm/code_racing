using Com.CodeGame.CodeRacing2015.DevKit.CSharpCgdk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeRacing
{
    class PhysicsCar
    {
        public double Mass;
        public double ForwardPower;
        public double BackwardPower;
        public double AngleSpeed;
        public double Angle;
        public Vector2 Speed;
        public Vector2 Direction;
        public double WheelTurn;
        public double EnginePower;
        public Vector2 Position;
        public double Acceleration
        {
            get
            {
                return ForwardPower / Mass * EnginePower;
            }
        }


    }
    static class Physics
    {
        public static List<PhysicsCar> CalcStateRecursive(Game game, PhysicsCar currentState, int tickNumber, (double wheelTurn, double enginePower)[] actions)
        {
            List<PhysicsCar> result = new List<PhysicsCar>();
            PhysicsCar newState = currentState;
            result.Add(newState);
            for (int i = 0; i < tickNumber; i++)
            {
                newState = GetNextTickState(game, newState, actions[i].wheelTurn, actions[i].enginePower);
                result.Add(newState);
            }
            return result;
        }
        public static PhysicsCar GetNextTickState(Game game, PhysicsCar currentState, double wheelTurn, double enginePower)
        {
            PhysicsCar result = new PhysicsCar();
            result.Mass = game.BuggyMass;
            result.ForwardPower = game.BuggyEngineForwardPower;
            result.BackwardPower = game.BuggyEngineRearPower;
            result.Direction = Vector2.sincos(result.Angle).Normalize();


            result.WheelTurn = currentState.WheelTurn + Calculation.Limit(wheelTurn - currentState.WheelTurn, game.CarWheelTurnChangePerTick);

            result.EnginePower = currentState.EnginePower + Calculation.Limit(enginePower - currentState.EnginePower, game.CarEnginePowerChangePerTick);

            Vector2 accel = result.Direction * result.Acceleration;
            //result.Position = currentState.Position + currentState.Speed;
            result.Speed = (currentState.Speed + accel) * (1 - game.CarMovementAirFrictionFactor);

            double lengthSpeed = result.Speed.Dot(result.Direction);
            double crossSpeed = result.Direction.Cross(result.Speed);

            Vector2 lengthFriction = result.Direction * Calculation.Limit(lengthSpeed, game.CarLengthwiseMovementFrictionFactor);
            Vector2 crossFriction = result.Direction.PerpendicularLeft() * Calculation.Limit(crossSpeed, game.CarCrosswiseMovementFrictionFactor);

            result.Speed -= lengthFriction;
            result.Speed -= crossFriction;

            result.AngleSpeed += result.WheelTurn * game.CarAngularSpeedFactor * result.Speed.Dot(result.Direction) - game.CarRotationAirFrictionFactor;
            return result;
        }
    }
}
