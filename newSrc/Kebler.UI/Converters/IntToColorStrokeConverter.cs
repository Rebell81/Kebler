﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Kebler.UI.Converters
{
    //0: 'stopped',
    //1: 'check pending',
    //2: 'checking',
    //3: 'download pending',
    //4: 'downloading',
    //5: 'seed pending',
    //6: 'seeding',

    public class IntToColorStrokeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double torrInf)
            {
                return torrInf switch
                {
                    -1 => (SolidColorBrush)new BrushConverter().ConvertFrom("#CA2327"),
                    1 => (SolidColorBrush)new BrushConverter().ConvertFrom("#FFB4B3F1"),
                    2 => (SolidColorBrush)new BrushConverter().ConvertFrom("#B0CB73E1"),
                    0 => (SolidColorBrush)new BrushConverter().ConvertFrom("#E0FF9502"),
                    4 => (SolidColorBrush)new BrushConverter().ConvertFrom("#B01CADF8"),
                    6 => (SolidColorBrush)new BrushConverter().ConvertFrom("#B064DA38"),
                    _ => (SolidColorBrush)new BrushConverter().ConvertFrom("#ffaacc")
                };
            }

            return new object();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}