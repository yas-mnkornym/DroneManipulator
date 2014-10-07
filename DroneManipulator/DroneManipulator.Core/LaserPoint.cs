using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneManipulator
{
	/// <summary>
	/// Represents a point which is measured by Smart Laser Scanner.
	/// </summary>
	public class LaserPoint
	{
		/// <summary>
		/// Gets or Sets the X-coordinate of the point in degrees.
		/// </summary>
		public double XDegrees { get; set; }

		/// <summary>
		/// Gets or Sets the Y-coordinate of the point in degrees.
		/// </summary>
		public double YDegress { get; set; }

		/// <summary>
		/// Gets or Sets the value.
		/// </summary>
		public double Value { get; set; }

		/// <summary>
		/// Gets or Sets the measured time (nanoseconds).
		/// </summary>
		public long Time { get; set; }
	}
}
