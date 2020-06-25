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

namespace Deliverable_7
{
    /// <summary>
    /// Interaction logic for frmExitDialog.xaml
    /// this is for checking if user really wants to exit
    /// WOW FACTOR
    /// </summary>
    public partial class frmExitDialog : Window
    {
        public frmExitDialog()
        {
            InitializeComponent();
        }

        //closes application
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnNo_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #region MouseEnters
        private void BtnNo_MouseEnter(object sender, MouseEventArgs e)
        {
            MediaPlayer s0 = new MediaPlayer();
            s0.Open(new Uri(@"buttonSelect.wav", UriKind.RelativeOrAbsolute));
            s0.Play();
        }

        private void BtnYes_MouseEnter(object sender, MouseEventArgs e)
        {
            MediaPlayer s0 = new MediaPlayer();
            s0.Open(new Uri(@"buttonSelect.wav", UriKind.RelativeOrAbsolute));
            s0.Play();
        }

        #endregion


    }


}
