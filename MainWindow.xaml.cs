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
        private DispatcherTimer timer;
        Player player = new Player();
        private Track current = new Track();

        public MainWindow()
        {
            InitializeComponent();
            Left = 0;
            Top = SystemParameters.PrimaryScreenHeight - MainPanel.Height - 30;
            MainPanel.Width = SystemParameters.PrimaryScreenWidth;
            Tracks.Width = SystemParameters.PrimaryScreenWidth - 40;
            
            timer = new DispatcherTimer(/*new TimeSpan(0,0,1),DispatcherPriority.Normal,TimerTick, timer.Dispatcher*/);
            timer.Tick += new EventHandler(TimerTick);
            timer.Interval = new TimeSpan(0, 0, 1); 
            //ShowInTaskbar = false;
            Resources.Add("height", SystemParameters.PrimaryScreenHeight - MainPanel.Height - 30);
            Resources.Add("minheight", SystemParameters.PrimaryScreenHeight - 45);
        }

        private void Tracks_Drop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files == null)
            {
                files = ((string)e.Data.GetData(DataFormats.UnicodeText)).Split(';');
            }
            AddTracks(files);
        }

        private void AddTracks(string[] files)
        {
            foreach (string file in files)
            {
                Track button = new Track() { Background = new ImageBrush() { ImageSource = ((Image)Resources["img"]).Source }, TrackPath = file};
                button.MouseDoubleClick += (MouseButtonEventHandler)PlayTrack;
                button.MouseDoubleClick += (MouseButtonEventHandler)Button_MouseDown;
                Tracks.Children.Add(button);
            }
        }
        
        private void PlayTrack(object sender, MouseEventArgs e)
        {
            
            Track button = (Track) sender;
            if(current == button)
            {
                player.Continue();
            }
            else
            {
                current.Value = 0;
                button.Maximum = player.Play(button.TrackPath) - 2;
                current = button;
            }
            
        }

        private void TimerTick(object sender, EventArgs e)
        {
            current.Value += 1;
            if (current.Value >= current.Maximum)
                PlayTrack(
                    Tracks.Children.IndexOf(current) == Tracks.Children.Count - 1
                        ? Tracks.Children[0]
                        : Tracks.Children[Tracks.Children.IndexOf(current) + 1], (MouseButtonEventArgs) null);
        }

        private void Pan_MouseUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Audio Files(*.wav;*.wave;*.mp3)|*.wav;*.wave;*.mp3";
            dialog.Multiselect = true;
            dialog.ShowDialog();
            AddTracks(dialog.FileNames);
        }

        private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            timer.Start();
        }

        private void Pause(object sender, MouseButtonEventArgs e)
        {
            player.Pause();
            timer.Stop();
            base.OnMouseUp(e);
        }
    }
}
