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
using System.Windows.Shapes;
using BE;
using BL;

namespace UI
{
    /// <summary>
    /// Interaction logic for HostMenueWindow.xaml
    /// </summary>
    public partial class HostMenueWindow : Window
    {
        private Host myHost = null;
        public Host MyHost
        {
            get { return myHost; }
            set { myHost = value;
                setDGordersItems();
            }
        }
        public HostMenueWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;

            cbSortArea.ItemsSource = Enum.GetValues(typeof(Area));
            
            
            List<GuestRequest> GuestRequests = BL.FactoriaIBL.getInstance().AllGuestRequest();
            foreach (GuestRequest request in GuestRequests)
            {
                dgGuesrRequests.Items.Add(request);
            }
        }
        private void setDGordersItems()
        {
            try
            {
                List<HostingUnit> HostingUnits = BL.FactoriaIBL.getInstance().unitsByHost(myHost.HostKey);
                foreach (HostingUnit unit in HostingUnits)
                {
                    dgHostingUnits.Items.Add(unit);
                }
                List<Order> Orders = BL.FactoriaIBL.getInstance().OrdersByHost(myHost.HostKey);
                foreach (Order order in Orders)
                {
                    dgOrders.Items.Add(order);
                }
                
            }
            catch { }


        }
        private void AddHostingUnitButton_Click(object sender, RoutedEventArgs e)
        {
            AddHostingUnitWindow addHostingUnitWindow = new AddHostingUnitWindow();
            addHostingUnitWindow.MyHost = myHost;
            addHostingUnitWindow.ShowDialog();
        }

        private void UpdateHostingUnitButton_Click(object sender, RoutedEventArgs e)
        {
            if (dgHostingUnits.SelectedIndex == -1 || dgHostingUnits.SelectedItems.Count > 1)
                return;
            UpdateHostingUnitWindow updateHostingUnitWindow = new UpdateHostingUnitWindow();
            updateHostingUnitWindow.MyHostingUnit = dgHostingUnits.SelectedItem as HostingUnit;
            updateHostingUnitWindow.ShowDialog();
        }




