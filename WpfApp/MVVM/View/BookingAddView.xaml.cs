using Core.Models;
using FluentAssertions;
using Microsoft.IdentityModel.Tokens;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp.MVVM.View
{
    /// <summary>
    /// Interaction logic for BookingUpdateView.xaml
    /// </summary>
    public partial class BookingAddView : UserControl
    {
        public ObservableCollection<Room> Rooms { get; set; } = new ObservableCollection<Room>();

        private string _selectedRoomCost;
        private int _orderId { get; set; }
        private int _customerId { get; set; }

        public string SelectedRoomCost
        {
            get { return _selectedRoomCost; }
            set
            {
                _selectedRoomCost = value;
                OnPropertyChanged(nameof(SelectedRoomCost));
            }
        }
        public BookingAddView(int orderId, int customerId)
        {
            InitializeComponent();

            using (var dbContext = new ApplicationDbContext())
            {
                Rooms = new ObservableCollection<Room>(dbContext.Rooms.ToList());
            }

            _orderId = orderId;
            _customerId = customerId;
            DataContext = this;
        }

        private void Discard(object sender, RoutedEventArgs e)
        {
            // Close the BookingUpdateView popup
            var popup = this.Parent as Popup;
            if (popup != null)
            {
                popup.IsOpen = false;
            }

        }
        private void Add(object sender, RoutedEventArgs e)
        {
            Booking booking = new()
            {
                OrderId = _orderId,
                CustomerId = _customerId,
                CheckIn = DateTime.Parse(CheckInTextBox.Text),
                CheckOut = DateTime.Parse(CheckOutTextBox.Text),
                RoomId = int.Parse(ComboBoxStandard.SelectedValue.ToString())
            };

            if (ValidateBooking(booking))
            {
                using (var dbContext = new ApplicationDbContext())
                {
                    dbContext.Bookings.Add(booking);
                    dbContext.SaveChanges();
                }

                Discard(sender, e);
            }
        }

        private bool ValidateBooking(Booking booking)
        {
            double span = (booking.CheckOut - booking.CheckIn).TotalDays;


            bool checkIn = span < 365;

            bool checkOut = span >= 1;


            return checkIn && checkOut;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
