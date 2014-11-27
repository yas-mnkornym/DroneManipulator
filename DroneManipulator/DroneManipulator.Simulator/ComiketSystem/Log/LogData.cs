using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ComiketSystem.Cheekichi.Infrastructure.Log
{
	/// <summary>
	/// ログデータ
	/// </summary>
	[DataContract]
	internal class LogData
	{
		/// <summary>
		/// タグを取得・設定する。
		/// </summary>
		[DataMember]
		public string Tag { get; set; }

		/// <summary>
		/// メッセージを取得・設定する。
		/// </summary>
		[DataMember]
		public string Message { get; set; }

		/// <summary>
		/// ログレベルを取得・設定する。
		/// </summary>
		[DataMember]
		public ECSLogLevel Level { get; set; }

		/// <summary>
		/// 例外情報を取得・設定する。		
		/// </summary>
		[DataMember]
		public Exception Exception { get; set; }

		/// <summary>
		/// ログを記録したソースコードのファイル名を取得・設定する。
		/// </summary>
		[DataMember]
		public string File { get; set; }

		/// <summary>
		/// ログを記録したソースコードの行番号を取得・設定する。
		/// </summary>
		[DataMember]
		public int Line { get; set; }

		/// <summary>
		/// ログを記録したメンバ名を取得・設定する。
		/// </summary>
		[DataMember]
		public string Member { get; set; }

		/// <summary>
		/// ログが記録された時刻を取得・設定する。
		/// </summary>
		[DataMember]
		public DateTime Time { get; set; }
	}
}
