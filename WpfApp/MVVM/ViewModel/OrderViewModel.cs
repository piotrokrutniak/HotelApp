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
    public class OrderViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Order> Orders { get; set; }

        public OrderViewModel()
        {
            using (var context = new ApplicationDbContext())
            {
                Orders = new ObservableCollection<Order>(context.Orders.Include(x => x.Customer).Include(x => x.Bookings).ThenInclude(x => x.Room));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
