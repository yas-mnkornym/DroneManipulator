using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneManipulator.Simulator.ViewModels
{
	internal class MonitorContainerViewModel : BaseNotifyPropertyChanged
	{
		#region Bindings
		#region ManipulatorControl
		object manipulatorControl_ = null;
		public object ManipulatorControl
		{
			get
			{
				return manipulatorControl_;
			}
			set
			{
				SetValue(ref manipulatorControl_, value);
			}
		}
		#endregion

		#region MonitorControl
		object monitorControl_ = null;
		public object MonitorControl
		{
			get
			{
				return monitorControl_;
			}
			set
			{
				SetValue(ref monitorControl_, value);
			}
		}
		#endregion
		#endregion // Bindings
	}
}
