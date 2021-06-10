using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoadRepair;

namespace RoadRepairTests
{
    [TestClass]
    public class B_PlannerTests
    {
        [TestMethod]
        public void CalculateTime()
        {
            var planner = new Planner();
            planner.HoursOfWork = 5;
            planner.Workers = 2;
            var time = planner.GetTime();
            Assert.AreEqual(2.5, time);
        }

        [TestMethod]
        public void PlanRepairForRoadWithManyPotholes()
        {
            var road = new Road {Length = 10, Width = 5};
            road.Potholes = 15;

            var planner = new Planner();
            var repair = planner.SelectRepairType(road);
            Assert.IsTrue(repair is Resurfacing, "The repair should be a Resurface");
        }

        [TestMethod]
        public void PlanRepairForRoadWithFewPotholes()
        {
            var road = new Road { Length = 10, Width = 5 };
            road.Potholes = 2;

            var planner = new Planner();
            var repair = planner.SelectRepairType(road);
            Assert.IsTrue(repair is PatchingRepair, "The repair should be a Patch");
        }
    }
}
