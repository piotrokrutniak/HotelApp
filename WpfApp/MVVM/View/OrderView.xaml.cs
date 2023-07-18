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
    /// Interaction logic for OrderView.xaml
    /// </summary>
    public partial class OrderView : UserControl
    {
        public OrderView()
        {
            InitializeComponent();
            DataContext = new OrderViewModel();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of the UpdateView
            OrderAddView updateView = new OrderAddView();
            var parentWindow = Window.GetWindow(this);

            // Show the UpdateView as a popup window
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

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected Order object from the ListBox
            var button = (Button)sender;
            var order = (Order)button.DataContext;
            var bookings = new ObservableCollection<Booking>();

            using (var context = new ApplicationDbContext())
            {
                bookings = new ObservableCollection<Booking>(context.Bookings.Include(x => x.Room));
            }

            // Create an instance of the UpdateView
            OrderUpdateView updateView = new OrderUpdateView(bookings.ToList(), order);

            // Set the DataContext of the UpdateView to the selected Order
            updateView.DataContext = order;

            // Show the UpdateView as a popup window
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
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected Order object from the ListBox
            var button = (Button)sender;
            var Order = (Order)button.DataContext;

            // Prompt the user for confirmation
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                using (var dbContext = new ApplicationDbContext())
                {
                    dbContext.Orders.Remove(Order);
                    dbContext.SaveChanges();
                }
            }
        }

    }
}
