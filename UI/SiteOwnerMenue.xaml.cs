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
using BL;
using BE;

namespace UI
{
    /// <summary>
    /// Interaction logic for SiteOwnerMenue.xaml
    /// </summary>
    public partial class SiteOwnerMenue : Window
    {
        public SiteOwnerMenue()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            IBL bl = BL.FactoriaIBL.getInstance();
            cbSortByAreaHU.ItemsSource = Enum.GetValues(typeof(Area));
            cbSortByAreaGR.ItemsSource = Enum.GetValues(typeof(Area));
            List<HostingUnit> HostingUnits = bl.AllHostingUnits();
            foreach(HostingUnit unit in HostingUnits)
            {
                dgHostingUnits.Items.Add(unit);
            }

            List<Order> Orders = bl.AllOrders();
            foreach (Order order in Orders)
            {
                dgOrders.Items.Add(order);
            }

            List<GuestRequest> GuestRequests = bl.AllGuestRequest();
            foreach (GuestRequest request in GuestRequests)
            {
                dgGuesrRequests.Items.Add(request);
            }
            List<Host> hosts = bl.AllHosts();
            foreach(Host o in hosts)
            {
                dgHosts.Items.Add(o);
            }
            
        }

        private void HostingUnitDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (dgHostingUnits.SelectedIndex == -1)
                return;
            HostingUnit unit = dgHostingUnits.SelectedItem as HostingUnit;
            try
            {
                BL.FactoriaIBL.getInstance().removeHostingUnit(unit);
                dgHostingUnits.Items.Remove(dgHostingUnits.SelectedItem);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "הפעולה נכשלה", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            

        }

        
        private void SortByAreaHUb_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<IGrouping<Area, HostingUnit>> UnitsByArea = BL.FactoriaIBL.getInstance().UnitsByArea();
            dgHostingUnits.Items.Clear();
            foreach (HostingUnit unit in UnitsByArea)
            {
                dgHostingUnits.Items.Add(unit);
            }
        }

        private void SortByNumberVacationersGRb_Click(object sender, RoutedEventArgs e)
        {
            dgGuesrRequests.Items.Clear();
            IEnumerable<IGrouping<int, GuestRequest>> guestRequests = BL.FactoriaIBL.getInstance().requestsByNumberVacationers();
            foreach(var item1 in guestRequests)
            {
                foreach(var item2 in item1)
                {
                    dgGuesrRequests.Items.Add(item2);
                }
            }
        }

        
        

        private void GuestRequestDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (dgGuesrRequests.SelectedIndex == -1)
                return;
            try
            {
                GuestRequest req = dgGuesrRequests.SelectedItem as GuestRequest;
                BL.FactoriaIBL.getInstance().removeGuestRequest(req);
                dgGuesrRequests.Items.Remove(dgGuesrRequests.SelectedItems);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "מחיקה נכשלה", MessageBoxButton.OK, MessageBoxImage.Error);
            }

           
        }

        private void CbSortByAreaHU_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dgHostingUnits.Items.Clear();
            Area selectedArea = (Area)Enum.Parse(typeof(Area), cbSortByAreaHU.SelectedItem.ToString());
            if(selectedArea == Area.All)
            {
                
                List<HostingUnit> HostingUnits = BL.FactoriaIBL.getInstance().AllHostingUnits();
                foreach (HostingUnit unit in HostingUnits)
                {
                    dgHostingUnits.Items.Add(unit);
                }
            }
            if(selectedArea == Area.Center)
            {
                dgHostingUnits.Items.Clear();
                IEnumerable<IGrouping<Area, HostingUnit>> UnitsByArea = BL.FactoriaIBL.getInstance().UnitsByArea();
                foreach(var UnitGroup in UnitsByArea)
                {
                    if(UnitGroup.Key==Area.Center)
                    {
                        foreach(var unit in UnitGroup)
                        {
                            dgHostingUnits.Items.Add(unit);
                        }
                    }
                }
            }
            if (selectedArea == Area.Jerusalem)
            {
                dgHostingUnits.Items.Clear();
                IEnumerable<IGrouping<Area, HostingUnit>> UnitsByArea = BL.FactoriaIBL.getInstance().UnitsByArea();
                foreach (var UnitGroup in UnitsByArea)
                {
                    if (UnitGroup.Key == Area.Jerusalem)
                    {
                        foreach (var unit in UnitGroup)
                        {
                            dgHostingUnits.Items.Add(unit);
                        }
                    }
                }
            }
            if (selectedArea == Area.North)
            {
                dgHostingUnits.Items.Clear();
                IEnumerable<IGrouping<Area, HostingUnit>> UnitsByArea = BL.FactoriaIBL.getInstance().UnitsByArea();
                foreach (var UnitGroup in UnitsByArea)
                {
                    if (UnitGroup.Key == Area.North)
                    {
                        foreach (var unit in UnitGroup)
                        {
                            dgHostingUnits.Items.Add(unit);
                        }
                    }
                }
            }
            if (selectedArea == Area.South)
            {
                dgHostingUnits.Items.Clear();
                IEnumerable<IGrouping<Area, HostingUnit>> UnitsByArea = BL.FactoriaIBL.getInstance().UnitsByArea();
                foreach (var UnitGroup in UnitsByArea)
                {
                    if (UnitGroup.Key == Area.South)
                    {
                        foreach (var unit in UnitGroup)
                        {
                            dgHostingUnits.Items.Add(unit);
                        }
                    }
                }
            }
        }

        private void CbSortByAreaGR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Area selectedArea = (Area)Enum.Parse(typeof(Area), cbSortByAreaGR.SelectedItem.ToString());
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
                dgGuesrRequests.Items.Clear();
                IEnumerable<IGrouping<Area, GuestRequest>> requestsByArea = BL.FactoriaIBL.getInstance().requestsByArea();
                foreach (var requestGroup in requestsByArea)
                {
                    if (requestGroup.Key == Area.Center)
                    {
                        foreach (var r in requestGroup)
                        {
                            dgHostingUnits.Items.Add(r);
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
                            dgHostingUnits.Items.Add(r);
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
                            dgHostingUnits.Items.Add(r);
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
                            dgHostingUnits.Items.Add(r);
                        }
                    }
                }
            }

        }
    }
}
