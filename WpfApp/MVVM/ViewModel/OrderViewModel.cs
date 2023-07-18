using Core.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.MVVM.ViewModel
{
    /// <summary>
    /// Represents a view model for the Order entity.
    /// </summary>
    public class OrderViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the collection of orders.
        /// </summary>
        public ObservableCollection<Order> Orders { get; set; }

        /// <summary>
        /// Initializes a new instance of the OrderViewModel class.
        /// </summary>
        public OrderViewModel()
        {
            using (var context = new ApplicationDbContext())
            {
                Orders = new ObservableCollection<Order>(context.Orders.Include(x => x.Customer).Include(x => x.Bookings).ThenInclude(x => x.Room));
            }
        }

        /// <summary>
        /// Event that is raised when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event for the specified property name.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
