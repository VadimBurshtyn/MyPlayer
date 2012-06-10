using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Win32;

namespace MyPlayer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Timer timer;
        Player player = new Player();
        private ImageButton current;
        public MainWindow()
        {
            InitializeComponent();
            Left = 0;
            Top = SystemParameters.PrimaryScreenHeight - MainPanel.Height - 30;
            MainPanel.Width = SystemParameters.PrimaryScreenWidth;
            Tracks.Width = SystemParameters.PrimaryScreenWidth - 40;
            timer = new Timer(TimerTick);
            //ShowInTaskbar = false;
            Resources.Add("height", SystemParameters.PrimaryScreenHeight - MainPanel.Height - 30);
            Resources.Add("minheight", SystemParameters.PrimaryScreenHeight - 45);
        }

        private void Tracks_Drop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            AddTracks(files);
        }

        private void AddTracks(string[] files)
        {
            foreach (string file in files)
            {
                ImageButton button = new ImageButton() { Content = new Image() { Source = (ImageSource)Resources["img"] }, TrackPath = file };
                button.MouseDoubleClick += (MouseButtonEventHandler)PlayTrack;
                Tracks.Children.Add(button);
            }
        }

        private void ControlMouseEnter(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            button.Opacity = 1;
        }

        private void ControlMouseLeave(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            button.Opacity += 0.3;
        }
        private void PlayTrack(object sender, MouseEventArgs e)
        {
            ImageButton button = (ImageButton) sender;
            current = button;
            button.Bar = new ProgressBar();
            player.Play(button.TrackPath);
        }

        private void TimerTick(object sender)
        {
            current.Bar.Value += 1;
        }

        private void Pan_MouseUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Audio Files(*.wav;*.wave;*.mp3)|*.wav;*.wave;*.mp3";
            dialog.Multiselect = true;
            dialog.ShowDialog();
            AddTracks(dialog.FileNames);
        }
    }
}
