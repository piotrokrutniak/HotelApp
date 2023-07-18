using Core.Models;
using Persistence.Context;
using System;
using System.Collections.Generic;
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
using WpfApp.MVVM.ViewModel;

namespace WpfApp.MVVM.View
{
    /// <summary>
    /// Represents a user control for updating an order.
    /// </summary>
    public partial class OrderUpdateView : UserControl
    {
        /// <summary>
        /// Gets or sets the list of bookings.
        /// </summary>
        public List<Booking> Bookings;

        /// <summary>
        /// Gets or sets the order ID.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the customer ID.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderUpdateView"/> class.
        /// </summary>
        /// <param name="bookings">The list of bookings.</param>
        /// <param name="order">The order object.</param>
        public OrderUpdateView(List<Booking> bookings, Order order)
        {
            InitializeComponent();
            Bookings = bookings;

            OrderId = order.Id;
            CustomerId = order.CustomerId;
        }

        /// <summary>
        /// Closes the OrderUpdateView popup.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void Discard(object sender, RoutedEventArgs e)
        {
            var popup = this.Parent as Popup;
            if (popup != null)
            {
                popup.IsOpen = false;
            }
        }

        /// <summary>
        /// Updates the total price of the order and saves the changes to the database.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void Save(object sender, RoutedEventArgs e)
        {
            var order = DataContext as Order;
            order.Total = order.Bookings.Sum(x => x.Room.Price * Convert.ToInt32(Math.Ceiling((x.CheckOut - x.CheckIn).TotalDays)));

            using (var dbContext = new ApplicationDbContext())
            {
                dbContext.Orders.Update(order);
                dbContext.SaveChanges();
            }

            Discard(sender, e);
        }

        /// <summary>
        /// Creates an instance of the BookingAddView and shows it as a popup window.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            BookingAddView updateView = new BookingAddView(OrderId, CustomerId);
            var parentWindow = Window.GetWindow(this);

            Popup popup = new Popup
            {
                Child = updateView,
                Width = updateView.Width,
                Height = updateView.Height,
                PlacementTarget = parentWindow,
                Placement = PlacementMode.Center,
                IsOpen = true
            };
        }

        /// <summary>
        /// Creates an instance of the CustomerUpdateView and shows it as a popup window.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var customer = (Customer)button.DataContext;

            CustomerUpdateView updateView = new CustomerUpdateView();
            updateView.DataContext = customer;

            Popup popup = new Popup
            {
                Child = updateView,
                Width = updateView.Width,
                Height = updateView.Height,
                PlacementTarget = this,
                Placement = PlacementMode.Center,
                IsOpen = true
            };
        }

        /// <summary>
        /// Deletes the selected customer from the database.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var customer = (Customer)button.DataContext;

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                using (var dbContext = new ApplicationDbContext())
                {
                    dbContext.Customers.Remove(customer);
                    dbContext.SaveChanges();
                }
            }
        }
    }

}
