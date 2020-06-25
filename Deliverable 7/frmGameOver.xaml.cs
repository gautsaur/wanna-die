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
    /// <summary>
    /// Interaction logic for frmGameOver.xaml
    /// </summary>
    public partial class frmGameOver : Window
    {
        public frmGameOver()
        {
            InitializeComponent();
            tbInfo.TextAlignment = TextAlignment.Center;
            tbInfo.TextWrapping = TextWrapping.Wrap;
            CheckGameState();
            CheckDoor();
        }

        /// <summary>
        /// Checks the game state of the player
        /// </summary>
        public void CheckGameState()
        {
            if (Game.GameStates == Game.GameState.Won)
            {
                tbInfo.Text = "You've won!";
                btnReset.Visibility = Visibility.Visible;
                btnExit.Visibility = Visibility.Visible;

            }
            else if (Game.GameStates == Game.GameState.Lost)
            {
                tbInfo.Text = "You've lost!";
                btnReset.Visibility = Visibility.Visible;
                btnExit.Visibility = Visibility.Visible;

            }
            else if (Game.GameStates == Game.GameState.Running)
            {
                btnOK.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Checks if the door can be opened with the key possessed
        /// </summary>
        public void CheckDoor()
        {
            if (Game.Map.CurrentLocation.HasItem)
            {
                if (Game.Map.CurrentLocation.Item.GetType() == typeof(Door))
                {
                    if (Game.Map.Adventurer.Key != null)
                    {
                        Door d = (Door)Game.Map.CurrentLocation.Item;
                        if (d.isMatch(Game.Map.Adventurer.Key))
                        {
                            Game.GameStates = Game.GameState.Won;
                            tbInfo.Text = "Your key matches!! \r\n You've won!!!";
                            btnReset.Visibility = Visibility.Visible;
                            btnOK.Visibility = Visibility.Collapsed;
                            btnExit.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            tbInfo.Text = "Your key doesnot match!!";
                        }
                    }
                    else
                    {
                        tbInfo.Text = "You have door but not the key ";
                        btnReset.Visibility = Visibility.Collapsed;
                        btnOK.Visibility = Visibility.Visible;
                        btnExit.Visibility = Visibility.Collapsed;
                    }

                }
            }
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Resets the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            frmMain frm = new frmMain();
            frm.ShowDialog();
        }


        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
