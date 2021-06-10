namespace RoadRepair
{
    /// <summary>
    /// A resurface is where you fix the whole road, not just the parts that are damaged.
    /// </summary>
    public class Resurfacing
    {
        public double Width { get; }
        public double Length { get; }
        public double Depth { get; }
        public Resurfacing(Road road)
        {
            Width = road.Width;
            Length = road.Length;
            Depth = 0.1;
        }



        public double GetVolume()
        {
            var volume = Width * Length * Depth;
            return volume;
        }
    }
}