using Core.Models;
using FluentAssertions;
using Microsoft.IdentityModel.Tokens;
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
    /// Represents a user control for adding a new room.
    /// </summary>
    public partial class RoomAddView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoomAddView"/> class.
        /// </summary>
        public RoomAddView()
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
        /// Adds a new room to the database.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void Add(object sender, RoutedEventArgs e)
        {
            Room room = new Room
            {
                RoomNumber = int.Parse(RoomNumberTextBox.Text),
                Floor = int.Parse(FloorTextBox.Text),
                Price = decimal.Parse(PriceTextBox.Text),
                Standard = ComboBoxStandard.SelectionBoxItem.ToString()
            };

            if (ValidateRoom(room))
            {
                using (var dbContext = new ApplicationDbContext())
                {
                    dbContext.Rooms.Add(room);
                    dbContext.SaveChanges();
                }

                Discard(sender, e);
            }
        }

        /// <summary>
        /// Validates the room.
        /// </summary>
        /// <param name="room">The room object to validate.</param>
        /// <returns>True if the room is valid, false otherwise.</returns>
        private bool ValidateRoom(Room room)
        {
            bool roomNumber = room.RoomNumber > 0;
            bool price = room.Price > 0;
            bool standard = !string.IsNullOrEmpty(room.Standard);

            return roomNumber && price && standard;
        }
    }

}
