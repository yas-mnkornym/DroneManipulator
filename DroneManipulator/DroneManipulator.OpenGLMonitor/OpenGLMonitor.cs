using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneManipulator.OpenGLMonitor
{
	public class OpenGLMonitor : IDronesMonitor
	{
		Views.MonitorControl monitorControl_ = null;
		ViewModels.MonitorControlViewModel viewModel_ = null;

		public OpenGLMonitor()
		{
			monitorControl_ = new Views.MonitorControl();
			viewModel_ = new ViewModels.MonitorControlViewModel(
				null,
				monitorControl_.Dispatcher);
			monitorControl_.DataContext = viewModel_;
		}

		#region IDronesMonitor メンバー

		public object MonitorControl
		{
			get
			{
				return monitorControl_;
			}
		}

		public object ManipulatorControl
		{
			get
			{
				return null;
			}
		}

		public void Update(DronePosture[] drones)
		{
			viewModel_.Update(drones);
		}

		#endregion
	}
}
