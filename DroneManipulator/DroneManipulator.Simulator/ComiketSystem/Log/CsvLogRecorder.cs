using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ComiketSystem.Csv;

namespace ComiketSystem.Cheekichi.Infrastructure.Log
{
	/// <summary>
	/// CSV出力するログレコーダ
	/// </summary>
	internal class CsvLogRecorder : LogRecorderBase
	{
		StreamWriter writer_ = null;
		ECSLogLevel MinimumLogLevel { get; set; }

		public CsvLogRecorder(Logger logger, string filename, bool append, ECSLogLevel minimum, Encoding encoding = null)
			: base(logger)
		{
			writer_ = new StreamWriter(filename, append, encoding ?? Encoding.UTF8);
			writer_.AutoFlush = true;
			MinimumLogLevel = minimum;
		}

		public CsvLogRecorder(Logger logger, Stream stream, ECSLogLevel minimumLogLevel, Encoding encoding = null, bool leaveOpen = false)
			: base(logger)
		{
			if (stream == null) { throw new ArgumentNullException("stream"); }
			writer_ = new StreamWriter(stream, encoding ?? Encoding.UTF8, 4096, leaveOpen);
			writer_.AutoFlush = true;
			MinimumLogLevel = minimumLogLevel;
		}

		protected override void OnLogged(object sender, LoggedEventArgs e)
		{
			var log = e.Data;
			if (log == null) { return; }
			if (log.Level < MinimumLogLevel) { return; }
			if (writer_ == null) { throw new InvalidOperationException("writer_がnullです。"); }
			string csvString = null;
			try{
				CsvMaker cm = new CsvMaker();
				cm.AddToken(log.Level);
				cm.AddToken(log.Tag);
				cm.AddToken(log.Time.ToString("yyyy/MM/dd HH:mm:ss.fff"));
				cm.AddToken(log.Message);
				cm.AddToken(log.Exception);

				// Exceptionがnullの時はコード情報出力しないようにしてる
				if (log.Exception != null || log.Level >= ECSLogLevel.Error) {
					cm.AddToken(log.File);
					cm.AddToken(log.Line);
					cm.AddToken(log.Member);
				}
				csvString = cm.ToString();
			}
			catch(Exception ex){
				throw new Exception("ログ情報からCSVへの変換に失敗しました。", ex);
			}
			writer_.WriteLine(csvString);
		}

		bool isDisposed_ = false;
		protected override void Dispose(bool disposing)
		{
			if (isDisposed_) { return; }
			if (disposing) {
				writer_.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}