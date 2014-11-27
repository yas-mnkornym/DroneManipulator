using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DroneManipulator.Simulator
{
	/// <summary>
	/// App.xaml の相互作用ロジック
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			// create and show the main window.
			var window = new Views.MainWindow();
			var vm = new ViewModels.MainWindowViewModel(window.Dispatcher);
			window.DataContext = vm;
			MainWindow = window;
			MainWindow.Show();
		}
	}
}
