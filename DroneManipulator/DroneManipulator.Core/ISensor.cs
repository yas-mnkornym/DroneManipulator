using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneManipulator
{
	/// <summary>
	/// Measures and obtains points by the sensor
	/// </summary>
	public interface ISensor
	{
		/// <summary>
		/// Gets points array of the next frame.
		/// </summary>
		/// <remarks>This method blocks current thread until next points comes.</remarks>
		/// <returns>points array</returns>
		LaserPoint[] GetPoints();
	}
}
