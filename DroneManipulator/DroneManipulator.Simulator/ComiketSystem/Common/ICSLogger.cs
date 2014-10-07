using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace ComiketSystem
{
	public interface ICSLogger
	{
		/// <summary>
		/// ロガーのタグを取得する
		/// </summary>
		string Tag { get; }

		/// <summary>
		/// ログを記録する
		/// </summary>
		/// <param name="message">メッセージ</param>
		/// <param name="level">ログのレベル</param>
		/// <param name="ex">例外情報</param>
		/// <param name="member">呼出元</param>
		/// <param name="file">呼出元ソースファイル</param>
		/// <param name="line">呼出元行番号</param>
		void Log(
			string message,
			ECSLogLevel level = ECSLogLevel.Information,
			Exception ex = null,
			[CallerFilePath]string file = null,
			[CallerLineNumber]int line = 0,
			[CallerMemberName]string member = null
			);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="format">メッセージのフォーマット</param>
		/// <param name="args">メッセージの引数</param>
		/// <param name="level">ログのレベル</param>
		/// <param name="ex">例外情報</param>
		/// <param name="member">呼出元</param>
		/// <param name="file">呼出元ソースファイル</param>
		/// <param name="line">呼出元行番号</param>
		void Log(
			string format,
			IEnumerable<object> args,
			ECSLogLevel level = ECSLogLevel.Information,
			Exception ex = null,
			[CallerFilePath]string file = null,
			[CallerLineNumber]int line = 0,
			[CallerMemberName]string member = null
			);

		#region レベル毎呼出
		/// <summary>
		/// デバッグログを記録する
		/// </summary>
		/// <param name="message">メッセージ</param>
		/// <param name="ex">例外情報</param>
		/// <param name="member">呼出元</param>
		/// <param name="file">呼出元ソースファイル</param>
		/// <param name="line">呼出元行番号</param>
		void Debug(
			string message,
			Exception ex = null,
			[CallerFilePath]string file = null,
			[CallerLineNumber]int line = 0,
			[CallerMemberName]string member = null
			);

		/// <summary>
		/// 詳細ログを記録する
		/// </summary>
		/// <param name="message">メッセージ</param>
		/// <param name="ex">例外情報</param>
		/// <param name="member">呼出元</param>
		/// <param name="file">呼出元ソースファイル</param>
		/// <param name="line">呼出元行番号</param>
		void Verbose(
			string message,
			Exception ex = null,
			[CallerFilePath]string file = null,
			[CallerLineNumber]int line = 0,
			[CallerMemberName]string member = null
			);

		/// <summary>
		/// 情報ログを記録する
		/// </summary>
		/// <param name="message">メッセージ</param>
		/// <param name="ex">例外情報</param>
		/// <param name="member">呼出元</param>
		/// <param name="file">呼出元ソースファイル</param>
		/// <param name="line">呼出元行番号</param>
		void Information(
			string message,
			Exception ex = null,
			[CallerFilePath]string file = null,
			[CallerLineNumber]int line = 0,
			[CallerMemberName]string member = null
			);

		/// <summary>
		/// 警告ログを記録する
		/// </summary>
		/// <param name="message">メッセージ</param>
		/// <param name="ex">例外情報</param>
		/// <param name="member">呼出元</param>
		/// <param name="file">呼出元ソースファイル</param>
		/// <param name="line">呼出元行番号</param>
		void Warning(
			string message,
			Exception ex = null,
			[CallerFilePath]string file = null,
			[CallerLineNumber]int line = 0,
			[CallerMemberName]string member = null
			);

		/// <summary>
		/// エラーログを記録する
		/// </summary>
		/// <param name="message">メッセージ</param>
		/// <param name="ex">例外情報</param>
		/// <param name="member">呼出元</param>
		/// <param name="file">呼出元ソースファイル</param>
		/// <param name="line">呼出元行番号</param>
		void Error(
			string message,
			Exception ex = null,
			[CallerFilePath]string file = null,
			[CallerLineNumber]int line = 0,
			[CallerMemberName]string member = null
			);

		/// <summary>
		/// もうだめぽなログを記録する
		/// </summary>
		/// <param name="message">メッセージ</param>
		/// <param name="ex">例外情報</param>
		/// <param name="member">呼出元</param>
		/// <param name="file">呼出元ソースファイル</param>
		/// <param name="line">呼出元行番号</param>
		void Fatal(
			string message,
			Exception ex = null,
			[CallerFilePath]string file = null,
			[CallerLineNumber]int line = 0,
			[CallerMemberName]string member = null
			);
		#endregion // レベル毎呼出

		#region フォーマット
		/// <summary>
		/// デバッグログを記録する
		/// </summary>
		/// <param name="format">メッセージのフォーマット</param>
		/// <param name="args">メッセージの引数</param>
		/// <param name="ex">例外情報</param>
		/// <param name="member">呼出元</param>
		/// <param name="file">呼出元ソースファイル</param>
		/// <param name="line">呼出元行番号</param>
		void Debug(
			string format,
			IEnumerable<object> args,
			Exception ex = null,
			[CallerFilePath]string file = null,
			[CallerLineNumber]int line = 0,
			[CallerMemberName]string member = null
			);

		/// <summary>
		/// 詳細ログを記録する
		/// </summary>
		/// <param name="format">メッセージのフォーマット</param>
		/// <param name="args">メッセージの引数</param>
		/// <param name="ex">例外情報</param>
		/// <param name="member">呼出元</param>
		/// <param name="file">呼出元ソースファイル</param>
		/// <param name="line">呼出元行番号</param>
		void Verbose(
			string format,
			IEnumerable<object> args,
			Exception ex = null,
			[CallerFilePath]string file = null,
			[CallerLineNumber]int line = 0,
			[CallerMemberName]string member = null
			);

		/// <summary>
		/// 情報ログを記録する
		/// </summary>
		/// <param name="format">メッセージのフォーマット</param>
		/// <param name="args">メッセージの引数</param>
		/// <param name="ex">例外情報</param>
		/// <param name="member">呼出元</param>
		/// <param name="file">呼出元ソースファイル</param>
		/// <param name="line">呼出元行番号</param>
		void Information(
			string format,
			IEnumerable<object> args,
			Exception ex = null,
			[CallerFilePath]string file = null,
			[CallerLineNumber]int line = 0,
			[CallerMemberName]string member = null
			);

		/// <summary>
		/// 警告ログを記録する
		/// </summary>
		/// <param name="format">メッセージのフォーマット</param>
		/// <param name="args">メッセージの引数</param>
		/// <param name="ex">例外情報</param>
		/// <param name="member">呼出元</param>
		/// <param name="file">呼出元ソースファイル</param>
		/// <param name="line">呼出元行番号</param>
		void Warning(
			string format,
			IEnumerable<object> args,
			Exception ex = null,
			[CallerFilePath]string file = null,
			[CallerLineNumber]int line = 0,
			[CallerMemberName]string member = null
			);

		/// <summary>
		/// エラーログを記録する
		/// </summary>
		/// <param name="format">メッセージのフォーマット</param>
		/// <param name="args">メッセージの引数</param>
		/// <param name="ex">例外情報</param>
		/// <param name="member">呼出元</param>
		/// <param name="file">呼出元ソースファイル</param>
		/// <param name="line">呼出元行番号</param>
		void Error(
			string format,
			IEnumerable<object> args,
			Exception ex = null,
			[CallerFilePath]string file = null,
			[CallerLineNumber]int line = 0,
			[CallerMemberName]string member = null
			);

		/// <summary>
		/// もうだめぽなログを記録する
		/// </summary>
		/// <param name="format">メッセージのフォーマット</param>
		/// <param name="args">メッセージの引数</param>
		/// <param name="ex">例外情報</param>
		/// <param name="member">呼出元</param>
		/// <param name="file">呼出元ソースファイル</param>
		/// <param name="line">呼出元行番号</param>
		void Fatal(
			string format,
			IEnumerable<object> args,
			Exception ex = null,
			[CallerFilePath]string file = null,
			[CallerLineNumber]int line = 0,
			[CallerMemberName]string member = null
			);
		#endregion // フォーマット
	}


	/// <summary>
	/// ログのレベルを表す列挙型
	/// </summary>
	public enum ECSLogLevel
	{
		/// <summary>
		/// デバッグレベル
		/// </summary>
		Debug = 10,

		/// <summary>
		/// 詳細レベル
		/// </summary>
		Verbose = 20,

		/// <summary>
		/// 情報レベル
		/// </summary>
		Information = 30,

		/// <summary>
		/// 警告レベル
		/// </summary>
		Warning = 40,

		/// <summary>
		/// エラーレベル
		/// </summary>
		Error = 50,

		/// <summary>
		/// もうだめぽ
		/// </summary>
		Fatal = 60,
	}
}
