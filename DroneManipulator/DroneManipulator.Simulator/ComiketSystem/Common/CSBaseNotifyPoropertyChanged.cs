using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Runtime.CompilerServices;

namespace ComiketSystem
{
	[DataContract]
	public class CSBaseNotifyPropertyChanged : INotifyPropertyChanged, INotifyPropertyChanging
	{
		#region コンストラクタ
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CSBaseNotifyPropertyChanged()
		{ }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="dispatcher">ディスパッチャ</param>
		public CSBaseNotifyPropertyChanged(ICSActionDispatcher dispatcher)
		{
			Dispatcher = dispatcher;
		}
		#endregion // コンスト楽アット


		/// <summary>
		/// 値を設定する。
		/// </summary>
		/// <typeparam name="T">変数の型</typeparam>
		/// <param name="dst">設定する対象の変数への参照</param>
		/// <param name="value">設定する値</param>
		/// <param name="propertyName">プロパティ名</param>
		protected virtual bool SetValue<T>(ref T dst, T value, [CallerMemberName]string propertyName = null)
		{
			bool isChanged = false;
			if (dst == null) {
				isChanged = (value != null);
			}
			else {
				isChanged = !dst.Equals(value);
			}

			if (isChanged) {
				RaisePropertyChanging(propertyName);
				dst = value;
				RaisePropertyChanged(propertyName);
				return true;
			}
			return false;
		}

		/// <summary>
		/// 値を設定する。
		/// </summary>
		/// <typeparam name="T">変数の型</typeparam>
		/// <param name="dst">設定する対象の変数への参照</param>
		/// <param name="value">設定する値</param>
		/// <param name="actBeforeChange">値が変更される前に実行する処理</param>
		/// <param name="actAfterChange">値が変更された後に実行される処理</param>
		/// <param name="propertyName">プロパティ名</param>
		/// <returns>値が変更されたらtrue, されなければfalse</returns>
		/// <remarks>actBeforeChange, actAfterChangeは、値が変更される時のみ実行される。</remarks>
		protected virtual bool SetValue<T>(
			ref T dst,
			T value,
			Action<T> actBeforeChange,
			Action<T> actAfterChange,
			[CallerMemberName]string propertyName = null)
		{
			bool isChanged = false;
			if (dst == null) {
				isChanged = (value != null);
			}
			else {
				isChanged = !dst.Equals(value);
			}

			if (isChanged){
				RaisePropertyChanging(propertyName);
				if (actBeforeChange != null) {
					actBeforeChange(dst);
				}
				dst = value;
				if (actAfterChange != null) {
					actAfterChange(dst);
				}
				RaisePropertyChanged(propertyName);
				return true;
			}
			return false;
		}


		/// <summary>
		/// プロパティの変更完了を通知する
		/// </summary>
		/// <param name="propertyName"></param>
		protected virtual void RaisePropertyChanged([CallerMemberName]string propertyName = null)
		{
			if (PropertyChanged != null) {
				Dispatch(() => {
					PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
				});
			}
		}

		/// <summary>
		/// プロパティの変更開始を通知する
		/// </summary>
		/// <param name="propertyName"></param>
		protected virtual void RaisePropertyChanging([CallerMemberName]string propertyName = null)
		{
			if (PropertyChanging != null) {
				Dispatch(() => {
					PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
				});
			}
		}

		/// <summary>
		/// ディスパッチする
		/// </summary>
		/// <param name="act"></param>
		protected void Dispatch(Action act)
		{
			if (act == null) { throw new ArgumentNullException("act"); }
			if (Dispatcher != null) {
				Dispatcher.Dispatch(act);
			}
			else {
				act();
			}
		}

		protected T Dispatch<T>(Func<T> func)
		{
			if (func == null) { throw new ArgumentNullException("func"); }
			if (Dispatcher != null) {
				return Dispatcher.Dispatch(func);
			}
			else {
				return func();
			}
		}

		/// <summary>
		/// ディスパッチャを取得・設定する
		/// </summary>
		[IgnoreDataMember]
		public ICSActionDispatcher Dispatcher { get; set; }

		/// <summary>
		/// 式からメンバ名を取得する。
		/// </summary>
		/// <typeparam name="MemberType">メンバの型</typeparam>
		/// <param name="expression">式</param>
		/// <returns>メンバ名</returns>
		public string GetMemberName<MemberType>(Expression<Func<MemberType>> expression)
		{
			return ((MemberExpression)expression.Body).Member.Name;
		}


		#region INotifyPropertyChanged メンバー

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		#region INotifyPropertyChanging メンバー

		public event PropertyChangingEventHandler PropertyChanging;

		#endregion
	}

	public interface ICSActionDispatcher
	{
		void Dispatch(Action act);
		T Dispatch<T>(Func<T> func);
	}

}
