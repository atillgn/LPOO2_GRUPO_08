using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using ClasesBase;
using System.Data;

namespace LPOO2_GRUPO_08
{
    /// <summary>
    /// Interaction logic for WinMenuAdmin.xaml
    /// </summary>
    public partial class WinMenuAdmin : Window
    {
        MediaPlayer media = new MediaPlayer();
        DispatcherTimer timer;
        bool rep;
        string img;
        DataTable dt;
        Usuario user;

        public WinMenuAdmin()
        {   
            InitializeComponent();
            timer = new DispatcherTimer();
            rep = false;
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += new EventHandler(timer_Tick);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            sldVideo.Value = mediaElm.Position.TotalSeconds;
        }

        private void menuUsuarios_Click(object sender, RoutedEventArgs e)
        {
            Window wWinUsuario = new WinTablaUsuarios();
            wWinUsuario.Show();
            this.Close();
        }

        private void menuArticulos_Click(object sender, RoutedEventArgs e)
        {
            if (rep) mediaElm.Stop();
            Window wWinArticulos = new WinABMArticulos();
            wWinArticulos.Show();
            this.Close();
        }

        private void menuFamilia_Click(object sender, RoutedEventArgs e)
        {
            if (rep) mediaElm.Stop();
            Window wWinFamilia = new WinABMFamilia();
            wWinFamilia.Show();
            this.Close();
        }

        private void menuCategoria_Click(object sender, RoutedEventArgs e)
        {
            if (rep) mediaElm.Stop();
            Window wWinCategoria = new WinABMCategoria();
            wWinCategoria.Show();
            this.Close();
        }

        private void menuUnidades_Click(object sender, RoutedEventArgs e)
        {
            if (rep) mediaElm.Stop();
            Window wWinUnidades = new WinABMUnidadDeMedida();
            wWinUnidades.Show();
            this.Close();
        }

        private void menuHistorial_Click(object sender, RoutedEventArgs e)
        {
            if (rep) mediaElm.Stop();
            Window wWinHistorial = new WinLogAdmin();
            wWinHistorial.Show();
            this.Close();
        }

        private void bntMinimizedScreen_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnMaximizeScreen_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState != WindowState.Maximized)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (rep) mediaElm.Stop();
            Window wWinLogin = new MainWindow();
            wWinLogin.Show();
            this.Close();
        }

        private void titleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dt = TrabajarHistoriaLogin.traerUltimoIDUser();
            user = TrabajarUsuario.buscarUsuarioById((int)dt.Rows[0][0]);
            img = user.Usu_Img;
            Elipse.Fill = new ImageBrush(new BitmapImage(new Uri(@img)));
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            mediaElm.Play();
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            mediaElm.Pause();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            mediaElm.Stop();
        }

        private void sldVol_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElm.Volume = (double)sldVol.Value;
        }

        private void sldVideo_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElm.Position = TimeSpan.FromSeconds(sldVideo.Value);
        }

        private void btnAcerca_Click(object sender, RoutedEventArgs e)
        {
            dpVideo.Visibility = Visibility.Visible;
            //mediaElm.Source = new Uri(@"C:\Users\admin\Downloads\videoplayback.mp4");
            mediaElm.Source = new Uri(@"C:\Users\admin\Documents\Visual Studio 2010\Projects\LPOO2_GRUPO_08\LPOO2_GRUPO_08\Resources\video.mp4");
            mediaElm.LoadedBehavior = MediaState.Manual;
            mediaElm.UnloadedBehavior = MediaState.Manual;
            mediaElm.Volume = (double)sldVol.Value;
            mediaElm.Play();
        }

        private void mediaElm_MediaOpened(object sender, RoutedEventArgs e)
        {
            TimeSpan ts = mediaElm.NaturalDuration.TimeSpan;
            sldVideo.Maximum = ts.TotalSeconds;
            timer.Start();
            rep = true;
        }

        
    }
}
