using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComiketSystem.Cheekichi.Infrastructure.Log
{
	internal class PassLogger : Logger
	{
		Logger parentLogger_ = null;

		public PassLogger(
			string tag,
			Logger parentLogger)
			: base(tag)
		{
			if (parentLogger == null) { throw new ArgumentNullException("parentLogger"); }
			parentLogger_ = parentLogger;
			parentLogger_.Logged += parentLogger__Logged;
		}

		void parentLogger__Logged(object sender, LoggedEventArgs e)
		{
			OnLogged(e);
		}


		public override void Log(string tag, string message, ECSLogLevel level = ECSLogLevel.Information, Exception ex = null, string file = null, int line = 0, string member = null)
		{
			parentLogger_.Log(tag, message, level, ex, file, line, member);
		}

		//aprotected override void Log(string tag, string message, ECSLogLevel level = ECSLogLevel.Information, Exception ex = null, string file = null, int line = 0, string member = null)
//		{
			//parentLogger_.Log(tag, message, level, ex, file, line, member);
		//}
	}
}
