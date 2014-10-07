using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComiketSystem.Cheekichi.Infrastructure.Log
{
	internal class LoggedEventArgs : EventArgs
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="data">ログデータ</param>
		public LoggedEventArgs(LogData data)
		{
			Data = data;
		}

		/// <summary>
		/// ログデータを取得する
		/// </summary>
		public LogData Data { get; private set; }
	}
}
