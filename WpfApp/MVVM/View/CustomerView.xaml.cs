using Core.Models;
using Persistence.Context;
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
using WpfApp.MVVM.ViewModel;

namespace WpfApp.MVVM.View
{
    /// <summary>
    /// Interaction logic for CustomerView.xaml
    /// </summary>
    public partial class CustomerView : UserControl
    {
        public CustomerView()
        {
            InitializeComponent();
            DataContext = new CustomerViewModel();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected Customer object from the ListBox
            var button = (Button)sender;
            var customer = (Customer)button.DataContext;

            // Open the record edit form or perform other actions
            // ...
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected Customer object from the ListBox
            var button = (Button)sender;
            var customer = (Customer)button.DataContext;

            // Prompt the user for confirmation
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                // TODO: Delete the selected record from the database
                // ...
            }
        }

    }
}
