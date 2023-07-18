using Core.Models;
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
    /// Represents a view model for the Customer entity.
    /// </summary>
    public class CustomerViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the collection of customers.
        /// </summary>
        public ObservableCollection<Customer> Customers { get; set; }

        /// <summary>
        /// Initializes a new instance of the CustomerViewModel class.
        /// </summary>
        public CustomerViewModel()
        {
            using (var context = new ApplicationDbContext())
            {
                Customers = new ObservableCollection<Customer>(context.Customers);
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
