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
using System.Windows.Media.Animation;

using System.Windows.Shapes;

namespace Deliverable_7
{
    /// <summary>
    /// Interaction logic for frmMonster.xaml
    /// </summary>
    public partial class frmMonster : Window
    {
        public frmMonster()
        {
            InitializeComponent();
            PlayMusic();
            DoubleAnimation dblAni = new DoubleAnimation(0, TimeSpan.FromSeconds(3));
            dblAni.Completed += DblAni_Completed;
            imgGif.BeginAnimation(Image.OpacityProperty, dblAni);
            tbMonster.Text = Game.Map.CurrentLocation.Monster.Name(false) + tbMonster.Text;
            tbHeroName.Text = Game.Map.Adventurer.Name(true);
            tbMonsterName.Text = Game.Map.CurrentLocation.Monster.Name(false);
            HeroAndMonster();
            ChangingData();
        }

        public void HeroAndMonster()
        {
            if (frmCharacter.gender == "MALE")
            {
                if (Game.Map.Adventurer.HasWeapon == false)
                {
                    imgHero.Source = new BitmapImage(new Uri(@"../../Images/mIdle.jpg", UriKind.RelativeOrAbsolute));

                }
                else if (Game.Map.Adventurer.Weapon.Name == "Dagger")
                {
                    imgHero.Source = new BitmapImage(new Uri(@"../../Images/mDagger.jpg", UriKind.RelativeOrAbsolute));

                }
                else if (Game.Map.Adventurer.Weapon.Name == "Wand")
                {
                    imgHero.Source = new BitmapImage(new Uri(@"../../Images/mWand.gif", UriKind.RelativeOrAbsolute));

                }
                else if (Game.Map.Adventurer.Weapon.Name == "Sword")
                {
                    imgHero.Source = new BitmapImage(new Uri(@"../../Images/mSword.png", UriKind.RelativeOrAbsolute));

                }
                else if (Game.Map.Adventurer.Weapon.Name == "Bow-arrow")
                {
                    imgHero.Source = new BitmapImage(new Uri(@"../../Images/mArrow.png", UriKind.RelativeOrAbsolute));

                }

            }
            else if (frmCharacter.gender == "FEMALE")
            {
                if (Game.Map.Adventurer.HasWeapon == false)
                {
                    imgHero.Source = new BitmapImage(new Uri(@"../../Images/feIdle.png", UriKind.RelativeOrAbsolute));

                }
                else if (Game.Map.Adventurer.Weapon.Name == "Dagger")
                {
                    imgHero.Source = new BitmapImage(new Uri(@"../../Images/feDagger.png", UriKind.RelativeOrAbsolute));

                }
                else if (Game.Map.Adventurer.Weapon.Name == "Wand")
                {
                    imgHero.Source = new BitmapImage(new Uri(@"../../Images/feWand.png", UriKind.RelativeOrAbsolute));

                }
                else if (Game.Map.Adventurer.Weapon.Name == "Sword")
                {
                    imgHero.Source = new BitmapImage(new Uri(@"../../Images/feSword.png", UriKind.RelativeOrAbsolute));

                }
                else if (Game.Map.Adventurer.Weapon.Name == "Bow-arrow")
                {
                    imgHero.Source = new BitmapImage(new Uri(@"../../Images/feArrow.png", UriKind.RelativeOrAbsolute));

                }

            }
            if (Game.Map.CurrentLocation.Monster.Name(false) == "Orc") imgMonster.Source = new BitmapImage(new Uri(@"../../Images/orc.png", UriKind.RelativeOrAbsolute));
            if (Game.Map.CurrentLocation.Monster.Name(false) == "Rat") imgMonster.Source = new BitmapImage(new Uri(@"../../Images/rat.png", UriKind.RelativeOrAbsolute));
            if (Game.Map.CurrentLocation.Monster.Name(false) == "Skeleton") imgMonster.Source = new BitmapImage(new Uri(@"../../Images/skudsll.png", UriKind.RelativeOrAbsolute));
            if (Game.Map.CurrentLocation.Monster.Name(false) == "Goblin") imgMonster.Source = new BitmapImage(new Uri(@"../../Images/avatar.png", UriKind.RelativeOrAbsolute));
            if (Game.Map.CurrentLocation.Monster.Name(false) == "Slug") imgMonster.Source = new BitmapImage(new Uri(@"../../Images/slug.png", UriKind.RelativeOrAbsolute));

        }

        public void ChangingData()
        {
            lblHeroHP.Content = Game.Map.Adventurer.CurrentHitPoints.ToString() + " / " + Game.Map.Adventurer.MaximumHitPoints.ToString();
            lblMonsterHP.Content = Game.Map.CurrentLocation.Monster.CurrentHitPoints.ToString() + " / " + Game.Map.CurrentLocation.Monster.MaximumHitPoints.ToString();
        }

        public void PlayMusic()
        {
            MediaPlayer s3 = new MediaPlayer();
            MediaPlayer s4 = new MediaPlayer();
            s4.Open(new Uri(@"boom.wav", UriKind.RelativeOrAbsolute));
            s4.Play();
            s3.Open(new Uri(@"fight.mp3", UriKind.RelativeOrAbsolute));
            s3.Play();

        }

        private void DblAni_Completed(object sender, EventArgs e)
        {
            tbMonster.Visibility = Visibility.Visible;
            pbHeroHealth.Visibility = Visibility.Visible;
            pbMonsterHealth.Visibility = Visibility.Visible;
            tbHeroName.Visibility = Visibility.Visible;
            tbMonsterName.Visibility = Visibility.Visible;
            lblHeroHP.Visibility = Visibility.Visible;
            lblMonsterHP.Visibility = Visibility.Visible;

        }

        private void BtnMonster_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void BtnAttack_Click(object sender, RoutedEventArgs e)
        {
            bool added = (Game.Map.Adventurer + Game.Map.CurrentLocation.Monster);
            pbHeroHealth.Value -= (double)(Game.Map.Adventurer.CurrentHitPoints / Game.Map.Adventurer.MaximumHitPoints) * 100.0;
            pbMonsterHealth.Value -= (double)(Game.Map.CurrentLocation.Monster.CurrentHitPoints / Game.Map.CurrentLocation.Monster.MaximumHitPoints) * 100.0;
            Window wnd = null;
            if (Game.Map.Adventurer.IsAlive == false)
            {
                Game.GameStates = Game.GameState.Lost;
                wnd = new frmGameOver();
                wnd.Show();
                this.Close();
            }
            else if (Game.Map.CurrentLocation.Monster.IsAlive == false)
            {
                Game.Map.Cells[Game.Map.Adventurer.PositionY, Game.Map.Adventurer.PositionX].Monster = null;
                this.Close();
            }
            else
            {
                ChangingData();
            }
        }
    }
}
