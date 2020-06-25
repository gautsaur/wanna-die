using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ClassLibrary1;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Deliverable_7
{
    /// <summary>
    /// Interaction logic for frmCharacter.xaml
    /// this is for getting character details
    /// WOW factor
    /// </summary>
    public partial class frmCharacter : Window
    {
        //used static key word for gender, name and title
        public static int femaleClick = 0;
        public static int maleClick = 0;

        frmMain fm;
        public static string gender = "";
        public static string firstName = "";
        public static string lastName = "";
        public static string title = "";


        public frmCharacter()
        {
            InitializeComponent();
            ShowButton();
        }

        /// <summary>
        /// Visible components
        /// </summary>
        private void VisibleComponents()
        {
            txtFirstName.Visibility = Visibility.Visible;
            txtLastName.Visibility = Visibility.Visible;
            txtTitle.Visibility = Visibility.Visible;
            tbGender.Visibility = Visibility.Visible;
            btnCharacterOK.Visibility = Visibility.Visible;
            imgFirstName.Visibility = Visibility.Visible;
            imgLastName.Visibility = Visibility.Visible;
            imgTitle.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Actions when image is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            VisibleComponents();
            gender = "MALE";
            tbGender.Text = "Who are you, boy ?";

            maleClick = 1;
        }

        /// <summary>
        /// Actions when image is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImgFemale_MouseDown(object sender, MouseButtonEventArgs e)
        {
            VisibleComponents();
            gender = "FEMALE";
            tbGender.Text = "Who are you, girl ?";
            femaleClick = 1;
        }

        /// <summary>
        /// Actions when mouse cursor enters image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            MediaPlayer s0 = new MediaPlayer();
            s0.Open(new Uri(@"../../data/button.wav", UriKind.RelativeOrAbsolute));
            s0.Play();
        }

        /// <summary>
        ///  Actions when mouse cursor enters image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImgFemale_MouseEnter(object sender, MouseEventArgs e)
        {
            MediaPlayer s0 = new MediaPlayer();
            s0.Open(new Uri(@"../../data/button.wav", UriKind.RelativeOrAbsolute));
            s0.Play();
        }


        private void BtnCharacterOK_Click(object sender, RoutedEventArgs e)
        {
            //making sure that every detail is accessed
            if (txtFirstName.Text == "" && txtLastName.Text == "" && txtTitle.Text == "")
            {
                MessageBox.Show("Please enter all the details.");
            }

            if (maleClick == 1)
            {
                Game.ResetGame(10, 10);

            }
            if (femaleClick == 1)
            {
                Game.ResetGame(10, 10);
            }
            firstName = txtFirstName.Text;
            lastName = txtLastName.Text;
            title = txtTitle.Text;
        
        fm = new frmMain();
            fm.Show();
            this.Close();
        }


        private void BtnCharacterOK_MouseEnter(object sender, MouseEventArgs e)
        {
            MediaPlayer s0 = new MediaPlayer();
            s0.Open(new Uri(@"../../data/button.wav", UriKind.RelativeOrAbsolute));
            s0.Play();
        }

        private void ShowButton()
        {
            if (txtFirstName.Text != "" && txtLastName.Text != "" && txtTitle.Text != "") btnCharacterOK.IsEnabled = true;

        }

        private void TxtTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowButton();
        }

        private void TxtLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowButton();
        }

        private void TxtFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowButton();
        }
    }
}