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

namespace WpfApp.MVVM.View
{
    /// <summary>
    /// Represents a user control for updating a room.
    /// </summary>
    public partial class RoomUpdateView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoomUpdateView"/> class.
        /// </summary>
        public RoomUpdateView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Closes the RoomUpdateView popup.
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
        /// Updates the room information and saves the changes to the database.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void Save(object sender, RoutedEventArgs e)
        {
            var room = DataContext as Room;
            room.Standard = ComboBoxStandard.SelectionBoxItem.ToString();

            using (var dbContext = new ApplicationDbContext())
            {
                dbContext.Rooms.Update(room);
                dbContext.SaveChanges();
            }

            Discard(sender, e);
        }
    }

}
