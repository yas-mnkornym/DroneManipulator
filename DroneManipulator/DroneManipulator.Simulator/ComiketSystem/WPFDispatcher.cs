using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ComiketSystem.Cheekichi
{
	internal class WPFDispatcher : ICSActionDispatcher
	{
		Dispatcher dispatcher_ = null;

		public WPFDispatcher(Dispatcher dispatcher)
		{
			if (dispatcher == null) { throw new ArgumentNullException("dispatcher"); }
			dispatcher_ = dispatcher;
		}

		public void Dispatch(Action act)
		{
			Exception exception = null;
			dispatcher_.Invoke(() => {
				try {
					act();
				}
				catch (Exception ex) {
					exception = ex;
				}
			});
			if (exception != null) {
				throw exception;
			}
		}

		public T Dispatch<T>(Func<T> func)
		{
			Exception exception = null;
			T result = default(T);
			dispatcher_.Invoke(() => {
				try {
					result = func();
				}
				catch (Exception ex) {
					exception = ex;
				}
			});
			if (exception != null) {
				throw exception;
			}
			return result;
		}
	}
}
