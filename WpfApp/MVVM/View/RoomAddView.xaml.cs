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
    /// Interaction logic for RoomUpdateView.xaml
    /// </summary>
    public partial class RoomAddView : UserControl
    {
        public RoomAddView()
        {
            InitializeComponent();
        }

        private void Discard(object sender, RoutedEventArgs e)
        {
            // Close the RoomUpdateView popup
            var popup = this.Parent as Popup;
            if (popup != null)
            {
                popup.IsOpen = false;
            }

        }
        private void Add(object sender, RoutedEventArgs e)
        {
            Room room = new()
            {
                RoomNumber = int.Parse(RoomNumberTextBox.Text),
                Floor = int.Parse(FloorTextBox.Text),
                Price = decimal.Parse(PriceTextBox.Text),
                Standard = ComboBoxStandard.Text
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

        private bool ValidateRoom(Room room)
        {
            bool roomNumber = room.RoomNumber > 0;
            bool price = room.Price > 0; 
            bool standard = !room.Standard.IsNullOrEmpty();
            
            return roomNumber && price && standard;
        }
    }
}
