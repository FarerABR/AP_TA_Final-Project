using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Core;

namespace UI.MVVM.ViewModel
{
	public class MainViewModel : ObservableObject
	{
		private HomeViewModel HomeVM { get; set; }


		private Object _currentView;
		public Object CurrentView
		{
			get { return _currentView; }
			set
			{
				_currentView = value;
				OnPropertyChanged();
			}
		}

		public MainViewModel()
		{
			HomeVM = new HomeViewModel();
			CurrentView = HomeVM;
		}
	}
}
