using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeRacing
{
    class TestMain
    {
        static void Main()
        {
            WaypointNode[] waypoints = new WaypointNode[13];
            for(int i = 0; i< 13; i++)
            {
                waypoints[i] = new WaypointNode();
            }
            waypoints[0].Weight = 0;
            waypoints[0].DRight = waypoints[10];
            waypoints[0].URight = waypoints[3];
            waypoints[1].DRight = waypoints[7];
            waypoints[1].DLeft = waypoints[6];
            waypoints[2].DRight = waypoints[3];
            waypoints[2].URight = waypoints[5];
            waypoints[3].URight = waypoints[4];
            waypoints[3].ULeft = waypoints[2];
            waypoints[3].DLeft = waypoints[0];
            waypoints[4].DLeft = waypoints[3];
            waypoints[5].DLeft = waypoints[2];
            waypoints[5].DRight = waypoints[7];
            waypoints[6].URight = waypoints[1];
            waypoints[6].DLeft = waypoints[10];
            waypoints[7].DLeft = waypoints[1];
            waypoints[7].Down = waypoints[9];
            waypoints[7].ULeft = waypoints[5];
            waypoints[8].URight = waypoints[9];
            waypoints[8].DLeft = waypoints[12];
            waypoints[9].Up = waypoints[7];
            waypoints[9].ULeft = waypoints[8];
            waypoints[10].ULeft = waypoints[0];
            waypoints[10].DRight = waypoints[12];
            waypoints[10].URight = waypoints[6];
            waypoints[12].ULeft = waypoints[10];
            waypoints[12].URight = waypoints[8];

            Console.WriteLine(new WaypointDistance().FindWay(waypoints[0], waypoints[7]).Count());
            Console.ReadKey();
        }
    }
}
