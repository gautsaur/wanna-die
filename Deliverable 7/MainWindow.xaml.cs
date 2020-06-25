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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using ClassLibrary1;
using System.IO;
using Microsoft.Win32;
using System.Runtime.Serialization.Formatters.Binary;


namespace Deliverable_7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MediaPlayer s1 = new MediaPlayer();
        public MainWindow()
        {
            InitializeComponent();
            PlayBackgroundMusic();

        }

        public void PlayBackgroundMusic()
        {
            s1.Open(new Uri("../../data/Phantom.wav", UriKind.RelativeOrAbsolute));
            s1.Play();
        }

        private void ImgNewGame_MouseDown(object sender, MouseButtonEventArgs e)
        {
            s1.Stop();
            MediaPlayer s3 = new MediaPlayer();
            s3.Open(new Uri(@"../../data/laughter.wav", UriKind.RelativeOrAbsolute));
            s3.Play();
            DoubleAnimation dblAni = new DoubleAnimation(0, TimeSpan.FromSeconds(3));
            dblAni.Completed += DblAni_Completed;
            BeginAnimation(MainWindow.OpacityProperty, dblAni);

        }

        private void DblAni_Completed(object sender, EventArgs e)
        {
            frmInfo fm = new frmInfo();
            fm.Show();
            this.Close();
        }

        private void ImgExitGame_MouseDown(object sender, MouseButtonEventArgs e)
        {
            frmExitDialog f = new frmExitDialog();
            f.ShowDialog();
        }

        #region MouseEnters
        private void ImgNewGame_MouseEnter(object sender, MouseEventArgs e)
        {
            MediaPlayer s0 = new MediaPlayer();
            s0.Open(new Uri(@"../../data/button.wav", UriKind.RelativeOrAbsolute));
            s0.Play();
        }

        private void ImgExitGame_MouseEnter(object sender, MouseEventArgs e)
        {
            MediaPlayer s0 = new MediaPlayer();
            s0.Open(new Uri(@"../../data/button.wav", UriKind.RelativeOrAbsolute));
            s0.Play();
        }

        private void ImgOptions_MouseEnter(object sender, MouseEventArgs e)
        {
            MediaPlayer s0 = new MediaPlayer();
            s0.Open(new Uri(@"../../data/button.wav", UriKind.RelativeOrAbsolute));
            s0.Play();
        }

        private void ImgLoadGame_MouseEnter(object sender, MouseEventArgs e)
        {
            MediaPlayer s0 = new MediaPlayer();
            s0.Open(new Uri(@"../../data/button.wav", UriKind.RelativeOrAbsolute));
            s0.Play();
        }
        #endregion

        private void ImgLoadGame_MouseDown(object sender, MouseButtonEventArgs e)
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
                    frmMain frm = new frmMain();
                    frm.ShowDialog();
                    this.Close();
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
