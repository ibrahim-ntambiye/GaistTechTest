using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoadRepair;

namespace RoadRepairTests
{
    [TestClass]
    public class C_MultipleRepairTests
    {
        [TestMethod]
        public void ThreeRepairs()
        {
            var plan = new Planner();

            var road1 = new Road() { Length = 3, Width = 2, Potholes = 5 };
            var road2 = new Road() { Length = 4, Width = 1.5, Potholes = 3};
            var road3 = new Road() { Length = 6, Width = 2, Potholes = 1};

            var roads = new List<Road>(){ road1, road2, road3 };

            // TODO make the planner calculate the total volume of all the repairs for the list of roads.

            var totalVolume = plan.GetVolume(roads);
            Assert.AreEqual(1.3, totalVolume, 0.00001);
        }

        [TestMethod]
        public void LimitedMaterial()
        {
            var plan = new Planner();

            var road1 = new Road() { Length = 6, Width = 2, Potholes = 1 };
            var road2 = new Road() { Length = 3, Width = 2, Potholes = 5 };
            var road3 = new Road() { Length = 4, Width = 1.5, Potholes = 3 };
            
            var roads = new List<Road>() { road1, road2, road3 };

            // TODO make the planner work out which roads to repair, if there is not enough material available to repair all of them.
            // Try to make the planner select the roads that will fix the highest number of potholes.

            // So for this test, the total volume of the repairs to the selected roads must not exceed the available material.
            // Note that the available material (1.25) is less than the total volume (1.3) needed to fix all 3 roads in the previous test.
            var availableMaterial = 1.25;

            var roadsRepaired = plan.SelectRoadsToRepair(roads, availableMaterial);

            var potholesRepaired = 0;
            foreach (var road in roadsRepaired)
            {
                potholesRepaired += road.Potholes;
            }

            Assert.IsTrue(potholesRepaired >= 8);

            var materialUsed = plan.GetVolume(roadsRepaired);
            Assert.IsTrue(materialUsed <= availableMaterial);
        }
    }
}
