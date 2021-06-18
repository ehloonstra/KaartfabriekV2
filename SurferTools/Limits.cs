namespace SurferTools
{
    /// <summary>
    /// To hold the layer limits
    /// </summary>
    /// <remarks>Can this me modified to a struct or record?</remarks>
    public class Limits
    {
        /// <summary>
        /// The minimum map limit in the X direction
        /// </summary>
        public double Xmin { get; set; }

        /// <summary>
        /// The minimum map limit in the Y direction
        /// </summary>
        public double Ymin { get; set; }
        
        /// <summary>
        /// The maximum map limit in the X direction
        /// </summary>
        public double Xmax { get; set; }

        /// <summary>
        /// The maximum map limit in the Y direction
        /// </summary>
        public double Ymax { get; set; }

        /// <summary>
        /// Set the limits
        /// </summary>
        /// <param name="xMin"></param>
        /// <param name="xMax"></param>
        /// <param name="yMin"></param>
        /// <param name="yMax"></param>
        public Limits(double xMin, double xMax, double yMin, double yMax)
        {
            Xmin = xMin;
            Ymin = yMin;
            Xmax = xMax;
            Ymax = yMax;
        }
    }
}
