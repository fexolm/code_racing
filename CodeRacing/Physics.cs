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
        public double AngleSpeed;
        public double Angle;
        public Vector2 Speed;
        public double WheelTurn;
        public double EnginePower;
        public Vector2 Position;


    }
    static class Physics
    {
        private static double dt = 0.1D;
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
            var mass = game.BuggyMass;
            var forwardPower = game.BuggyEngineForwardPower;
            var backwardPower = game.BuggyEngineRearPower;
            enginePower = Calculation.LimitChange(enginePower, 1);
            wheelTurn = Calculation.LimitChange(wheelTurn, 1);

            var wturn = currentState.WheelTurn;
            var epower = currentState.EnginePower;
            var pos = currentState.Position;
            var speed = currentState.Speed;
            var angle = currentState.Angle;
            var angleSpeed = currentState.AngleSpeed;

            var direction = Vector2.sincos(angle).Normalize();

            double baseAngleSpeed = wturn * game.CarAngularSpeedFactor * speed.Dot(direction);
            angleSpeed -= baseAngleSpeed;

            wturn += Calculation.LimitChange(wheelTurn - wturn, game.CarWheelTurnChangePerTick);
            epower += Calculation.LimitChange(enginePower - epower, game.CarEnginePowerChangePerTick);
            var acceleration = forwardPower / mass * epower;
            Vector2 accel = direction * acceleration;

            baseAngleSpeed = wturn * game.CarAngularSpeedFactor * speed.Dot(direction);
            angleSpeed += baseAngleSpeed;

            for (int i = 0; i < 10; i++)
            {
                pos = pos + speed * dt;
                speed = (speed + accel*dt) * Math.Pow(1 - game.CarMovementAirFrictionFactor, dt);
                double lengthSpeed = speed.Dot(direction);
                double crossSpeed = speed.Cross(direction);
                Vector2 lengthFriction = direction * Calculation.Limit(lengthSpeed, game.CarLengthwiseMovementFrictionFactor * dt);
                Vector2 crossFriction = direction.PerpendicularLeft() * Calculation.Limit(crossSpeed, game.CarCrosswiseMovementFrictionFactor * dt);
                speed -= lengthFriction + crossFriction;
                angle += angleSpeed * dt;
                angle = Calculation.NormalizeAngle(angle);

                direction = Vector2.sincos(angle);

                angleSpeed = baseAngleSpeed + (angleSpeed - baseAngleSpeed) * Math.Pow(1 - game.CarRotationAirFrictionFactor, dt);
                angleSpeed -= Calculation.Limit(angleSpeed - baseAngleSpeed, game.CarRotationFrictionFactor * dt);
            }

            result.Angle = angle;
            result.AngleSpeed = angleSpeed;
            result.EnginePower = epower;
            result.WheelTurn = wturn;
            result.Speed = speed;
            result.Position = pos;

            return result;
        }
    }
}
