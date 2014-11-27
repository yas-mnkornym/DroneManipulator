using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneManipulator.OpenGLMonitor.ViewModels
{
	internal class ManipulatorControlViewModel : BaseNotifyPropertyChanged
	{
		#region Bindings
		#region FOV
		double fov_ = 45.0;
		public double FOV
		{
			get
			{
				return fov_;
			}
			set
			{
				SetValue(ref fov_, value);
			}
		}
		#endregion

		#region 

		#endregion

		#endregion
	}
}
