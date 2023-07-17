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
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand CustomerViewCommand { get; set; }
        public RelayCommand RoomViewCommand { get; set; }
        public RelayCommand OrderViewCommand { get; set; }
        public HomeViewModel HomeVM { get; set; }
        public CustomerViewModel CustomerVM { get; set; }
        public RoomViewModel RoomVM { get; set; }
        public OrderViewModel OrderVM { get; set; }

        private object _currentView;
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
            RoomVM = new RoomViewModel();
            OrderVM = new OrderViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(x =>
            {
                CurrentView = HomeVM;
            });

            RoomViewCommand = new RelayCommand(x =>
            {
                CurrentView = RoomVM;
            });

            CustomerViewCommand = new RelayCommand(x =>
            {
                CurrentView = CustomerVM;
            });

            OrderViewCommand = new RelayCommand(x =>
            {
                CurrentView = OrderVM;
            });



        }

        
    }
}
