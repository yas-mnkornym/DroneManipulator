using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneManipulator
{
	/// <summary>
	/// Represents a posture of the drone.
	/// </summary>
	public class DronePosture
	{
		/// <summary>
		/// Gets or Sets the X coordinate (of the camera's coordinate system)
		/// </summary>
		public double X { get; set; }

		/// <summary>
		/// Gets or Sets the Y coordinate (of the camera's coordinate system)
		/// </summary>
		public double Y { get; set; }

		/// <summary>
		/// Gets or Sets the Z coordinate (of the camera's coordinate system)
		/// </summary>
		public double Z { get; set; }

		/// <summary>
		/// Gets or Sets the rotation around X axis (of the object's coordinate system)
		/// </summary>
		public double RotationX { get; set; }
		/// <summary>
		/// Gets or Sets the rotation around Y axis (of the object's coordinate system)
		/// </summary>
		public double RotationY { get; set; }

		/// <summary>
		/// Gets or Sets the rotation around Z axis (of the object's coordinate system)
		/// </summary>
		public double RotationZ { get; set; }
	}
}
