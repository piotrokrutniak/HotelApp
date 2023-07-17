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
    /// Interaction logic for RoomUpdateView.xaml
    /// </summary>
    public partial class RoomUpdateView : UserControl
    {
        public RoomUpdateView()
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
        private void Save(object sender, RoutedEventArgs e)
        {
            var room = DataContext as Room;
            room.Standard = ComboBoxStandard.Text;

            using (var dbContext = new ApplicationDbContext())
            {
                dbContext.Rooms.Update(room);
                dbContext.SaveChanges();
            }

            Discard(sender, e);
        }




    }
}
