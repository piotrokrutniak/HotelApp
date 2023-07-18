using Core.Models;
using FluentAssertions;
using Microsoft.IdentityModel.Tokens;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for OrderUpdateView.xaml
    /// </summary>
    public partial class OrderAddView : UserControl
    {
        /// <summary>
        /// Gets or sets the collection of customers.
        /// </summary>
        public ObservableCollection<Customer> Customers { get; set; } = new ObservableCollection<Customer>();

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderAddView"/> class.
        /// </summary>
        public OrderAddView()
        {
            InitializeComponent();

            using (var dbContext = new ApplicationDbContext())
            {
                Customers = new ObservableCollection<Customer>(dbContext.Customers.ToList());
            }

            DataContext = this;
        }

        /// <summary>
        /// Handles the click event of the Discard button.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event data.</param>
        private void Discard(object sender, RoutedEventArgs e)
        {
            // Close the OrderUpdateView popup
            var popup = this.Parent as Popup;
            if (popup != null)
            {
                popup.IsOpen = false;
            }
        }

        /// <summary>
        /// Handles the click event of the Add button.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event data.</param>
        private void Add(object sender, RoutedEventArgs e)
        {
            Order Order = new()
            {
                CustomerId = int.Parse(ComboBoxStandard.SelectedValue.ToString()),
                Total = 0.00m
            };

            if (ValidateOrder(Order))
            {
                using (var dbContext = new ApplicationDbContext())
                {
                    dbContext.Orders.Add(Order);
                    dbContext.SaveChanges();
                }

                Discard(sender, e);
            }
        }

        /// <summary>
        /// Validates the order.
        /// </summary>
        /// <param name="Order">The order to validate.</param>
        /// <returns><c>true</c> if the order is valid; otherwise, <c>false</c>.</returns>
        private bool ValidateOrder(Order Order)
        {
            bool customerId = Order.CustomerId > 0;

            return customerId;
        }
    }

}
