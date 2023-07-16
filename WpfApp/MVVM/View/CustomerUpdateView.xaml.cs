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
    public partial class CustomerUpdateView : UserControl
    {
        public CustomerUpdateView()
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
        private void Save(object sender, RoutedEventArgs e)
        {
            // Close the popup window
            
        }

    }
}
