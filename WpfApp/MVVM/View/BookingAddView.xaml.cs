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
    /// Represents a partial class for the BookingAddView user control.
    /// </summary>
    public partial class BookingAddView : UserControl
    {
        /// <summary>
        /// Gets or sets the collection of rooms.
        /// </summary>
        public ObservableCollection<Room> Rooms { get; set; } = new ObservableCollection<Room>();

        private string _selectedRoomCost;
        private int _orderId { get; set; }
        private int _customerId { get; set; }

        /// <summary>
        /// Gets or sets the selected room cost.
        /// </summary>
        public string SelectedRoomCost
        {
            get { return _selectedRoomCost; }
            set
            {
                _selectedRoomCost = value;
                OnPropertyChanged(nameof(SelectedRoomCost));
            }
        }

        /// <summary>
        /// Initializes a new instance of the BookingAddView class.
        /// </summary>
        /// <param name="orderId">The order ID.</param>
        /// <param name="customerId">The customer ID.</param>
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

        /// <summary>
        /// Event handler for the Discard button.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void Discard(object sender, RoutedEventArgs e)
        {
            // Close the BookingUpdateView popup
            var popup = this.Parent as Popup;
            if (popup != null)
            {
                popup.IsOpen = false;
            }
        }

        /// <summary>
        /// Event handler for the Add button.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void Add(object sender, RoutedEventArgs e)
        {
            Booking booking = new()
            {
                OrderId = _orderId,
                CustomerId = _customerId,
                CheckIn = DateTime.Parse(CheckInTextBox.Text.IsNullOrEmpty() ? DateTime.Now.ToString() : CheckInTextBox.Text),
                CheckOut = DateTime.Parse(CheckOutTextBox.Text.IsNullOrEmpty() ? DateTime.Now.ToString() : CheckOutTextBox.Text),
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
            else
            {
                MessageBoxResult result = MessageBox.Show("Booking validation failed. Please check the check-in and check-out dates.", "Validation Error", MessageBoxButton.OK);
            }
        }

        /// <summary>
        /// Validates a booking.
        /// </summary>
        /// <param name="booking">The booking to validate.</param>
        /// <returns>True if the booking is valid, false otherwise.</returns>
        private bool ValidateBooking(Booking booking)
        {
            double span = (booking.CheckOut - booking.CheckIn).TotalDays;

            bool checkIn = span < 365;
            bool checkOut = span >= 1;

            return checkIn && checkOut;
        }

        /// <summary>
        /// Raises the PropertyChanged event for the specified property name.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Event that is raised when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }

}
