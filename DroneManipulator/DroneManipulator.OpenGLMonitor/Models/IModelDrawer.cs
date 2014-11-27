using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneManipulator.OpenGLMonitor.Models
{
	public interface IDroneDrawer
	{
		void Draw(DronePosture drone);
	}
}
