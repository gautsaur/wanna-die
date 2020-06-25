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
using ClassLibrary1;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Deliverable_7
{
    /// <summary>
    /// Interaction logic for frmMain.xaml
    /// </summary>
    public partial class frmMain : Window
    {
        public frmMain()
        {
            InitializeComponent();
            Game.ResetGame(10, 10);
            MediaPlayer s3 = new MediaPlayer();
            s3.Open(new Uri(@"yoy.mp3", UriKind.RelativeOrAbsolute));
            s3.Play();
            ShowContent();
        }

        /// <summary>
        /// Shows Map and the details of the map
        /// used Prof.Holmes' code for showing the map
        /// </summary>
        public void ShowContent()
        {
            grdGameBorad.Children.Clear();

            CreateAGrid(grdGameBorad, 10, 10);
            for (int row = 0; row <= Game.Map.Cells.GetUpperBound(0); row++)
            {
                for (int col = 0; col <= Game.Map.Cells.GetUpperBound(1); col++)
                {
                    Button btn = new Button();
                    MapCell mcToCheck = Game.Map.Cells[row, col]; // get the current mapcell
                    TextBlock tbContents = new TextBlock(); // create a textblock to display contents
                    tbContents.TextAlignment = TextAlignment.Center;
                    //mapcell will be black if hasn't been seen
                    if (mcToCheck.HasBeenSeen == false)
                    {
                        tbContents.Background = new SolidColorBrush(Colors.Black);
                    }
                    else if (mcToCheck.HasItem)
                    {
                        if (mcToCheck.Item.GetType() == typeof(Weapon))

                            tbContents.Background = new SolidColorBrush(Colors.Gray);

                        else if (mcToCheck.Item.GetType() == typeof(Potion))
                        {
                            Potion ptn = (Potion)mcToCheck.Item;
                            tbContents.Background = GetColorBrush(ptn);
                        }

                        else if (mcToCheck.Item.GetType() == typeof(Door))
                            tbContents.Background = new SolidColorBrush(Colors.Peru);

                        else if (mcToCheck.Item.GetType() == typeof(DoorKey))
                            tbContents.Background = new SolidColorBrush(Colors.Gold);

                        tbContents.Text = mcToCheck.Item.Name;
                    }
                    else if (mcToCheck.HasMonster)
                    {
                        tbContents.Background = new SolidColorBrush(Colors.DarkRed);
                        tbContents.Foreground = new SolidColorBrush(Colors.White);
                        tbContents.Text = Game.Map.Cells[row, col].Monster.Name(false);
                    }
                    else if (Game.Map.Adventurer.PositionX == col && Game.Map.Adventurer.PositionY == row)
                    {
                        tbContents.Text = Game.Map.Adventurer.Name(true);
                        tbContents.Foreground = new SolidColorBrush(Colors.White);
                        tbContents.Background = new SolidColorBrush(Colors.DarkBlue);
                    }
                    tbContents.TextWrapping = TextWrapping.Wrap;
                    Grid.SetRow(tbContents, row);
                    Grid.SetColumn(tbContents, col);
                    grdGameBorad.Children.Add(tbContents);


                }
            }

            string weapName = "";
            if (Game.Map.Adventurer.HasWeapon == true)
            {
                weapName = Game.Map.Adventurer.Weapon.Name;
            }
            else
            {
                weapName = "NONE";
            }
            //details to be shown

            tbMapDet.Text = String.Format("{5}\r\nMonsters count: {0} \r\n Items count: {1} \r\n Map Discovered: {2} % \r\n Current Location: {3},{4}", Game.Map.CountMonsters, Game.Map.CountItems, Game.Map.MapDiscPercent, Game.Map.Adventurer.PositionX, Game.Map.Adventurer.PositionY, Game.Map.Adventurer.Name(true));
            tbItemDet.Text = String.Format("Max Monster HP: {0} \r\n Min Weapon Damage: {1} \r\n Average Potion Effect: {2} \r\n Weapon: {3}", Game.Map.MostHP, Game.Map.LeastWeaponDamageValue, Game.Map.PotionAVG, weapName);

        }


        /// <summary>
        /// used prof.holmes' code for this
        /// setting the color of potion
        /// </summary>
        /// <param name="ptn"></param>
        /// <returns></returns>
        private static SolidColorBrush GetColorBrush(Potion ptn)
        {
            if (ptn.Color == Potion.Colors.Blue)
                return new SolidColorBrush(Colors.LightBlue);
            else if (ptn.Color == Potion.Colors.Red)
                return new SolidColorBrush(Colors.LightPink);
            else if (ptn.Color == Potion.Colors.Green)
                return new SolidColorBrush(Colors.LightGreen);
            else if (ptn.Color == Potion.Colors.Purple)
                return new SolidColorBrush(Colors.Thistle);
            else
                return new SolidColorBrush(Colors.Black);
        }


        /// <summary>
        /// Creates grid with given rows and cols
        /// </summary>
        /// <param name="gameBorad"> grid</param>
        /// <param name="rows">no. of rows</param>
        /// <param name="cols">no. of cols</param>
        private void CreateAGrid(Grid gameBorad, int rows, int cols)
        {
            gameBorad.RowDefinitions.Clear();
            for (int i = 0; i < rows; i++)
            {
                gameBorad.RowDefinitions.Add(new RowDefinition());
            }
            gameBorad.ColumnDefinitions.Clear();
            for (int i = 0; i < cols; i++)
            {
                gameBorad.ColumnDefinitions.Add(new ColumnDefinition());
            }
            grdGameBorad.Children.Clear();
        }

        /// <summary>
        /// Resets the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            Game.ResetGame(10, 10);
            ShowContent();
        }

        /// <summary>
        /// Assigns the game state if lost
        /// </summary>
        public void AssignGameState()
        {
            if (Game.GameStates == Game.GameState.Lost)
            {
                frmGameOver frm = new frmGameOver();
                frm.ShowDialog();
            }

        }

        /// <summary>
        /// Gets frmItem or frmMonster when hero moves up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUp_Click(object sender, RoutedEventArgs e)
        {
            if (Game.Map.MoveHero(Actor.Direction.Up))
            {

                Window wind = null;

                if (Game.Map.CurrentLocation.HasItem)
                {
                    if (Game.Map.CurrentLocation.Item.GetType() == typeof(Door))
                    {
                        wind = new frmGameOver();
                    }
                    else
                    {
                        wind = new frmItem();
                    }
                }

                else if (Game.Map.CurrentLocation.HasMonster) { wind = new frmMonster(); }
                if (wind != null) wind.ShowDialog();

            }
            ShowContent();
            AssignGameState();
        }

        /// <summary>
        /// Gets frmItem or frmMonster when hero moves left
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLeft_Click(object sender, RoutedEventArgs e)
        {

            if (Game.Map.MoveHero(Actor.Direction.Left))
            {
                Window wind = null;

                if (Game.Map.CurrentLocation.HasItem)
                {
                    if (Game.Map.CurrentLocation.Item.GetType() == typeof(Door))
                    {
                        Game.GameStates = Game.GameState.Won;
                        wind = new frmGameOver();
                    }
                    else
                    {
                        wind = new frmItem();
                    }
                }

                else if (Game.Map.CurrentLocation.HasMonster) { wind = new frmMonster(); }
                if (wind != null) wind.ShowDialog();

            }
            ShowContent();
            AssignGameState();

        }

        /// <summary>
        /// Gets frmItem or frmMonster when hero moves right
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRight_Click(object sender, RoutedEventArgs e)
        {
            if (Game.Map.MoveHero(Actor.Direction.Right))
            {
                Window wind = null;

                if (Game.Map.CurrentLocation.HasItem)
                {
                    if (Game.Map.CurrentLocation.Item.GetType() == typeof(Door))
                    {
                        wind = new frmGameOver();
                    }
                    else
                    {
                        wind = new frmItem();
                    }
                }

                else if (Game.Map.CurrentLocation.HasMonster) { wind = new frmMonster(); }
                if (wind != null) wind.ShowDialog();

            }
            ShowContent();
            AssignGameState();
        }

        /// <summary>
        /// Gets frmItem or frmMonster when hero moves down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDown_Click(object sender, RoutedEventArgs e)
        {
            if (Game.Map.MoveHero(Actor.Direction.Down))
            {
                Window wind = null;

                if (Game.Map.CurrentLocation.HasItem)
                {
                    if (Game.Map.CurrentLocation.Item.GetType() == typeof(Door))
                    {
                        wind = new frmGameOver();
                    }
                    else
                    {
                        wind = new frmItem();
                    }
                }

                else if (Game.Map.CurrentLocation.HasMonster) { wind = new frmMonster(); }
                if (wind != null) wind.ShowDialog();

            }
            ShowContent();
            AssignGameState();
        }

        /// <summary>
        /// Assigns keyboard shortcuts for button clicks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                BtnDown_Click(null, null);
            }
            else if (e.Key == Key.Up)
            {
                BtnUp_Click(null, null);
            }
            else if (e.Key == Key.Right)
            {
                BtnRight_Click(null, null);
            }
            else if (e.Key == Key.Left)
            {
                BtnLeft_Click(null, null);
            }
        }

        /// <summary>
        /// Resets the game 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Game.ResetGame(10, 10);
            ShowContent();
        }

        /// <summary>
        /// Saves the game as a map file when clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saved = new SaveFileDialog();
            saved.Filter = "Map file(*.map)|*.map";
            if (saved.ShowDialog() == true)
            {
                string fileName = saved.FileName;
                BinaryFormatter bin = new BinaryFormatter();
                FileStream file = File.Create(fileName);
                bin.Serialize(file, Game.Map);
                file.Close();
                MessageBox.Show("The game is saved.");
            }
        }

        /// <summary>
        /// Loads the map file when clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog opd = new OpenFileDialog();
            opd.Filter = "Map file(*.map)|*.map";
            if (opd.ShowDialog() == true)
            {
                try
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    FileStream file = new FileStream(opd.FileName, FileMode.Open);
                    Map saved = (Map)bin.Deserialize(file);
                    Game.Map = saved;
                    ShowContent();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    MessageBox.Show("The game is loaded.");
                }
            }
        }
    }
}