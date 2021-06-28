using BE;
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

namespace UI
{
    /// <summary>
    /// Interaction logic for AddHostingUnitWindow.xaml
    /// </summary>
    public partial class AddHostingUnitWindow : Window
    {
        
        HostingUnit MyHostingUnit = null;
        private Host myHost = null;
        public Host MyHost
        {
            set { myHost = value; }
        }
        public AddHostingUnitWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            cbArea.ItemsSource = Enum.GetValues(typeof(Area));
            cbVacationType.ItemsSource = Enum.GetValues(typeof(TypeOfVication));
            
            
        }

        private void CreateHostingUnit_Click(object sender, RoutedEventArgs e)
        {
            nameEmptyErrorMessege.Visibility = Visibility.Hidden;
            areaEmptyErrorMessege.Visibility = Visibility.Hidden;
            typeEmptyErrorMessege.Visibility = Visibility.Hidden;
            priceEmptyErrorMessege.Visibility = Visibility.Hidden;
            priceValueErrorMessege.Visibility = Visibility.Hidden;
            adultsEmptyErrorMessege.Visibility = Visibility.Hidden;
            adultsValueErrorMessege.Visibility = Visibility.Hidden;
            childrensEmptyErrorMessege.Visibility = Visibility.Hidden;
            childrensValueErrorMessege.Visibility = Visibility.Hidden;
            if (!tbHostingUnitName.Text.Any())
                nameEmptyErrorMessege.Visibility = Visibility.Visible;
            if (cbArea.SelectedIndex == -1)
                areaEmptyErrorMessege.Visibility = Visibility.Visible;
            if(cbVacationType.SelectedIndex==-1)
                typeEmptyErrorMessege.Visibility = Visibility.Visible;
            if (!tbPrice.Text.Any())
                priceEmptyErrorMessege.Visibility = Visibility.Visible;
            else
            {
                try
                {
                    Convert.ToDouble(tbPrice.Text);
                }
                catch
                {
                    priceValueErrorMessege.Visibility = Visibility.Visible;
                }
            }
            if (!tbMaxAdults.Text.Any())
                adultsEmptyErrorMessege.Visibility = Visibility.Visible;
            else
            {
                try
                {
                    Convert.ToInt32(tbMaxAdults.Text);
                }
                catch
                {
                    adultsValueErrorMessege.Visibility = Visibility.Visible;
                }
            }
            if (!tbMaxChildrens.Text.Any())
                childrensEmptyErrorMessege.Visibility = Visibility.Visible;
            else
            {
                try
                {
                    Convert.ToInt32(tbMaxChildrens.Text);
                }
                catch
                {
                    childrensValueErrorMessege.Visibility = Visibility.Visible;
                }
            }

            if (tbHostingUnitName.Text.Any() && cbArea.SelectedIndex != -1 && cbVacationType.SelectedIndex != -1
                && tbPrice.Text.Any() && tbMaxAdults.Text.Any() && tbMaxChildrens.Text.Any())
            {
                MyHostingUnit = new HostingUnit
                {
                    Owner = myHost,
                    Jacuzzi = Convert.ToBoolean(cbJaccuzzi.IsChecked),
                    Pool = Convert.ToBoolean(cbPool.IsChecked),
                    Garden = Convert.ToBoolean(cbGarden.IsChecked),
                    ChildrensAttractions = Convert.ToBoolean(cbAttractions.IsChecked),
                    HostingUnitName = tbHostingUnitName.Text,
                    maxAdults = Convert.ToInt32(tbMaxAdults.Text),
                    maxChildrens = Convert.ToInt32(tbMaxChildrens.Text),
                    PricePerNight = Convert.ToInt64(tbPrice.Text),
                    Type = (TypeOfVication)Enum.Parse(typeof(TypeOfVication), cbVacationType.SelectedItem.ToString()),
                    Area=(Area)Enum.Parse(typeof(Area), cbArea.SelectedItem.ToString())
                };
                try
                {
                    BL.FactoriaIBL.getInstance().CreateHostingUnit(MyHostingUnit);
                   
                    DialogResult = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "יצירת יחידת אירוח נדחתה"
                      , MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            } 
        }

        
    }
}
