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
    /// Represents the view for updating customer information.
    /// </summary>
    public partial class CustomerUpdateView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerUpdateView"/> class.
        /// </summary>
        public CustomerUpdateView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Discards any changes made and closes the CustomerUpdateView popup.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
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
        /// Saves the changes made to the customer and closes the CustomerUpdateView popup.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void Save(object sender, RoutedEventArgs e)
        {
            var customer = DataContext as Customer;

            using (var dbContext = new ApplicationDbContext())
            {
                dbContext.Customers.Update(customer);
                dbContext.SaveChanges();
            }

            Discard(sender, e);
        }
    }

}
