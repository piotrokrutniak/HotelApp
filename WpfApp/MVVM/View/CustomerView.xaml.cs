﻿using Core.Models;
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

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of the UpdateView
            CustomerAddView updateView = new CustomerAddView();

            // Show the UpdateView as a popup window
            Popup popup = new Popup
            {
                Child = updateView,
                Width = updateView.Width,
                Height = updateView.Height,
                PlacementTarget = this,
                Placement = PlacementMode.Center,
                IsOpen = true
            };
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected Customer object from the ListBox
            var button = (Button)sender;
            var customer = (Customer)button.DataContext;

            // Create an instance of the UpdateView
            CustomerUpdateView updateView = new CustomerUpdateView();

            // Set the DataContext of the UpdateView to the selected Customer
            updateView.DataContext = customer;

            // Show the UpdateView as a popup window
            Popup popup = new Popup
            {
                Child = updateView,
                Width = updateView.Width,
                Height = updateView.Height,
                PlacementTarget = this,
                Placement = PlacementMode.Center,
                IsOpen = true
            };
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
                using (var dbContext = new ApplicationDbContext())
                {
                    dbContext.Customers.Remove(customer);
                    dbContext.SaveChanges();
                }
            }
        }

    }
}
