using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComiketSystem.Cheekichi.Infrastructure.Log
{
	internal class Logger : ICSLogger
	{
		#region Events
		public event EventHandler<LoggedEventArgs> Logged;

		protected void OnLogged(LoggedEventArgs e)
		{
			if (Logged != null) {
				Logged(this, e);
			}
		}
		#endregion

		#region コンストラクタ
		public Logger(string tag)
		{
			if (tag == null) { throw new ArgumentNullException("tag"); }
			Tag = tag;
		}
		#endregion

		#region ICSLogger メンバー
		public string Tag
		{
			get;
			set;
		}


		public virtual void Log(string tag, string message, ECSLogLevel level = ECSLogLevel.Information, Exception ex = null, string file = null, int line = 0, string member = null)
		{
			if (Logged != null) {
				var data = new LogData {
					Tag = tag,
					Message = message,
					Level = level,
					Exception = ex,
					File = file,
					Line = line,
					Member = member,
					Time = DateTime.Now
				};
				OnLogged(new LoggedEventArgs(data));
			}
		}


		public void Log(string message, ECSLogLevel level = ECSLogLevel.Information, Exception ex = null, string file = null, int line = 0, string member = null)
		{
			Log(
				Tag,
				message,
				level,
				ex,
				file,
				line,
				member);
		}

		public void Log(string format, IEnumerable<object> args, ECSLogLevel level = ECSLogLevel.Information, Exception ex = null, string file = null, int line = 0, string member = null)
		{
			Log(
				string.Format(format, args.ToArray()),
				level, ex, file, line, member
			);
		}

		public void Debug(string message, Exception ex = null, string file = null, int line = 0, string member = null)
		{
			Log(message, ECSLogLevel.Debug, ex, file, line, member);
		}

		public void Verbose(string message, Exception ex = null, string file = null, int line = 0, string member = null)
		{
			Log(message, ECSLogLevel.Verbose, ex, file, line, member);
		}

		public void Information(string message, Exception ex = null, string file = null, int line = 0, string member = null)
		{
			Log(message, ECSLogLevel.Information, ex, file, line, member);
		}

		public void Warning(string message, Exception ex = null, string file = null, int line = 0, string member = null)
		{
			Log(message, ECSLogLevel.Warning, ex, file, line, member);
		}

		public void Error(string message, Exception ex = null, string file = null, int line = 0, string member = null)
		{
			Log(message, ECSLogLevel.Error, ex, file, line, member);
		}

		public void Fatal(string message, Exception ex = null, string file = null, int line = 0, string member = null)
		{
			Log(message, ECSLogLevel.Fatal, ex, file, line, member);
		}

		public void Debug(string format, IEnumerable<object> args, Exception ex = null, string file = null, int line = 0, string member = null)
		{
			Log(format, args, ECSLogLevel.Debug, ex, file, line, member);
		}

		public void Verbose(string format, IEnumerable<object> args, Exception ex = null, string file = null, int line = 0, string member = null)
		{
			Log(format, args, ECSLogLevel.Verbose, ex, file, line, member);
		}

		public void Information(string format, IEnumerable<object> args, Exception ex = null, string file = null, int line = 0, string member = null)
		{
			Log(format, args, ECSLogLevel.Information, ex, file, line, member);
		}

		public void Warning(string format, IEnumerable<object> args, Exception ex = null, string file = null, int line = 0, string member = null)
		{
			Log(format, args, ECSLogLevel.Warning, ex, file, line, member);
		}

		public void Error(string format, IEnumerable<object> args, Exception ex = null, string file = null, int line = 0, string member = null)
		{
			Log(format, args, ECSLogLevel.Error, ex, file, line, member);
		}

		public void Fatal(string format, IEnumerable<object> args, Exception ex = null, string file = null, int line = 0, string member = null)
		{
			Log(format, args, ECSLogLevel.Fatal, ex, file, line, member);
		}

		#endregion
	}
}
