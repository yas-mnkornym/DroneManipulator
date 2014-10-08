using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneManipulator
{
	public interface IDronesMonitor
	{
		/// <summary>
		/// Gets the monitor control as a WPF control.
		/// </summary>
		object Control { get; }

		/// <summary>
		/// Gets the control for manipulation monitor as a WPF control.
		/// </summary>
		object ManipulatorControl { get; }

		/// <summary>
		/// Updates drones posture.
		/// </summary>
		/// <param name="drones"></param>
		void Update(DronePosture[] drones);
	}
}
