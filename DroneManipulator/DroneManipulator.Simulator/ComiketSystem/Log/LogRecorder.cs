using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComiketSystem.Cheekichi.Infrastructure.Log
{
	internal abstract class LogRecorderBase : IDisposable
	{
		Logger logger_ = null;

		/// <summary>
		/// ロガーを取得する。
		/// </summary>
		public Logger Logger
		{
			get
			{
				return logger_;
			}
			private set
			{
				if (logger_ != null) {
					logger_.Logged -= OnLogged;
				}
				logger_ = value;
				if (logger_ != null) {
					logger_.Logged += OnLogged;
				}
			}
		}

		#region Constructors
		protected LogRecorderBase(Logger logger)
		{
			if (logger == null) { throw new ArgumentNullException("logger"); }
			Logger = logger;
		}
		#endregion

		/// <summary>
		/// ログが完了したときに呼び出される。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected abstract void OnLogged(object sender, LoggedEventArgs e);


		#region IDisposable
		bool isDisposable__ = false;
		virtual protected void Dispose(bool disposing)
		{
			if (isDisposable__) { return; }
			if (disposing) {
				Logger = null;
			}
			isDisposable__ = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion
	}
}