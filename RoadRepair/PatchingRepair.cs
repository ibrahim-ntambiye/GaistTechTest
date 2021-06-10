namespace RoadRepair
{
    /// <summary>
    /// A patch is where you fix just the part of the road that is damaged.
    /// </summary>
    public class PatchingRepair
    {
        public PatchingRepair(Road road)
        {
            Patches = road.Potholes;
            Depth = 0.1;
        }

        public int Patches { get; }

        public double Depth { get; }

        public double GetVolume()
        {
            // Assume that every patch has a 1 x 1 area
            var volume = Patches * Depth;
            return volume;
        }

    }
}
