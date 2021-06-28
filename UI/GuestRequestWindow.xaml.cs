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
    /// Interaction logic for GuestRequestWindow.xaml
    /// </summary>
    public partial class GuestRequestWindow : Window
    {
        private GuestRequest guestRequest = null;
        public GuestRequest GuestRequest
        {
            get => guestRequest;
        }
        public GuestRequestWindow()
        {

            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            cbArea.ItemsSource = Enum.GetValues(typeof(Area));
            cbVacationType.ItemsSource = Enum.GetValues(typeof(TypeOfVication));
        }
        private bool IsValidInput()
        {
            bool IsValid = true;
            ErrorEmptyPrivateName.Visibility = Visibility.Hidden;
            ErrorEmptyFamilyName.Visibility = Visibility.Hidden;
            ErrorEmptyMail.Visibility = Visibility.Hidden;
            ErrorEmptyArea.Visibility = Visibility.Hidden;
            ErrorEmptyType.Visibility = Visibility.Hidden;
            ErrorEmptyChildrens.Visibility = Visibility.Hidden;
            ErrorEmptyAdults.Visibility = Visibility.Hidden;
            ErrorEmptyEntryDate.Visibility = Visibility.Hidden;
            ErrorEmptyReleasDate.Visibility = Visibility.Hidden;
            ErrorContentMail.Visibility = Visibility.Hidden;
            if (!tbPrivateName.Text.Any())
            {
                IsValid = false;
                ErrorEmptyPrivateName.Visibility = Visibility.Visible;
            }
            if (!tbFamilyName.Text.Any())
            {
                ErrorEmptyFamilyName.Visibility = Visibility.Visible;
                IsValid = false;
            }
            if (!tbMailAddress.Text.Any())
            {
                ErrorEmptyMail.Visibility = Visibility.Visible;
                IsValid = false;
            }
            if (tbMailAddress.Text.Any() && !BL.FactoriaIBL.getInstance().CheckMailAddress(tbMailAddress.Text))
            {
                ErrorContentMail.Visibility = Visibility.Visible;
                IsValid = false;
            }
            if (cbArea.SelectedIndex == -1)
            {
                ErrorEmptyArea.Visibility = Visibility.Visible;
                IsValid = false;
            }
            if (cbVacationType.SelectedIndex == -1)
            {
                ErrorEmptyType.Visibility = Visibility.Visible;
                IsValid = false;
            }
            if (!Adults.Text.Any())
            {
                ErrorEmptyAdults.Visibility = Visibility.Visible;
                IsValid = false;
            }
            if (!Childrens.Text.Any())
            {
                ErrorEmptyChildrens.Visibility = Visibility.Visible;
                IsValid = false;
            }
            if (!EntryDate.SelectedDate.HasValue)
            {
                ErrorEmptyEntryDate.Visibility = Visibility.Visible;
                IsValid = false;
            }
            if (!ReleaseDate.SelectedDate.HasValue)
            {
                ErrorEmptyReleasDate.Visibility = Visibility.Visible;
                IsValid = false;
            }
            return IsValid;
        }
        private void OkButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (!IsValidInput())
                return;
            Jacuzzi j = Jacuzzi.possible;
            if (YesJaccuzi.IsChecked == true)
                j = Jacuzzi.Necessary;
            if (NoJaccuzi.IsChecked == true)
                j = Jacuzzi.uninterested;
            Pool p = Pool.possible;
            if (YesPool.IsChecked == true)
                p = Pool.Necessary;
            if (NoPool.IsChecked == true)
                p = Pool.uninterested;
            Garden g = Garden.possible;
            if (YesGarden.IsChecked == true)
                g = Garden.Necessary;
            if (NoGarden.IsChecked == true)
                g = Garden.uninterested;
            ChildrensAttractions ca = ChildrensAttractions.possible;
            if (YesAttractions.IsChecked == true)
                ca = ChildrensAttractions.Necessary;
            if (NoAttractions.IsChecked == true)
                ca = ChildrensAttractions.uninterested;

            guestRequest = new GuestRequest
            {
                Area = (Area)Enum.Parse(typeof(Area), cbArea.SelectedItem.ToString()),
                Type = (TypeOfVication)Enum.Parse(typeof(TypeOfVication), cbVacationType.SelectedItem.ToString()),
                EntryDate = Convert.ToDateTime(EntryDate.SelectedDate),
                ReleaseDate = Convert.ToDateTime(ReleaseDate.SelectedDate),
                Adults = Convert.ToInt32(Adults.Text),
                Children = Convert.ToInt32(Childrens.Text),
                Jacuzzi = j,
                Pool = p,
                Garden = g,
                ChildrensAttractions = ca,
                RegistrationDate=DateTime.Today,
                PrivateName=tbPrivateName.Text,
                FamilyName=tbFamilyName.Text,
                MailAddress=tbMailAddress.Text
            };
            try
            {
                BL.FactoriaIBL.getInstance().CreateGuestRequest(guestRequest);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "יצירת בקשת אירוח נדחתה",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        
    }
}
