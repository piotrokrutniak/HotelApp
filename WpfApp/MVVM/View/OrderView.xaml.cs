using Core.Models;
using Microsoft.EntityFrameworkCore;
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
using WpfApp.MVVM.ViewModel;

namespace WpfApp.MVVM.View
{
    /// <summary>
    /// Represents a user control for viewing orders.
    /// </summary>
    public partial class OrderView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderView"/> class.
        /// </summary>
        public OrderView()
        {
            InitializeComponent();
            DataContext = new OrderViewModel();
        }

        /// <summary>
        /// Creates an instance of the OrderAddView and shows it as a popup window.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            OrderAddView orderAddView = new OrderAddView();
            var parentWindow = Window.GetWindow(this);

            Popup popup = new Popup
            {
                Child = orderAddView,
                Width = orderAddView.Width,
                Height = orderAddView.Height,
                PlacementTarget = parentWindow,
                Placement = PlacementMode.Center,
                IsOpen = true
            };
        }

        /// <summary>
        /// Creates an instance of the OrderUpdateView and shows it as a popup window.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var order = (Order)button.DataContext;
            var bookings = new ObservableCollection<Booking>();

            using (var context = new ApplicationDbContext())
            {
                bookings = new ObservableCollection<Booking>(context.Bookings.Include(x => x.Room));
            }

            OrderUpdateView orderUpdateView = new OrderUpdateView(bookings.ToList(), order);
            orderUpdateView.DataContext = order;

            Popup popup = new Popup
            {
                Child = orderUpdateView,
                Width = orderUpdateView.Width,
                Height = orderUpdateView.Height,
                PlacementTarget = this,
                Placement = PlacementMode.Center,
                IsOpen = true
            };
        }

        /// <summary>
        /// Deletes the selected order from the database.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var order = (Order)button.DataContext;

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                using (var dbContext = new ApplicationDbContext())
                {
                    dbContext.Orders.Remove(order);
                    dbContext.SaveChanges();
                }
            }
        }
    }

}
