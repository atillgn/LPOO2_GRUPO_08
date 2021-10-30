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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LPOO2_GRUPO_08.ControlUsuario
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public LinearGradientBrush myGradientBrush = new LinearGradientBrush();
            
        public Login()
        {
            InitializeComponent();

            Color color1 = (Color)ColorConverter.ConvertFromString("#FFE63070");
            Color color2 = (Color)ColorConverter.ConvertFromString("#FFFE8704");
            myGradientBrush.StartPoint = new Point(0.1, 0);
            myGradientBrush.EndPoint = new Point(0.9, 1);
            myGradientBrush.GradientStops.Add(new GradientStop(color2, 2.0));
            myGradientBrush.GradientStops.Add(new GradientStop(color1, 0.0));

            rUser.Fill = myGradientBrush;
            rPass.Fill = myGradientBrush;
        }


        private void txtPass_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            rPass.Fill = new SolidColorBrush(Color.FromRgb(28, 191, 255));
            rPass.Height = 3;
            lblPass.Foreground = new SolidColorBrush(Color.FromRgb(28, 191, 255));
        }

        private void txtPass_LostFocus(object sender, RoutedEventArgs e)
        {
            rPass.Fill = myGradientBrush;
            rPass.Height = 2;
            lblPass.Foreground = Brushes.White;
        }

        private void txtUser_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            rUser.Fill = new SolidColorBrush(Color.FromRgb(28, 191, 255));
            rUser.Height = 3;
            lblUser.Foreground = new SolidColorBrush(Color.FromRgb(28, 191, 255));
        }

        private void txtUser_LostFocus(object sender, RoutedEventArgs e)
        {
            rUser.Fill = myGradientBrush;
            rUser.Height = 2;
            lblUser.Foreground = Brushes.White;
        }
    }
}
