using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoadRepair;
using System.Collections.Generic;

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
            var road = new Road { Length = 10, Width = 5 };
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



        [TestMethod]
        public void GetRoadRepairInformation()
        {
            //Arrange 
            var road = new Road { Length = 10, Width = 5, Potholes = 15 };

            List<RoadRepairInformation> roadRepairInformation = new List<RoadRepairInformation>();


            //Act 
            var planner = new Planner();
            var result = planner.GetRoadRepairInformation(road, ref roadRepairInformation);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result[0].Volume, 5);
        }

        [TestMethod]
        public void OptimalRoadsToRepair()
        {
            //Arrange 
            var planner = new Planner();

            var road = new Road()
            {
                Length = 70,
                Width = 80,
                Potholes = 20
            };

            List<RoadRepairInformation> roadRepair = new List<RoadRepairInformation>()
            {
                new RoadRepairInformation
                {
                     Road = road,
                     Volume = planner.GetVolume(new List<Road>{ road }  )
                },
                new RoadRepairInformation
                {
                    Road = road,
                     Volume = planner.GetVolume(new List<Road>{ road }  )
                }

            };
            //Act
            var result = planner.OptimalRoadsToRepair(3, roadRepair);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.SelectedRoads.Count);
            Assert.AreEqual(1, result.AavailableMaterial);

        }

    }
}
