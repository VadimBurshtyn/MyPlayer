using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace MyPlayer
{
    class ImageButton: Button
    {
        public ProgressBar Bar;// = new ProgressBar();
        public string TrackPath { get; set; }
        public ImageButton():base(){}
    }
}
