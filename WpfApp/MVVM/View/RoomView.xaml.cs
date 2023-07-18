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
        /// <summary>
        /// Initializes a new instance of the <see cref="RoomView"/> class.
        /// </summary>
        public RoomView()
        {
            InitializeComponent();
            DataContext = new RoomViewModel();
        }

        /// <summary>
        /// Creates an instance of the RoomAddView and shows it as a popup window.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            RoomAddView updateView = new RoomAddView();

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
        /// Creates an instance of the RoomUpdateView and shows it as a popup window.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var room = (Room)button.DataContext;

            RoomUpdateView updateView = new RoomUpdateView();
            updateView.DataContext = room;

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
        /// Deletes the selected room from the database.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var room = (Room)button.DataContext;

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                using (var dbContext = new ApplicationDbContext())
                {
                    dbContext.Rooms.Remove(room);
                    dbContext.SaveChanges();
                }
            }
        }
    }

}
