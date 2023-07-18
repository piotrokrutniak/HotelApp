using Persistence.Context;
using Persistence.Seeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            ApplicationDbContext context = new();

            if(!context.Customers.Any())
            {
                SeedDefaultCustomers.Seed(context);
            }

            if(!context.Rooms.Any())
            {
                SeedDefaultRooms.Seed(context);
            }

            if (!context.Orders.Any())
            {
                SeedDefaultOrders.Seed(context);
            }


            InitializeComponent();
        }
    }
}
