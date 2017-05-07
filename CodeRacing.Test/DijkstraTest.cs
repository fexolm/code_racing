using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeRacing;
using NUnit.Framework;

namespace CodeRacing.Test
{
    class DijkstraTest
    {
        [Test]
        public void Test1()
        {
            WaypointNode[] waypoints = new WaypointNode[13];
            for (int i = 0; i < 13; i++)
            {
                waypoints[i] = new WaypointNode();
            }
            waypoints[0].Weight = 0;
            waypoints[0].Ways.Add((1,waypoints[10]));
            waypoints[0].Ways.Add((1,waypoints[3]));
            waypoints[1].Ways.Add((1,waypoints[7]));
            waypoints[1].Ways.Add((1,waypoints[6]));
            waypoints[2].Ways.Add((1,waypoints[3]));
            waypoints[2].Ways.Add((1,waypoints[5]));
            waypoints[3].Ways.Add((1,waypoints[4]));
            waypoints[3].Ways.Add((1,waypoints[2]));
            waypoints[3].Ways.Add((1,waypoints[0]));
            waypoints[4].Ways.Add((1,waypoints[3]));
            waypoints[5].Ways.Add((1,waypoints[2]));
            waypoints[5].Ways.Add((1,waypoints[7]));
            waypoints[6].Ways.Add((1,waypoints[1]));
            waypoints[6].Ways.Add((1,waypoints[10]));
            waypoints[7].Ways.Add((1,waypoints[1]));
            waypoints[7].Ways.Add((1,waypoints[9]));
            waypoints[7].Ways.Add((1,waypoints[5]));
            waypoints[8].Ways.Add((1,waypoints[9]));
            waypoints[8].Ways.Add((1,waypoints[12]));
            waypoints[9].Ways.Add((1,waypoints[7]));
            waypoints[9].Ways.Add((1,waypoints[8]));
            waypoints[10].Ways.Add((1,waypoints[0]));
            waypoints[10].Ways.Add((1,waypoints[12]));
            waypoints[10].Ways.Add((1,waypoints[6]));
            waypoints[12].Ways.Add((1,waypoints[10]));
            waypoints[12].Ways.Add((1,waypoints[8]));

            var wp = DijkstraAlgorithm.FindWay(waypoints[0], waypoints[7]);
            var res = new List<int>();
            var realResult = new int[] { 0, 10, 6, 1, 7 };
            foreach (var element in wp)
            {
                for (int i = 0; i < 13; i++)
                {
                    if (waypoints[i] == element)
                        res.Add(i);
                }
            }
            for (int i = 0; i < realResult.Length; i++)
            {
                Assert.AreEqual(realResult[i], res[i]);
            }
        }
    }
}
