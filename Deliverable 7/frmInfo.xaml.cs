using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using ClassLibrary1;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Deliverable_7
{

        public partial class frmInfo : Window
        {
            /// <summary>
            /// Informs the player about the rules
            /// WOW FACTOR
            /// </summary>
            public frmInfo()
            {
                InitializeComponent();
                MediaPlayer s3 = new MediaPlayer();
                s3.Open(new Uri(@"../../data/intro.wav", UriKind.RelativeOrAbsolute));
                s3.Play();
            }

            private void BtnYes_MouseEnter(object sender, MouseEventArgs e)
            {
                MediaPlayer s0 = new MediaPlayer();
                s0.Open(new Uri(@"../../data/button.wav", UriKind.RelativeOrAbsolute));
                s0.Play();

            }

            private void BtnYes_Click(object sender, RoutedEventArgs e)
            {
                frmCharacter fm = new frmCharacter();
                fm.Show();
                this.Close();

            }
        }
    }