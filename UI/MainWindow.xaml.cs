using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BL;
using BE;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            
        }

        
        //private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    if (e.WidthChanged)
        //        this.Width = e.NewSize.Width - 115 * 3 - 70 - 30;
        //}
        private void GuestRequestButto_Click(object sender, RoutedEventArgs e)
        {
            GuestRequestWindow guestRequestWindow = new GuestRequestWindow();
            guestRequestWindow.ShowDialog();
        }

        private void HostSingIn_Click(object sender, RoutedEventArgs e)
        {
            HostSingIn hostSingInWindow = new HostSingIn();
            hostSingInWindow.ShowDialog();
            
        }

        private void OwnerButton_Click(object sender, RoutedEventArgs e)
        {
            SiteOwnersingin siteOwnersinginWindow = new SiteOwnersingin();
            siteOwnersinginWindow.ShowDialog();
        }
    }
}
