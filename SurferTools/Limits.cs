using System;

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

        /// <summary>
        /// Empty constructor
        /// </summary>
        public Limits()
        {

        }

        /// <summary>
        /// Round up to the round up value
        /// </summary>
        /// <param name="limits"></param>
        /// <param name="roundUpValue"></param>
        /// <returns>New limits</returns>
        public static Limits RoundUp(Limits limits, int roundUpValue)
        {
            return new()
            {
                Xmin = Math.Round(limits.Xmin / roundUpValue, MidpointRounding.ToZero) * roundUpValue,
                Ymin = Math.Round(limits.Ymin / roundUpValue, MidpointRounding.ToZero) * roundUpValue,
                Xmax = (Math.Round(limits.Xmax / roundUpValue, MidpointRounding.ToZero) * roundUpValue) + roundUpValue,
                Ymax = (Math.Round(limits.Ymax / roundUpValue, MidpointRounding.ToZero) * roundUpValue) + roundUpValue
            };
        }

        #region Overrides of Object

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"Xmin: {Xmin}; Xmax: {Xmax}; Ymin: {Ymin}; Ymax: {Ymax}; ";
        }

        #endregion
    }
}
