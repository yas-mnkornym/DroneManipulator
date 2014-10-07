using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.IO;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ComiketSystem
{
	public class CSSettingsBase : CSBaseNotifyPropertyChanged
	{
		public ICSSettings Settings { get; private set; }

		protected CSSettingsBase(ICSSettings settings, ICSActionDispatcher dispatcher = null)
			: base(dispatcher)
		{
			if (settings == null) { throw new ArgumentNullException("settings"); }
			Settings = settings;
			settings.PropertyChanged += settings_PropertyChanged;
			settings.PropertyChanging += settings_PropertyChanging;
		}

		protected void settings_PropertyChanging(object sender, PropertyChangingEventArgs e)
		{
			RaisePropertyChanging(e.PropertyName);
		}

		protected void settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			RaisePropertyChanged(e.PropertyName);
		}

		/// <summary>
		/// 呼び出したプロパティ名の設定を取得する。
		/// </summary>
		/// <typeparam name="T">設定の型</typeparam>
		/// <param name="defaultValue">デフォルト値</param>
		/// <param name="key">プロパティ名</param>
		/// <returns>取得した値</returns>
		protected T GetMe<T>(T defaultValue = default(T), [CallerMemberName]string key = null)
		{
			return Settings.Get(key, defaultValue);
		}

		/// <summary>
		/// 呼び出したプロパティ名の設定を設定する。
		/// </summary>
		/// <typeparam name="T">設定の型</typeparam>
		/// <param name="value">値</param>
		/// <param name="key">プロパティ名</param>
		protected void SetMe<T>(T value, [CallerMemberName]string key = null)
		{
			Settings.Set(key, value);
		}

		/// <summary>
		/// 呼び出したプロパティ名の設定を削除する。
		/// </summary>
		/// <param name="key">プロパティ名</param>
		protected void RemoveMe([CallerMemberName]string key = null)
		{
			Settings.Remove(key);
		}

		/// <summary>
		/// 設定を設定する
		/// </summary>
		/// <typeparam key="T">データの型</typeparam>
		/// <param key="key">データのキー</param>
		/// <param key="value">データの値</param>
		protected void Set<T>(string key, T value)
		{
			Settings.Set(key, value);
		}

		/// <summary>
		/// 設定を取得する
		/// </summary>
		/// <typeparam key="T">データの型</typeparam>
		/// <param key="key">データのキー</param>
		/// <param key="defaultValue">データの値</param>
		/// <returns>データ</returns>
		protected T Get<T>(string key, T defaultValue = default(T))
		{
			return Settings.Get(key, defaultValue);
		}

		/// <summary>
		/// データを削除する
		/// </summary>
		/// <param key="key">データのキー</param>
		protected void Remove(string key)
		{
			Settings.Remove(key);
		}

		/// <summary>
		/// データを全て削除する。
		/// </summary>
		protected void Clear()
		{
			Settings.Clear();
		}
	}
	/*
	/// <summary>
	/// 設定ベース実装
	/// </summary>
	[DataContract]
	public class  aCSSettingsBaseImpl : BaseNotifyPoropertyChanged
	{
		protected object settingsDataLockObj_ = new object();
		Dictionary<string, object> settingsData_ = new Dictionary<string, object>();

		#region コンストラクタ
		/// <summary>
		/// コンストラクタ
		/// </summary>
		protected aCSSettingsBaseImpl()
		{ }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="dispatcher">ディスパッチャ</param>
		protected aCSSettingsBaseImpl(ICSActionDispatcher dispatcher)
			: base(dispatcher)
		{ }
		#endregion // コンストラクタ


		/// <summary>
		/// 他のインスタンスからコピーする
		/// </summary>
		/// <param name="obj"></param>
		protected void CopyFrom(aCSSettingsBaseImpl obj)
		{
			this.settingsData_ = new Dictionary<string, object>(obj.settingsData_);
		}

		#region インデクサ
		/// <summary>
		/// 設定を取得・設定する
		/// </summary>
		/// <param name="key">設定のキー</param>
		/// <returns>設定の値</returns>
		[IgnoreDataMember]
		public object this[string key]
		{
			get
			{
				try {
					if (settingsData_.ContainsKey(key)) {
						return settingsData_[key];
					}
					else {
						return null;
					}
				}
				catch (Exception ex) {
					throw new Exception(string.Format("設定 {0} の値が存在しません。", key), ex);
				}
			}
			set
			{
				bool isNew = true;
				if (settingsData_.ContainsKey(key)) {
					if (settingsData_[key] != null) {
						isNew = (!settingsData_[key].Equals(value));
					}
					else {
						isNew = (value != null);
					}
				}

				if (isNew) {
					RaisePropertyChanging(key);
					lock (settingsDataLockObj_) {
						settingsData_[key] = value;
					}
					RaisePropertyChanged(key);
				}
			}
		}
		#endregion

		#region Get/Set
		/// <summary>
		/// 設定を設定する
		/// </summary>
		/// <typeparam name="T">設定の型</typeparam>
		/// <param name="name">設定名</param>
		/// <param name="value">設定値</param>
		public void Set<T>([CallerMemberName]string name = null, T value = default(T))
		{
			this[name] = value;
		}

		/// <summary>
		/// 設定を取得する
		/// </summary>
		/// <typeparam name="T">設定の型</typeparam>
		/// <param name="name">設定名</param>
		/// <param name="defaultValue">デフォルト値</param>
		/// <returns></returns>
		public T Get<T>([CallerMemberName]string name = null, T defaultValue = default(T))
		{
			var val = this[name];
			if (val is T) {
				return (T)val;
			}
			else {
				return defaultValue;
			}
		}
		#endregion

		/// <summary>
		/// プロパティの名前を取得する
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="expr"></param>
		/// <returns></returns>
		public string GetSettingName<T>(System.Linq.Expressions.Expression<Func<T>> expr)
		{
			var memberExpr = (System.Linq.Expressions.MemberExpression)expr.Body;
			return memberExpr.Member.Name;
		}

		#region 設定変更通知
		#endregion

		#region 設定の本体
		/// <summary>
		/// 設定データのコレクションを取得・設定するする
		/// </summary>
		[DataMember]
		public Dictionary<string, object> SettingData
		{
			get
			{
				return settingsData_;
			}
			set
			{
				settingsData_ = value;
			}
		}
		#endregion

		#region 公開プロパティ
		/// <summary>
		/// 設定を全て削除する
		/// </summary>
		public void ClearAllSettings()
		{
			var keys = settingsData_.Keys.ToArray();
			settingsData_.Clear();
			foreach (var key in keys) {
				RaisePropertyChanged(key);
			}
		}

		/// <summary>
		/// 設定を削除する
		/// </summary>
		/// <param name="key"></param>
		public void ClearSetting(string key)
		{
			if (settingsData_.ContainsKey(key)) {
				RaisePropertyChanging(key);
				SettingData.Remove(key);
				RaisePropertyChanged(key);
			}
		}
		#endregion
	}

	public abstract class aCSSettingsBase<SettingsType> : aCSSettingsBaseImpl
		where SettingsType : aCSSettingsBaseImpl
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public aCSSettingsBase()
		{ }

		/// <summary>
		/// コンストラクタ、設定をファイルから読み込む。
		/// </summary>
		/// <param name="path">読み込み元ファイル</param>
		public aCSSettingsBase(string path)
		{
			LoadFromFile(path);
		}

		/// <summary>
		/// コンストラクタ、設定をストリームから読み込む。
		/// </summary>
		/// <param name="str">読み込み元ストリーム</param>
		public aCSSettingsBase(Stream str)
		{
			LoadFromStream(str);
		}

		abstract public ICSSettings GetChildSettings(string name);

		#region 入出力
		/// <summary>
		/// 設定をファイルから読み込む
		/// </summary>
		/// <param name="path">読み込み元ファイル</param>
		public void LoadFromFile(string path)
		{
			using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read)) {
				LoadFromStream(fs);
			}
		}

		/// <summary>
		/// 設定をストリームから読み込む
		/// </summary>
		/// <param name="str">読み込み元ストリーム</param>
		public void LoadFromStream(Stream str)
		{
			lock ( settingsDataLockObj_) {
				DataContractSerializer dcs = new DataContractSerializer(typeof(SettingsType));
				CopyFrom((SettingsType)dcs.ReadObject(str));
			}
		}

		/// <summary>
		/// 設定をファイルに出力する
		/// </summary>
		/// <param name="path"></param>
		public void SaveToFile(string path)
		{
			using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read)) {
				SaveToStream(fs);
			}
		}

		/// <summary>
		/// 設定をストリームに出力する
		/// </summary>
		/// <param name="str"></param>
		public void SaveToStream(Stream str)
		{
			lock ( settingsDataLockObj_) {
				DataContractSerializer dcs = new DataContractSerializer(typeof(SettingsType));
				dcs.WriteObject(str, this);
			}
		}
		#endregion // 入出力
	}
	 * */
}
