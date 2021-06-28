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

namespace UI
{
    /// <summary>
    /// Interaction logic for SiteOwnerMenue.xaml
    /// </summary>
    public partial class SiteOwnersingin : Window
    {
        public SiteOwnersingin()
        {
            InitializeComponent();
        }

        private void OK_button_Click(object sender, RoutedEventArgs e)
        {
            IBL BL_Instance = BL.FactoriaIBL.getInstance();
            try
            {
                BL_Instance.IsOwner(tbUserName.Text, tbPassword.Password);
                SiteOwnerMenue window = new SiteOwnerMenue();
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "!הכניסה נכשלה", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                tbUserName.Clear();
                tbPassword.Clear();
            }
        }
    }
}
