using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.Xml;
using System.IO;

namespace ComiketSystem.Cheekichi.Infrastructure.Settings
{
	internal class DataContractSettingsSerializer : ISettingsSerializer
	{
		ICSLogger logger_ = null;

		public DataContractSettingsSerializer(ICSLogger logger = null)
		{
			logger_ = logger;
		}

		#region ISettingsSerializer メンバー

		public void Serialize(System.IO.Stream stream, SettingsImpl settings)
		{
			var serializationInfoArray = settings.SettingsData.Select(x => SerializeSerializaeInfo(x.Key, x.Value, settings.KnownTypes)).ToArray();
			DataContractSerializer serializer = new DataContractSerializer(typeof(KeyValueSerializationInfo[]));
			serializer.WriteObject(stream, serializationInfoArray);
		}

		public void Deserialize(System.IO.Stream stream, SettingsImpl settings)
		{
			DataContractSerializer serializer = new DataContractSerializer(typeof(KeyValueSerializationInfo[]));
			var serializationInfoArray = (KeyValueSerializationInfo[])serializer.ReadObject(stream);
			var deserialized = serializationInfoArray.Select(x => new { Key = x.Key, Value = DeserializSeserializeInfo(x, settings.KnownTypes) });
			settings.Clear();
			foreach (var des in deserialized) {
				settings.Set(des.Key, des.Value);
			}
		}

		#endregion

		object DeserializSeserializeInfo(KeyValueSerializationInfo info, IEnumerable<Type> knownTypes)
		{
			if (info == null) { throw new ArgumentNullException("info"); }

			// 設定の型を取得
			Type type = Type.GetType(info.Type, false);
			if (type == null) {
				foreach (var knownType in knownTypes) {
					if (knownType.FullName == info.Type) {
						type = knownType;
						break;
					}
				}
			}

			if(type == null){
				if (logger_ != null) {
					logger_.Error("設定取得に失敗しました。(キー:{0}, 型:{1})",
						new object[] { info.Key, info.Type });
				}
				return null;
			}

			// デシリアライズ
			try {
				using (var reader = new StringReader(info.Value))
				using (var xmlReader = new XmlTextReader(reader)) {
					DataContractSerializer serializer = new DataContractSerializer(type, knownTypes);
					return serializer.ReadObject(xmlReader);
				}
			}
			catch (Exception ex) {
				throw new Exception("設定値のデシリアライズに失敗しました。", ex);
			}
		}


		KeyValueSerializationInfo SerializeSerializaeInfo(string key, object value, IEnumerable<Type> knownTypes)
		{
			if (key == null) { throw new ArgumentNullException("key"); }
			if (value == null) { throw new ArgumentNullException("value"); }
			var serializationInfo = new KeyValueSerializationInfo {
				Key = key,
				Type = value.GetType().FullName
			};

			using(var writer = new StringWriter())
			using(var xmlWriter = new XmlTextWriter(writer)){
				try {
					DataContractSerializer serializer = new DataContractSerializer(value.GetType(), knownTypes);
					serializer.WriteObject(xmlWriter, value);
				}
				catch (Exception ex) {
					throw new Exception("設定値のシリアライズに失敗しました。", ex);
				}

				try {
					serializationInfo.Value = writer.ToString();
				}
				catch (Exception ex) {
					throw new Exception("シリアライズ済み文字列の取得に失敗しました。", ex);
				}
			}
			return serializationInfo;
		}
	}

	[DataContract]
	class KeyValueSerializationInfo
	{
		[DataMember]
		public string Type { get; set; }

		[DataMember]
		public string Key { get; set; }

		[DataMember]
		public string Value { get; set; }
	}

}