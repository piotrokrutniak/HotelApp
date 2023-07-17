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
    /// Interaction logic for RoomView.xaml
    /// </summary>
    public partial class RoomView : UserControl
    {
        public RoomView()
        {
            InitializeComponent();
            DataContext = new RoomViewModel();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of the UpdateView
            RoomAddView updateView = new RoomAddView();

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

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected Room object from the ListBox
            var button = (Button)sender;
            var Room = (Room)button.DataContext;

            // Create an instance of the UpdateView
            RoomUpdateView updateView = new RoomUpdateView();

            // Set the DataContext of the UpdateView to the selected Room
            updateView.DataContext = Room;

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
            // Get the selected Room object from the ListBox
            var button = (Button)sender;
            var Room = (Room)button.DataContext;

            // Prompt the user for confirmation
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                using (var dbContext = new ApplicationDbContext())
                {
                    dbContext.Rooms.Remove(Room);
                    dbContext.SaveChanges();
                }
            }
        }

    }
}
