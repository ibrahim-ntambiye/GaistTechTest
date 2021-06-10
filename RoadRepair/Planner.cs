using System;
using System.Collections;
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
           
            var potholeDensity =   road.Potholes/(road.Length * road.Width);
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
            double repairVolume=0;
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
           var totalVolume = GetVolume(roads);
            double availableSpace;
            if (GetVolume(roads) > availableMaterial)
            {
                var roadList = roads;

                var remainder =  GetVolume(roads) % availableMaterial;

                return roadList;
            }
            else
            {
                return roads;
            }
         

        }
    }

}
