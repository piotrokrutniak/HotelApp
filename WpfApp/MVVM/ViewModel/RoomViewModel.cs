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
    public class RoomViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Room> Rooms { get; set; }

        public RoomViewModel()
        {
            using (var context = new ApplicationDbContext())
            {
                Rooms = new ObservableCollection<Room>(context.Rooms);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
