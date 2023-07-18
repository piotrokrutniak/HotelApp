using Core.Models;
using FluentAssertions;
using Microsoft.IdentityModel.Tokens;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public BookingAddView()
        {
            InitializeComponent();

            using (var dbContext = new ApplicationDbContext())
            {
                Rooms = new ObservableCollection<Room>(dbContext.Rooms.ToList());
            }

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
                CheckIn = DateTime.Parse(CheckInTextBox.Text),
                CheckOut = DateTime.Parse(CheckOutTextBox.Text),
                Days = 0,
                RoomId = int.Parse(ComboBoxStandard.SelectedValue.ToString())
            };

            if (ValidateBooking(booking))
            {
                using (var dbContext = new ApplicationDbContext())
                {
                    //dbContext.Bookings.Add(booking);
                    //dbContext.SaveChanges();
                }

                //Discard(sender, e);
            }
        }

        private bool ValidateBooking(Booking booking)
        {
            bool checkIn = (booking.CheckIn - booking.CheckOut).TotalDays < 365;

            bool checkOut = (booking.CheckIn - booking.CheckOut).TotalDays >= 1;


            return checkIn && checkOut;
        }
    }
}