        private void HostingUnitDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (dgHostingUnits.SelectedIndex == -1)
                return;
            try
            {
                HostingUnit unit = dgHostingUnits.SelectedItem as HostingUnit;
                BL.FactoriaIBL.getInstance().removeHostingUnit(unit);
                dgHostingUnits.Items.Remove(dgHostingUnits.SelectedItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "מחיקה נכשלה", MessageBoxButton.OK, MessageBoxImage.Error);

            }

           
        }



        private void AddHostingUnitButton_Click_1(object sender, RoutedEventArgs e)
        {
            AddHostingUnitWindow addHostingUnitWindow = new AddHostingUnitWindow();
            addHostingUnitWindow.MyHost = myHost;
            addHostingUnitWindow.ShowDialog();
            if (addHostingUnitWindow.DialogResult == true)
            {
                dgHostingUnits.Items.Clear();
                List<HostingUnit> HostingUnits = BL.FactoriaIBL.getInstance().unitsByHost(myHost.HostKey);
                foreach (HostingUnit unit in HostingUnits)
                {
                    dgHostingUnits.Items.Add(unit);
                }

            }
        }

        private void UpdateHostingUnitButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (dgHostingUnits.SelectedIndex == -1)
                return;
            UpdateHostingUnitWindow updateHostingUnitWindow = new UpdateHostingUnitWindow();
            updateHostingUnitWindow.MyHostingUnit = dgHostingUnits.SelectedItem as HostingUnit;
            updateHostingUnitWindow.ShowDialog();
            if (updateHostingUnitWindow.DialogResult == true)
            {
                dgHostingUnits.Items.Clear();
                List<HostingUnit> HostingUnits = BL.FactoriaIBL.getInstance().unitsByHost(myHost.HostKey);
                foreach (HostingUnit unit in HostingUnits)
                {
                    dgHostingUnits.Items.Add(unit);
                }
            }
        }

        private void CreateOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (!tbHostingUnitKey.Text.Any() || dgGuesrRequests.SelectedIndex == -1)
                return;
            try
            {
                Convert.ToInt64(tbHostingUnitKey.Text);
            }
            catch
            {
                MessageBox.Show("הכנסת פרטים שגויים. מספר יחידת אירוח אמור להכיל ספרות בלבד", "!הפעולה נכשלה", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                return;
            }
            IBL bl = BL.FactoriaIBL.getInstance();
            long HostingUnitKey = Convert.ToInt64(tbHostingUnitKey.Text);
            List<HostingUnit> hostingUnits = bl.unitsByHost(myHost.HostKey);
            HostingUnit unit = hostingUnits.Find(x => x.HostingUnitKey == HostingUnitKey);
            if(unit==null)
            {
                MessageBox.Show("אתה מנסה ליצור הזמנה ליחידת אירוח שלא קיימת ברשותך", "!הפעולה נכשלה", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                return;
            }
            GuestRequest request = dgGuesrRequests.SelectedItem as GuestRequest;
            try
            {
                bl.CreateOrder(unit, request);
                List<Order> orders =bl.OrdersByHost(myHost.HostKey);
                foreach (Order o in orders)
                {
                    dgOrders.Items.Add(o);
                }
                MessageBox.Show("הזמנה נוצרה בהצלחה", " הזמנה", MessageBoxButton.OK,

                  MessageBoxImage.Asterisk);
                dgGuesrRequests.Items.Clear();
                List<GuestRequest> GuestRequests = BL.FactoriaIBL.getInstance().AllGuestRequest();
                foreach (GuestRequest r in GuestRequests)
                {
                    dgGuesrRequests.Items.Add(r);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "!יצירת הזמנה נכשלה", MessageBoxButton.OK,
                   MessageBoxImage.Error);
            }
            
        }

        private void CloseOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (dgOrders.SelectedIndex == -1 || dgOrders.SelectedItems.Count > 1)
                return;
            Order orderToUpdate = dgOrders.SelectedItem as Order;
            try
            {
                BL.FactoriaIBL.getInstance().updateOrdersStatus(orderToUpdate, Status.ClosedClientResponse);
                dgOrders.Items.Clear();
                List<Order> Orders = BL.FactoriaIBL.getInstance().OrdersByHost(myHost.HostKey);
                foreach (Order order in Orders)
                {
                    dgOrders.Items.Add(order);
                }
                MessageBox.Show("הזמנה נסגרה בהצלחה", "פעולה בוצעה בהצלחה", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "יצירת הזמנה נכשלה", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }

        private void CbSortArea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Area selectedArea = (Area)Enum.Parse(typeof(Area), cbSortArea.SelectedItem.ToString());
            dgGuesrRequests.Items.Clear();
            if (selectedArea == Area.All)
            {
                
                List<GuestRequest> guestRequests = BL.FactoriaIBL.getInstance().AllGuestRequest();
                foreach (GuestRequest request in guestRequests)
                {
                    dgGuesrRequests.Items.Add(request);
                }
            }
            if (selectedArea == Area.Center)
            {
                
                IEnumerable<IGrouping<Area, GuestRequest>> requestsByArea = BL.FactoriaIBL.getInstance().requestsByArea();
                foreach (var requestGroup in requestsByArea)
                {
                    if (requestGroup.Key == Area.Center)
                    {
                        foreach (var r in requestGroup)
                        {
                            dgGuesrRequests.Items.Add(r);
                        }
                    }
                }
            }
            if (selectedArea == Area.Jerusalem)
            {
                dgGuesrRequests.Items.Clear();
                IEnumerable<IGrouping<Area, GuestRequest>> requestsByArea = BL.FactoriaIBL.getInstance().requestsByArea();
                foreach (var requestGroup in requestsByArea)
                {
                    if (requestGroup.Key == Area.Jerusalem)
                    {
                        foreach (var r in requestGroup)
                        {
                            dgGuesrRequests.Items.Add(r);
                        }
                    }
                }
            }
            if (selectedArea == Area.North)
            {
                dgGuesrRequests.Items.Clear();
                IEnumerable<IGrouping<Area, GuestRequest>> requestsByArea = BL.FactoriaIBL.getInstance().requestsByArea();
                foreach (var requestGroup in requestsByArea)
                {
                    if (requestGroup.Key == Area.North)
                    {
                        foreach (var r in requestGroup)
                        {
                            dgGuesrRequests.Items.Add(r);
                        }
                    }
                }
            }
            if (selectedArea == Area.South)
            {
                dgGuesrRequests.Items.Clear();
                IEnumerable<IGrouping<Area, GuestRequest>> requestsByArea = BL.FactoriaIBL.getInstance().requestsByArea();
                foreach (var requestGroup in requestsByArea)
                {
                    if (requestGroup.Key == Area.South)
                    {
                        foreach (var r in requestGroup)
                        {
                            dgGuesrRequests.Items.Add(r);
                        }
                    }
                }
            }

        }

        private void SortByNumberVacationersGRb_Click(object sender, RoutedEventArgs e)
        {
            dgGuesrRequests.Items.Clear();
            IEnumerable<IGrouping<int, GuestRequest>> guestRequests = BL.FactoriaIBL.getInstance().requestsByNumberVacationers();
            foreach (var item1 in guestRequests)
            {
                foreach (var item2 in item1)
                {
                    dgGuesrRequests.Items.Add(item2);
                }
            }
        }

    }
}
