using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Core;

namespace UI.MVVM.ViewModel
{
    public class ConfigViewModel : ObservableObject
    {
        public ServerConfigViewModel ServerVM { get; set; }
        public RelayComamnd ServerViewCommand { get; set; }

        public ClientConfigViewModel ClientVM { get; set; }
        public RelayComamnd ClientViewCommand { get; set; }

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

        public ConfigViewModel()
        {
            ServerVM = new ServerConfigViewModel();
            ClientVM = new ClientConfigViewModel();

            CurrentView = ServerVM;

            ServerViewCommand = new RelayComamnd(o =>
            {
                CurrentView = ServerVM;
            });

            ClientViewCommand = new RelayComamnd(o =>
            {
                CurrentView = ClientVM;
            });
        }
    }
}