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
    /// Interaction logic for CustomerUpdateView.xaml
    /// </summary>
    public partial class CustomerAddView : UserControl
    {
        public CustomerAddView()
        {
            InitializeComponent();
        }

        private void Discard(object sender, RoutedEventArgs e)
        {
            // Close the CustomerUpdateView popup
            var popup = this.Parent as Popup;
            if (popup != null)
            {
                popup.IsOpen = false;
            }

        }
        private void Add(object sender, RoutedEventArgs e)
        {
            var firstName = FirstNameTextBox.Text;
            var lastName = LastNameTextBox.Text;
            var loyaltyCard = LoyaltyCardComboBox.SelectedItem as ComboBoxItem;

            if (firstName != null && lastName != null && loyaltyCard != null)
            {
                var customer = new Customer(firstName, lastName);

                using (var dbContext = new ApplicationDbContext())
                {
                    dbContext.Customers.Add(customer);
                    dbContext.SaveChanges();
                }

                Discard(sender, e);
            }
        }
    }
}
