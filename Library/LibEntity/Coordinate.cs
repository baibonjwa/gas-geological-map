using System;
using Castle.ActiveRecord;

namespace LibEntity
{
    /// <summary>
    ///     Immutable Class
    /// </summary>
    public class Coordinate
    {
        public Coordinate(double xx, double yy, double zz)
        {
            x = xx;
            y = yy;
            z = zz;
        }

        /// <summary>
        ///     坐标X
        /// </summary>
        [Property("COORDINATE_X")]
        public double x { get; private set; }

        /// <summary>
        ///     坐标X
        /// </summary>
        [Property("COORDINATE_Y")]
        public double y { get; private set; }

        /// <summary>
        ///     坐标X
        /// </summary>
        [Property("COORDINATE_Y")]
        public double z { get; private set; }
    }
}