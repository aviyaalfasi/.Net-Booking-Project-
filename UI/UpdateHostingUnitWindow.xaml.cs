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

namespace UI
{
    /// <summary>
    /// Interaction logic for UpdateHostingUnitWindow.xaml
    /// </summary>
    public partial class UpdateHostingUnitWindow : Window
    {
        HostingUnit myHostingUnit = null;
        public HostingUnit MyHostingUnit
        {
            set { myHostingUnit = value;
                setHostingUnitDetails();
            }
        }

        public UpdateHostingUnitWindow()
        {
            InitializeComponent();
            cbArea.ItemsSource = Enum.GetValues(typeof(Area));
            cbVacationType.ItemsSource = Enum.GetValues(typeof(TypeOfVication));


        }
        private void setHostingUnitDetails()
        {
            tbHostingUnitName.Text = myHostingUnit.HostingUnitName;
            tbMaxAdults.Text = Convert.ToString(myHostingUnit.maxAdults);
            tbMaxChildrens.Text = Convert.ToString(myHostingUnit.maxChildrens);
            tbPrice.Text = Convert.ToString(myHostingUnit.PricePerNight);
            cbJaccuzzi.IsChecked = myHostingUnit.Jacuzzi;
            cbPool.IsChecked = myHostingUnit.Pool;
            cbGarden.IsChecked = myHostingUnit.Garden;
            cbAttractions.IsChecked = myHostingUnit.ChildrensAttractions;
            cbArea.SelectedItem = myHostingUnit.Area;
            cbVacationType.SelectedItem = myHostingUnit.Type;
        }
        private void UpdateHostingUnit_Click(object sender, RoutedEventArgs e)
        {
            nameEmptyErrorMessege.Visibility = Visibility.Hidden;
            areaEmptyErrorMessege.Visibility = Visibility.Hidden;
            cbVacationType.Visibility = Visibility.Hidden;
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
            if (cbVacationType.SelectedIndex == -1)
                cbVacationType.Visibility = Visibility.Visible;
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

                myHostingUnit.Jacuzzi = Convert.ToBoolean(cbJaccuzzi.IsChecked);
                myHostingUnit.Pool = Convert.ToBoolean(cbPool.IsChecked);
                myHostingUnit.Garden = Convert.ToBoolean(cbGarden.IsChecked);
                myHostingUnit.ChildrensAttractions = Convert.ToBoolean(cbAttractions.IsChecked);
                myHostingUnit.HostingUnitName = tbHostingUnitName.Text;
                myHostingUnit.maxAdults = Convert.ToInt32(tbMaxAdults.Text);
                myHostingUnit.maxChildrens = Convert.ToInt32(tbMaxChildrens.Text);
                myHostingUnit.PricePerNight = Convert.ToInt32(tbPrice.Text);
                myHostingUnit.Type = (TypeOfVication)Enum.Parse(typeof(TypeOfVication), cbVacationType.SelectedItem.ToString());
               
                try
                {
                    BL.FactoriaIBL.getInstance().UpdateHostingUnit(myHostingUnit);
                    DialogResult = true;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "עדכון יחידת אירוח נדחה"
                      , MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }

       
    }
}
