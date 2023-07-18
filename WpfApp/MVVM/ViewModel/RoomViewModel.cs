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
    /// Represents a view model for the Room entity.
    /// </summary>
    public class RoomViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the collection of rooms.
        /// </summary>
        public ObservableCollection<Room> Rooms { get; set; }

        /// <summary>
        /// Initializes a new instance of the RoomViewModel class.
        /// </summary>
        public RoomViewModel()
        {
            using (var context = new ApplicationDbContext())
            {
                Rooms = new ObservableCollection<Room>(context.Rooms);
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
