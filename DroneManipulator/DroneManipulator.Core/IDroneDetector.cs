using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneManipulator
{
	/// <summary>
	/// Detects drones from points array.
	/// </summary>
	public interface IDroneDetector
	{
		DronePosture[] Detect(LaserPoint[] points);
	}
}
