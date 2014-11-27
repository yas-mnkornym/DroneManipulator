using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using SharpGL;

using MathNet.Numerics.LinearAlgebra.Double;

namespace DroneManipulator.OpenGLMonitor.Views
{
	/// <summary>
	/// MonitorControl.xaml の相互作用ロジック
	/// </summary>
	public partial class MonitorControl : UserControl
	{
		public MonitorControl()
		{
			InitializeComponent();
		}
	}
}