using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace MyPlayer
{
    class Track: ProgressBar
    {
        //public ProgressBar Bar;// = new ProgressBar();
        public string TrackPath { get; set; }
        public Track():base()
        {
            Width = 135;
            Height = 130;
            Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF01D328")){Opacity = 0.05};
        }
    }
}
