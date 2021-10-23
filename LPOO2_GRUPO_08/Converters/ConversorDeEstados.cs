using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media;
using System.Windows;

namespace LPOO2_GRUPO_08.Converters
{
    public class ConversorDeEstados : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string estado = "";
            if (value != null)
                estado = value.ToString();
            switch (estado)
            {
                case "Libre":
                    return new SolidColorBrush(Colors.Green);
                case "Reservada":
                    return (SolidColorBrush)new BrushConverter().ConvertFrom("#d2691e");
                case "Ocupada":
                    return new SolidColorBrush(Colors.Red);
                case "Pidiendo":
                    return (SolidColorBrush)new BrushConverter().ConvertFrom("#9932cc");
                case "En espera de comida":
                    return (SolidColorBrush)new BrushConverter().ConvertFrom("#90ee90");
                case "Servidos":
                    return (SolidColorBrush)new BrushConverter().ConvertFrom("#ff6347");
                case "Esperando cuenta":
                    return (SolidColorBrush)new BrushConverter().ConvertFrom("#4169e1");
                case "Pagando":
                    return (SolidColorBrush)new BrushConverter().ConvertFrom("#91ab5c");
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
