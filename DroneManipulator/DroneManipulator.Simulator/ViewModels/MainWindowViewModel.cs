using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace DroneManipulator.Simulator.ViewModels
{
	internal class MainWindowViewModel : BaseNotifyPropertyChanged
	{
		public MainWindowViewModel(Dispatcher dispatcher = null)
			: base(dispatcher)
		{
			// 実際のモニタを作成
			var actualMonitor = new OpenGLMonitor.OpenGLMonitor();
			var actualMonitorVM = new MonitorContainerViewModel {
				MonitorControl = actualMonitor.MonitorControl,
				ManipulatorControl = actualMonitor.ManipulatorControl
			};
			ActualMonitorViewModel = actualMonitorVM;

			// 予想モニタを作成
			var expectedMonitor = new OpenGLMonitor.OpenGLMonitor();
			var expectedMonitorVM = new MonitorContainerViewModel {
				MonitorControl = expectedMonitor.MonitorControl,
				ManipulatorControl = expectedMonitor.ManipulatorControl
			};
			ExpectedMonitorViewModel = expectedMonitorVM;
		}

		#region Bindings
		#region ActualMonitorViewModel
		MonitorContainerViewModel actualMonitorViewModel_ = null;
		public MonitorContainerViewModel ActualMonitorViewModel
		{
			get
			{
				return actualMonitorViewModel_;
			}
			set
			{
				SetValue(ref actualMonitorViewModel_, value);
			}
		}
		#endregion

		#region ExpectedMonitorViewModel
		MonitorContainerViewModel expectedMonitorViewModel_ = null;
		public MonitorContainerViewModel ExpectedMonitorViewModel
		{
			get
			{
				return expectedMonitorViewModel_;
			}
			set
			{
				SetValue(ref expectedMonitorViewModel_, value);
			}
		}
		#endregion
		#endregion
	}
}
