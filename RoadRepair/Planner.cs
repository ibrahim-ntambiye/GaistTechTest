using System.Collections.Generic;
using System.Linq;

namespace RoadRepair
{
   
    public class Planner
    {
        /// <summary>
        /// The total number of hours needed to complete the work.
        /// </summary>
        public double HoursOfWork { get; set; }

        /// <summary>
        /// The number of people available to do the work
        /// </summary>
        public int Workers { get; set; }

        /// <summary>
        /// The time to complete the work, using all the workers.
        /// </summary>
        /// <returns>The number of hours to complete the work.</returns>
        public double GetTime()
        {
            var time = HoursOfWork / Workers;
            return time;
        }

        /// <summary>
        /// Return the correct type of repair (either a patch or a resurface) depending on
        /// the density of potholes.
        /// </summary>
        /// <param name="road">A road needing repair</param>
        /// <returns>Either a PatchingRepair or a Resurfacing</returns>
        public object SelectRepairType(Road road)
        {
            // Use the road.Width, road.Length and road.Potholes properties to calculate the density of potholes.

            // If the density of potholes is more than 20% the road should be resurfaced.
            // Otherwise it should be patched.

            var potholeDensity = road.Potholes / (road.Length * road.Width);
            if (potholeDensity > 0.2)
            {
                var resurface = new Resurfacing(road);

                return resurface;
            }
            var patching = new PatchingRepair(road);

            return patching;
        }

        /// <summary>
        /// Calculate the total volume of all the repairs for a list of roads that need repairs.
        /// </summary>
        /// <param name="roads">A list of roads needing repairs</param>
        /// <returns>The total volume of all the repairs</returns>
        public double GetVolume(List<Road> roads)
        {
            double repairVolume = 0;
            //Check the type of repair needed
            foreach (var r in roads)
            {
                var repairType = SelectRepairType(r);
                if (repairType.GetType() == typeof(PatchingRepair))
                {
                    var patching = new PatchingRepair(r);
                    repairVolume += patching.GetVolume();
                }
                else
                {
                    var resurface = new Resurfacing(r);
                    repairVolume += resurface.GetVolume();
                }
            }

            return repairVolume;
        }

        public List<Road> SelectRoadsToRepair(List<Road> roads, double availableMaterial)
        {
            List<RoadRepairInformation> volumeValues = new List<RoadRepairInformation>();

            foreach (var road in roads)
            {
                GetRoadRepairInformation(road, ref volumeValues);
            }

            var OrderedByDescending = volumeValues.OrderByDescending(value => value.Volume).ToList();
            var OrderedByAscending = volumeValues.OrderBy(value => value.Volume).ToList();

            var OptimalRoadsToRepairAscending = OptimalRoadsToRepair(availableMaterial, OrderedByAscending);
            var OptimalRoadsToRepairDescending = OptimalRoadsToRepair(availableMaterial, OrderedByDescending);

            if (OptimalRoadsToRepairAscending.AavailableMaterial < OptimalRoadsToRepairDescending.AavailableMaterial)
            {
                return OptimalRoadsToRepairAscending.SelectedRoads;
            }

            return OptimalRoadsToRepairDescending.SelectedRoads;
        }

        public List<RoadRepairInformation> GetRoadRepairInformation(Road road, ref List<RoadRepairInformation> volumeValues)
        {
            var value = SelectRepairType(road);
            if (value.GetType() == typeof(PatchingRepair))
            {
                var newVal = (PatchingRepair)value;
                volumeValues.Add(new RoadRepairInformation
                {
                    Volume = newVal.GetVolume(),
                    Road = road
                });
            }
            else
            {
                var newVal = (Resurfacing)value;
                volumeValues.Add(new RoadRepairInformation
                {
                    Volume = newVal.GetVolume(),
                    Road = road
                });
            }

            return volumeValues;
        }

        public OptimalRoadsToRepair OptimalRoadsToRepair(double availableMaterial, List<RoadRepairInformation> roadRepair)
        {
            List<Road> roadsToRepair = new List<Road>();


            foreach (var road in roadRepair)
            {
                if (road.Volume <= availableMaterial)
                {
                    roadsToRepair.Add(road.Road);
                    availableMaterial -= road.Volume;
                }
            }

            return new OptimalRoadsToRepair
            {
                AavailableMaterial = availableMaterial,
                SelectedRoads = roadsToRepair
            };
        }
    }
}