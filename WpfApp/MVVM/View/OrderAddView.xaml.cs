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
    /// Interaction logic for OrderUpdateView.xaml
    /// </summary>
    public partial class OrderAddView : UserControl
    {
        public OrderAddView()
        {
            InitializeComponent();
        }

        private void Discard(object sender, RoutedEventArgs e)
        {
            // Close the OrderUpdateView popup
            var popup = this.Parent as Popup;
            if (popup != null)
            {
                popup.IsOpen = false;
            }

        }
        private void Add(object sender, RoutedEventArgs e)
        {
            Order Order = new()
            {
                CustomerId = int.Parse(ComboBoxStandard.Text),
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

        private bool ValidateOrder(Order Order)
        {
            bool customerId = Order.CustomerId > 0;
            
            return customerId;
        }
    }
}
