using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.MVVM.Core;
using WpfApp.MVVM.ViewModel;

namespace WpfApp.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private object _currentView;
        public HomeViewModel HomeVM { get; set; }
        public CustomerViewModel CustomerVM { get; set; }
        public object CurrentView
        {
            get { return _currentView; }
            set 
            { 
                _currentView = value; 
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            CustomerVM = new CustomerViewModel();
            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(x =>
            {
                CurrentView = HomeVM;
            });

            CustomerViewCommand = new RelayCommand(x =>
            {
                CurrentView = CustomerVM;
            });

        }

        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand CustomerViewCommand { get; set; }
    }
}
