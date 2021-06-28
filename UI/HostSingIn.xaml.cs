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
    /// Interaction logic for HostSingIn.xaml
    /// </summary>
    public partial class HostSingIn : Window
    {
       
        public HostSingIn()
        {
            InitializeComponent();
        }

        private void SingInButton_Click(object sender, RoutedEventArgs e)
        {
            if(!HostId.Text.Any() || !HostPassword.Password.Any())
            {
                return;
            }
            try
            {
                Convert.ToInt64(HostId.Text);
            }
            catch
            {
                MessageBox.Show("הכנסת פרטים שגויים. מספר תז אמור להכיל ספרות בלבד", "!הכניסה נכשלה", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                return;
            }
            try
            {
                
                Host myHost = BL.FactoriaIBL.getInstance().IsHost(Convert.ToInt64(HostId.Text), HostPassword.Password); 
                HostMenueWindow hostMenueWindow = new HostMenueWindow();
                hostMenueWindow.MyHost = myHost;
                hostMenueWindow.ShowDialog();
                DialogResult = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "!הכניסה נכשלה", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                
                return;
            }
            this.Close();
        }

        private void SinghUpButton_Click(object sender, RoutedEventArgs e)
        {
            HostSiInhUpWindow hostSiInhUpWindow = new HostSiInhUpWindow();
            hostSiInhUpWindow.ShowDialog();
            if(hostSiInhUpWindow.DialogResult==true)
            {
                MessageBox.Show("חשבון המארח שלך נוצר בהצלחה ויחידת האירוח הראשונה שלך נוספה למערכת. לאפשרויות נוספות התחבר לחשבונך", "חשבון מארח נוצר בהצלחה", MessageBoxButton.OK
                        , MessageBoxImage.Asterisk);
            }
        }
    }
}
