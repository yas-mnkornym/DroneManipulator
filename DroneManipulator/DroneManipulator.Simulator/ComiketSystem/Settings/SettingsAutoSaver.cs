using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Timers;

namespace ComiketSystem.Cheekichi.Infrastructure.Settings
{
	internal class SettingsAutoSaver : IDisposable
	{
		#region private変数
		ISettingsSerializer serializer_ = null;
		Stream stream_ = null;
		object saveLock_ = new object();
		Timer saveTimer = null;
		bool isSettingsChanged_ = false;
		#endregion

		#region プロパティ
		ICSLogger Logger { get; set; }
		public string SettingsFile { get; private set; }
		public string SettingsTempFile { get; private set; }
		public int SaveDelay { get; private set; }
		public bool IsMonitoring { get; private set; }
		public SettingsImpl Settings { get; private set; }
		#endregion

		#region コンストラクタ
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="settings">設定インスタンス</param>
		/// <param name="settingsFile">設定を保存するファイル名</param>
		/// <param name="saveDelay">保存ディレイを(ミリ秒単位)</param>
		public SettingsAutoSaver(
			SettingsImpl settings,
			string settingsFile,
			ICSLogger logger,
			int saveDelay = 300)
		{
			if (settings == null) { throw new ArgumentNullException("settigns"); }
			if (settingsFile == null) { throw new ArgumentNullException("settingsFile"); }
			if (logger == null) { throw new ArgumentNullException("logger"); }
			if (saveDelay < 0) { throw new ArgumentOutOfRangeException("saveDelay", "saveDelayは0以上の整数である必要があります。"); }
			Logger = logger;
			SaveDelay = saveDelay;
			SettingsFile = settingsFile;
			SettingsTempFile = SettingsFile + ".temp";
			Settings = settings;
			IsMonitoring = false;

		}
		#endregion

		#region public変数
		/// <summary>
		/// 監視を開始する。
		/// </summary>
		public void StartMonitoring()
		{
			if (IsMonitoring) { throw new InvalidOperationException("既に監視中です。"); }
			try {
				// シリアライザを作成
				try {
					serializer_ = new DataContractSettingsSerializer(Logger);
				}
				catch (Exception ex) {
					throw new Exception("シリアライザの作成に失敗しました。", ex);
				}

				// 保存用ファイルのストリームを開く
				try {
					// stream_.SetLength(0)のために必ず FileAccess.ReadWrite を指定！
					stream_ = new FileStream(SettingsTempFile, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
				}
				catch (Exception ex) {
					serializer_ = null;
					throw new Exception("設定書き出し用ファイルの作成に失敗しました。", ex);
				}

				// 設定変更の監視を開始
				Settings.PropertyChanged += Settings_PropertyChanged;
				saveTimer = new Timer(SaveDelay);
				saveTimer.AutoReset = false;
				saveTimer.Elapsed += saveTiemr__Elapsed;
				IsMonitoring = true;
			}
			catch (Exception ex) {
				IsMonitoring = false;
				throw ex;
			}
		}

		/// <summary>
		/// 監視を停止する。
		/// </summary>
		public void EndMonitoring()
		{
			try {
				Settings.PropertyChanged -= Settings_PropertyChanged;


				if (saveTimer != null) {
					saveTimer.Dispose();
					saveTimer = null;
				}

				// タイマ止めた後に変更されていれば保存
				try {
					lock (saveLock_) {
						if (isSettingsChanged_) {
							Save();
						}
					}
				}
				finally {
					serializer_ = null;

					if (stream_ != null) {
						stream_.Dispose();
						stream_ = null;
					}
				}
			}
			finally {
				IsMonitoring = false;
			}
		}

		/// <summary>
		/// 強制的にセーブを行う
		/// </summary>
		public void SaveImmediately(){
			lock(saveLock_){
				isSettingsChanged_ = true;
				Save();
			}
		}

		#endregion

		#region private変数
		/// <summary>
		/// 設定を保存する。
		/// </summary>
		void Save()
		{
			// ファイルを空にする
			try {
				stream_.SetLength(0);
			}
			catch (Exception ex) {
				throw new Exception("ファイルの初期化に失敗しました。", ex);
			}

			// 設定をシリアラズしてファイルに保存
			try {
				serializer_.Serialize(stream_, Settings);
			}
			catch (Exception ex) {
				throw new Exception("設定のシリアライズに失敗しました。", ex);
			}

			// 一時ファイルから本来のファイルにコピーする
			try {
				File.Copy(SettingsTempFile, SettingsFile, true);
			}
			catch (Exception ex) {
				throw new Exception("設定の上書きに失敗しました。", ex);
			}

			// 変更フラグを折る
			lock (saveLock_) {
				isSettingsChanged_ = false;
			}
		}
		#region 設定変更通知処理
		void Settings_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			lock (saveLock_) {
				isSettingsChanged_ = true;
			}
			if (saveTimer != null) {
				saveTimer.Stop();
				saveTimer.Start();
			}
		}

		/// <summary>
		/// 設定タイマ時間経過時
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void saveTiemr__Elapsed(object sender, ElapsedEventArgs e)
		{
			try {
				Save();
			}
			catch (Exception ex) {
				Logger.Error("設定の保存に失敗しました。", ex);
			}
		}
		#endregion
		#endregion

		#region IDisposable メンバー

		bool isDisposed_ = false;
		virtual protected void Dispose(bool disposing)
		{
			if (isDisposed_) { return; }
			if (disposing) {
				try {
					EndMonitoring();
				}
				catch (Exception ex) {
					Logger.Error("SettingsAutoSaverのDispose()による監視停止に失敗しました。", ex);
				}
			}
			isDisposed_ = false;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		#endregion

	}
}
