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
    /// Interaction logic for HostSiInhUpWindow.xaml
    /// </summary>
    public partial class HostSiInhUpWindow : Window
    {
        private Host myHost = null;
        public Host MyHost
        {
            get => myHost;
        }
        public HostSiInhUpWindow()
        {
            InitializeComponent();
            
            try
            {
                cbBanks.ItemsSource = BL.FactoriaIBL.getInstance().AllBanksNames();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            bool isValidDetails = true;
            ErrorEmptyPrivateName.Visibility = Visibility.Hidden;
            ErrorEmptyFamilyName.Visibility = Visibility.Hidden;
            ErrorEmptyId.Visibility = Visibility.Hidden;
            Errorid.Visibility = Visibility.Hidden;
            ErrorEmptyEmail.Visibility = Visibility.Hidden;
            ErrorEmail.Visibility = Visibility.Hidden;
            ErrorEmptyPhoneNumber.Visibility = Visibility.Hidden;
            ErrorAccountNumber.Visibility = Visibility.Hidden;
            ErrorEmptyAccountNumber.Visibility = Visibility.Hidden;
            ErrorEmptyBankName.Visibility = Visibility.Hidden;
            ErrorEmptyBankBranch.Visibility = Visibility.Hidden;
            ErrorPhoneNumber.Visibility = Visibility.Hidden;
            if(!IsCollectionClearance.IsChecked==true)
            {
                MessageBox.Show("כדי לצירוק חשבון מארח בהצלחה עליך לאשר הרשאת חשבון בנק", "יצירת חשבון מארח", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                isValidDetails = false;
            }
            if (!tbPrivateName.Text.Any())
            {
                ErrorEmptyPrivateName.Visibility = Visibility.Visible;
                isValidDetails = false;
            }
            if (!tbFamilyeName.Text.Any())
            {
                ErrorEmptyFamilyName.Visibility = Visibility.Visible;
                isValidDetails = false;
            }
            if (!tbID.Text.Any())
            {
                ErrorEmptyId.Visibility = Visibility.Visible;
                isValidDetails = false;
            }
            else
            {
                try
                {
                    Convert.ToInt64(tbID.Text);
                }
                catch
                {
                    Errorid.Visibility = Visibility.Visible;
                    isValidDetails = false;
                }
            }
            if (!tbMailAddress.Text.Any())
            {
                ErrorEmptyEmail.Visibility = Visibility.Visible;
                isValidDetails = false;
            }
            else if (!BL.FactoriaIBL.getInstance().CheckMailAddress(tbMailAddress.Text))
            {
                ErrorEmail.Visibility = Visibility.Visible;
                isValidDetails = false;
            }
            if (!tbFhoneNumber.Text.Any())
            {
                ErrorEmptyPhoneNumber.Visibility = Visibility.Visible;
                isValidDetails = false;
            }
            else
            {
                try
                {
                    Convert.ToInt64(tbFhoneNumber.Text);
                }
                catch
                {
                    ErrorPhoneNumber.Visibility = Visibility.Visible;
                    isValidDetails = false;
                }
            }
            if (!pbPassword.Password.Any())
            {
                ErrorEmptyPassword.Visibility = Visibility.Visible;
                isValidDetails = false;
            }
            if (!tbBankAccountNumber.Text.Any())
            {
                ErrorEmptyAccountNumber.Visibility = Visibility.Visible;
                isValidDetails = false;
            }
            else
            {
                try
                {
                    Convert.ToInt64(tbBankAccountNumber.Text);
                }
                catch
                {
                    ErrorAccountNumber.Visibility = Visibility.Visible;
                    isValidDetails = false;
                }
            }
            if (cbBanks.SelectedIndex == -1)
            {
                ErrorEmptyBankName.Visibility = Visibility.Visible;
                isValidDetails = false;
            }
            if (cbBankBranches.SelectedIndex == -1)
            {
                ErrorEmptyBankBranch.Visibility = Visibility.Visible;
                isValidDetails = false;
            }
            if (isValidDetails)
            {
                myHost = new Host()
                {
                    PrivateName = tbPrivateName.Text,
                    FamilyName = tbFamilyeName.Text,
                    HostKey = Convert.ToInt64(tbID.Text),
                    Password = pbPassword.Password,
                    FhoneNumber = tbFhoneNumber.Text,
                    MailAddress = tbMailAddress.Text,
                    BankAccountNumber = Convert.ToInt64(tbBankAccountNumber.Text),
                    CollectionClearance = Convert.ToBoolean(IsCollectionClearance.IsChecked),
                    BankBranchDetails = BL.FactoriaIBL.getInstance().FindBranchBank(cbBankBranches.SelectedItem.ToString(), cbBanks.SelectedItem.ToString())
                };

                try
                {
                    BL.FactoriaIBL.getInstance().addHost(myHost);

                    MessageBox.Show("כדי להשלים את יצירת חשבון המארח עליך ליצור לפחות יחידת אירוח אחת", "פרטיך נקלטו במערכת", MessageBoxButton.OK, MessageBoxImage.Information);
                    AddHostingUnitWindow addHostingUnitWindow = new AddHostingUnitWindow();
                    addHostingUnitWindow.MyHost = myHost;
                    addHostingUnitWindow.ShowDialog();
                    if (addHostingUnitWindow.DialogResult == false)
                    {

                        return;
                    }
                    else
                    {
                        
                        DialogResult = true;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "יצירת חשבון מארח נדחתה", MessageBoxButton.OK
                        , MessageBoxImage.Error);
                    return;
                }

                this.Close();
            }
        }
        private void CbBanks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                List<string> a = BL.FactoriaIBL.getInstance().AllBanksBranches(cbBanks.SelectedItem.ToString());

                cbBankBranches.ItemsSource = cbBankBranches.ItemsSource = a;
                cbBankBranches.IsEnabled = true;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "הפעולה נכשלה", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

       
    }
}
