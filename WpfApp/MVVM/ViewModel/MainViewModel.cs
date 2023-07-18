using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.MVVM.Core;
using WpfApp.MVVM.ViewModel;

namespace WpfApp.MVVM.ViewModel
{
    /// <summary>
    /// Represents the main view model for the application.
    /// </summary>
    public class MainViewModel : ObservableObject
    {
        /// <summary>
        /// Gets or sets the command for navigating to the home view.
        /// </summary>
        public RelayCommand HomeViewCommand { get; set; }

        /// <summary>
        /// Gets or sets the command for navigating to the customer view.
        /// </summary>
        public RelayCommand CustomerViewCommand { get; set; }

        /// <summary>
        /// Gets or sets the command for navigating to the room view.
        /// </summary>
        public RelayCommand RoomViewCommand { get; set; }

        /// <summary>
        /// Gets or sets the command for navigating to the order view.
        /// </summary>
        public RelayCommand OrderViewCommand { get; set; }

        /// <summary>
        /// Gets or sets the view model for the home view.
        /// </summary>
        public HomeViewModel HomeVM { get; set; }

        /// <summary>
        /// Gets or sets the view model for the customer view.
        /// </summary>
        public CustomerViewModel CustomerVM { get; set; }

        /// <summary>
        /// Gets or sets the view model for the room view.
        /// </summary>
        public RoomViewModel RoomVM { get; set; }

        /// <summary>
        /// Gets or sets the view model for the order view.
        /// </summary>
        public OrderViewModel OrderVM { get; set; }

        private object _currentView;

        /// <summary>
        /// Gets or sets the current view.
        /// </summary>
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            CustomerVM = new CustomerViewModel();
            RoomVM = new RoomViewModel();
            OrderVM = new OrderViewModel();

            CurrentView = CustomerVM;

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
