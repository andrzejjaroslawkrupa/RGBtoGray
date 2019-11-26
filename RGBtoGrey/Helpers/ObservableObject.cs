using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace RGBtoGrey.Helpers
{
	public abstract class ObservableObject : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private void RaisePropertyChangedEvent(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected void OnPropertyChanged<T>(Expression<Func<T>> expression)
		{
			var name = (expression.Body as MemberExpression).Member.Name;
			RaisePropertyChangedEvent(name);
		}
	}
}