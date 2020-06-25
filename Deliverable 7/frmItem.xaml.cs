using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ClassLibrary1;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Deliverable_7
{
    /// <summary>
    /// Interaction logic for frmItem.xaml
    /// </summary>
    public partial class frmItem : Window
    {
        public frmItem()
        /// <summary>
        /// When user finds any sort of item in  a map cell
        /// </summary>
      
        {
            InitializeComponent();
            CustomActions();
        }

        /// <summary>
        /// Accesses the item found
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFoundIt_Click(object sender, RoutedEventArgs e)
        {
            Game.Map.CurrentLocation.Item = Game.Map.Adventurer.Apply(Game.Map.CurrentLocation.Item);
            this.Close();
        }

        /// <summary>
        /// Deniess the item found
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeny_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Items specfic actions when the items are found
        /// </summary>
        public void CustomActions()
        {
            if (Game.Map.CurrentLocation.Item.GetType() == typeof(DoorKey))
            {

                tbFoundIt.Text = "COngratulations you've found the key to your escape!! Do you want it??";
                imgItem.Source = new BitmapImage(new Uri(@"../../Images/key.png", UriKind.RelativeOrAbsolute));
            }
            if (Game.Map.CurrentLocation.Item.GetType() == typeof(Weapon))
            {
                if (Game.Map.CurrentLocation.Item.Name == "Dagger") imgItem.Source = new BitmapImage(new Uri(@"../../Images/knife.png", UriKind.RelativeOrAbsolute));

                if (Game.Map.CurrentLocation.Item.Name == "Wand") imgItem.Source = new BitmapImage(new Uri(@"../../Images/magic-wand.png", UriKind.RelativeOrAbsolute));

                if (Game.Map.CurrentLocation.Item.Name == "Sword") imgItem.Source = new BitmapImage(new Uri(@"../../Images/saber.png", UriKind.RelativeOrAbsolute));

                if (Game.Map.CurrentLocation.Item.Name == "Bow-arrow") imgItem.Source = new BitmapImage(new Uri(@"../../Images/archery.png", UriKind.RelativeOrAbsolute));

                tbFoundIt.Text = "You've found the " + Game.Map.CurrentLocation.Item.Name + " !! Do you want it??";
            }
            if (Game.Map.CurrentLocation.Item.GetType() == typeof(Potion))
            {
                if (Game.Map.CurrentLocation.Item.Name == "Angel's Tears") imgItem.Source = new BitmapImage(new Uri(@"../../Images/drop.png", UriKind.RelativeOrAbsolute));

                if (Game.Map.CurrentLocation.Item.Name == "Dragon's Blood") imgItem.Source = new BitmapImage(new Uri(@"../../Images/dragon's blood.png", UriKind.RelativeOrAbsolute));

                if (Game.Map.CurrentLocation.Item.Name == "Elixir of Athens") imgItem.Source = new BitmapImage(new Uri(@"../../Images/elixir.png", UriKind.RelativeOrAbsolute));

                if (Game.Map.CurrentLocation.Item.Name == "Blood wine") imgItem.Source = new BitmapImage(new Uri(@"../../Images/wine.png", UriKind.RelativeOrAbsolute));

                tbFoundIt.Text = "You've found the " + Game.Map.CurrentLocation.Item.Name + " !! Do you want it??";
            }
        }
    }

}
