using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ComiketSystem.Cheekichi.Infrastructure.Settings
{
	internal class SettingsImpl : CSBaseNotifyPropertyChanged, ICSSettings, IDisposable
	{
		#region Private Fields
		List<Type> knownTypes_ = new List<Type>(); // 既知の型
		Dictionary<string, object> settingsData_ = new Dictionary<string, object>(); // 設定データ
		Dictionary<string, SettingsImpl> settingsChildren_ = new Dictionary<string, SettingsImpl>(); // 子設定
		ISettingsSerializer childSettingsSerializer_ = new DataContractSettingsSerializer(); // 子設定のシリアライザ 
		#endregion

		#region Properties
		internal Dictionary<string, object> SettingsData { get { return settingsData_; } }
		protected SettingsImpl ParentSettings { get; private set; }
		
		#endregion

		#region Constructors
		public SettingsImpl(string tag, ICSActionDispatcher dispatcher)
			: base(dispatcher)
		{
			Tag = tag;
		}

		public SettingsImpl(IEnumerable<Type> knownTypes, ICSActionDispatcher dispatcher = null)
			: this(null, knownTypes, dispatcher)
		{ }

		public SettingsImpl(string tag, IEnumerable<Type> knownTypes, ICSActionDispatcher dispatcher = null)
			: this(tag, dispatcher)
		{
			if (knownTypes != null) {
				knownTypes_.AddRange(knownTypes);
			}
		}

		protected SettingsImpl(SettingsImpl parentSettings, string tag, IEnumerable<Type> knownTypes, ICSActionDispatcher dispatcher = null)
			: this(tag, knownTypes, dispatcher)
		{
			if (parentSettings == null) { throw new ArgumentNullException("parentSettings"); }
			ParentSettings = parentSettings;

			// タグ編集
			List<string> tags = new List<string>();
			var settings = this;
			while(settings != null){
				tags.Add(settings.Tag);
				settings = settings.ParentSettings;
			}
			tags.Reverse();
			Tag = string.Join("_", tags);
		}

		#endregion
		
		#region Private Methods
		string GetTaggedKey(string key, bool isEmbed = false)
		{
			string result;
			if (Tag == null) {
				result = key;
			}
			else {
				result = string.Format("{0}.{1}", Tag, key);
			}

			if (isEmbed) {
				return "__" + result;
			}
			else {
				return result;
			}
		}
		#endregion

		#region ICSSettings
		public IList<Type> KnownTypes
		{
			get
			{
				return knownTypes_;
			}
		}

		public string Tag{get; private set;}


		public void Set<T>(string key, T value)
		{
			var actKey = key;// GetTaggedKey(key);
			bool isNew = true;

			if (SettingsData.ContainsKey(actKey)) {
				var oldValue = SettingsData[actKey];
				if (oldValue != null) {
					isNew = (!oldValue.Equals(value));
				}
				else {
					isNew = (value != null);
				}
			}

			if (isNew) {
				RaisePropertyChanging(key);
				SettingsData[actKey] = value;
				RaisePropertyChanged(key);
			}
		}

		public T Get<T>(string key, T defaultValue = default(T))
		{
			var actKey = key; // GetTaggedKey(key);
			if (SettingsData.ContainsKey(actKey)) {
				return (T)settingsData_[actKey];
			}
			else {
				return defaultValue;
			}
		}

		public bool Exists(string key)
		{
			var actKey = key;// GetTaggedKey(key);
			return SettingsData.ContainsKey(actKey);
		}

		public void Remove(string key)
		{
			var actKey = key; // GetTaggedKey(key);
			if (SettingsData.ContainsKey(actKey)) {
				RaisePropertyChanging(key);
				SettingsData.Remove(actKey);
				RaisePropertyChanged(key);
			}
		}

		public void Clear()
		{
			foreach (var key in SettingsData.Keys.ToArray()) {
				Remove(key);
			}
		}

		public IEnumerable<string> SettingKeys
		{
			get
			{
				return SettingsData.Keys;
			}
		}

		#region Child Settings
		public ICSSettings GetChildSettings(string tag, IEnumerable<Type> knownTypes)
		{
			if (settingsChildren_.ContainsKey(tag)) {
				return settingsChildren_[tag];
			}
			else {
				var settings = new SettingsImpl(this, tag, knownTypes, Dispatcher);

				// 設定をロード
				var setTag = GetTaggedKey(settings.Tag, true);
				var setStr = Get<string>(setTag, null);
				if (setStr != null) {
					using (var ms = new MemoryStream())
					using (var writer = new StreamWriter(ms)) {
						writer.Write(setStr);
						writer.Flush();
						ms.Seek(0, SeekOrigin.Begin);
						childSettingsSerializer_.Deserialize(ms, settings);
					}
				}

				// 子設定に追加
				settings.PropertyChanged += settings_PropertyChanged;
				settingsChildren_[settings.Tag] = settings;
				return settings;
			}
		}

		void settings_PropertyChanging(object sender, System.ComponentModel.PropertyChangingEventArgs e)
		{
			var settings = sender as SettingsImpl;
			if (settings != null) {
				var tag = GetTaggedKey(e.PropertyName);
				RaisePropertyChanging(tag);
			}
		}

		void settings_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			var settings = sender as SettingsImpl;
			if (settings != null) {
				var tag = GetTaggedKey(settings.Tag, true);
				string str = null;
				using(var ms = new MemoryStream()){
					childSettingsSerializer_.Serialize(ms, settings);
					ms.Seek(0, SeekOrigin.Begin);
					using (var reader = new StreamReader(ms)) {
						str = reader.ReadToEnd();
					}
				}
				
				// 設定保存
				Set(tag, str);
			}

		}

		public void RemoveChildSettings(string tag)
		{
			if (settingsChildren_.ContainsKey(tag)) {
				var settings = settingsChildren_[tag];
				settingsChildren_[tag] = null;
				settings.PropertyChanged -= settings_PropertyChanged;
				settings.Dispose();
			}
		}

		public IEnumerable<string> ChildSettingsTags
		{
			get
			{
				return settingsChildren_.Keys;
			}
		}
		#endregion // Child Settings
		#endregion // ICSSettings

		#region IDisposable
		bool isDisposed_ = false;
		virtual protected void Dispose(bool disposing)
		{
			if (isDisposed_) { return; }
			if (disposing) {
			}
			isDisposed_ = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion
	}
}
