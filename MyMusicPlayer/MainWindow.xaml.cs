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
using System.Windows.Threading;
using System.Windows.Media.Animation;
using NAudio;
using NAudio.Wave;
using TagLib;

namespace MyMusicPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public WaveOut waveOut;
        public WaveStream stream;
        public DispatcherTimer timer;
        public PlayList prevPlayList;
        public PlayListWindow playListWindow;
        public PlayListCollection collectionOfPlayLists;
        public float prevVolumeValue = 0;
        public bool IsFirst = true;
        public bool IsRepeat = false;

        public MainWindow()
        {
            InitializeComponent();

            Left = System.Windows.SystemParameters.PrimaryScreenWidth - this.Width;
            Top = 0;

            collectionOfPlayLists = new PlayListCollection("playlists.bin");

            try
            {
                stream = new AudioFileReader(collectionOfPlayLists.currentPlayItem.Path);
                waveOut = new WaveOut();
                waveOut.Init(stream);

                TextBlockInfo.Text = collectionOfPlayLists.currentPlayItem.ToString();
            }
            catch
            {
                
            }
        }

        public void InitPlayListCollection()
        {
            collectionOfPlayLists = new PlayListCollection();
            collectionOfPlayLists.playLists = new List<PlayList>();
            collectionOfPlayLists.playLists.Add(collectionOfPlayLists.currentPlayList);
            collectionOfPlayLists.currentPlayList = collectionOfPlayLists.currentPlayList;
            collectionOfPlayLists.currentPlayItem = collectionOfPlayLists.currentPlayItem;
        }

        public static String BytesToString(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }

        private void playListView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Hi!!!");
        }

        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (playListWindow != null)
                playListWindow.Close();
            Close();
        }

        private void Image_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            
        }

        public void Image_MouseDown_PauseOrPlay(object sender, MouseButtonEventArgs e)
        {
            if (PlayOrPauseButton.Source == FindResource("PauseButton") as ImageSource)
            {
                PlayOrPauseButton.Source = FindResource("PlayButton") as ImageSource;
                waveOut.Pause();
            }
            else
            {
                if (IsFirst)
                {
                    timer = new System.Windows.Threading.DispatcherTimer();
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += timer_Tick;
                    timer.Start();

                    stream = new AudioFileReader(collectionOfPlayLists.currentPlayItem.Path);
                    waveOut = new WaveOut();
                    waveOut.Init(stream);
                    waveOut.Play();
                    progressOfSong.Maximum = stream.TotalTime.TotalMinutes * 60 + stream.TotalTime.TotalSeconds;
                    progressOfSong.Value = stream.CurrentTime.TotalMinutes * 60 + stream.CurrentTime.TotalSeconds;

                    IsFirst = false;
                }
                else
                {
                    waveOut.Resume();
                }

                PlayOrPauseButton.Source = FindResource("PauseButton") as ImageSource;
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            labelTime.Content = stream.CurrentTime.ToString(@"mm\:ss");
            progressOfSong.Value = stream.CurrentTime.TotalMinutes * 60 + stream.CurrentTime.TotalSeconds;
            if (stream.CurrentTime == stream.TotalTime)
            {
                timer.Stop();
                if (IsRepeat)
                {
                    waveOut.Stop();
                    stream = new AudioFileReader(collectionOfPlayLists.currentPlayItem.Path);
                    waveOut = new WaveOut();
                    waveOut.Init(stream);
                    waveOut.Play();
                    labelTime.Content = "00:00";
                }
                else
                {
                    if (collectionOfPlayLists.currentPlayList.Items.IndexOf(collectionOfPlayLists.currentPlayItem) + 1 == collectionOfPlayLists.currentPlayList.Items.Count)
                    {
                        collectionOfPlayLists.currentPlayItem = collectionOfPlayLists.currentPlayList.Items[0];
                    }
                    else
                    {
                        collectionOfPlayLists.currentPlayItem = collectionOfPlayLists.currentPlayList.Items[collectionOfPlayLists.currentPlayList.Items.IndexOf(collectionOfPlayLists.currentPlayItem) + 1];
                    }
                    TextBlockInfo.Text = collectionOfPlayLists.currentPlayItem.ToString();

                    stream = new AudioFileReader(collectionOfPlayLists.currentPlayItem.Path);
                    waveOut = new WaveOut();
                    waveOut.Init(stream);
                    waveOut.Play();
                    labelTime.Content = "00:00";
                    progressOfSong.Maximum = stream.TotalTime.TotalMinutes * 60 + stream.TotalTime.TotalSeconds;
                    progressOfSong.Value = stream.CurrentTime.TotalMinutes * 60 + stream.CurrentTime.TotalSeconds;
                }
                timer.Start();
            }
        }

       
        private void progressOfSong_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(progressOfSong);
            double percent = point.X / progressOfSong.Width;
            progressOfSong.Value = percent * progressOfSong.Maximum;
            stream.CurrentTime = TimeSpan.FromSeconds(progressOfSong.Value / 2);
            labelTime.Content = stream.CurrentTime.ToString(@"mm\:ss");
        }

        public void Image_Stop_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (waveOut != null)
            {
                waveOut.Stop();
            }
            PlayOrPauseButton.Source = FindResource("PlayButton") as ImageSource;
            progressOfSong.Value = 0;
            if (stream != null)
            {
                stream.CurrentTime = TimeSpan.FromSeconds(0);

                labelTime.Content = stream.CurrentTime.ToString(@"mm\:ss");
            }
            if (timer != null)
            {
                timer.Stop();
            }
            IsFirst = true;
        }

        
        private void Image_MouseDown_Hide(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
            playListWindow.WindowState = System.Windows.WindowState.Minimized;
        }

        private void progressBarVolume_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(progressBarVolume);
            double percent = point.X / progressBarVolume.Width;
            waveOut.Volume = (float)percent;
            progressBarVolume.Value = percent * progressBarVolume.Maximum;
        }

        
        private void Image_MouseDown_Volume(object sender, MouseButtonEventArgs e)
        {
            if (VolumeImg.Source == FindResource("VolumeButton") as ImageSource)
            {
                VolumeImg.Source = FindResource("MuteButton") as ImageSource;
                VolumeImg.OpacityMask = new ImageBrush(FindResource("MuteButton") as ImageSource);
                prevVolumeValue = waveOut.Volume;
                waveOut.Volume = 0;
            }
            else
            {
                VolumeImg.Source = FindResource("VolumeButton") as ImageSource;
                VolumeImg.OpacityMask = new ImageBrush(FindResource("VolumeButton") as ImageSource);
                waveOut.Volume = prevVolumeValue;
            }
            progressBarVolume.Value = waveOut.Volume * 100;
        }

        private void RepeatImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsRepeat)
            {
                IsRepeat = false;
                RepeatImg.Source = FindResource("RepeatButton") as ImageSource;
                RepeatImg.OpacityMask = new ImageBrush(FindResource("RepeatButton") as ImageSource);
            }
            else
            {
                IsRepeat = true;
                RepeatImg.Source = FindResource("NotRefreshButton") as ImageSource;
                RepeatImg.OpacityMask = new ImageBrush(FindResource("NotRefreshButton") as ImageSource);
            }
        }

        private void RandomImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (RandomImg.Source == FindResource("RandomButton") as ImageSource)
            {
                RandomImg.Source = FindResource("NotRandomButton") as ImageSource;
                prevPlayList = new PlayList();
                prevPlayList.Items.AddRange(collectionOfPlayLists.currentPlayList.Items);
                collectionOfPlayLists.currentPlayList.RandomTheList();
            }
            else
            {
                RandomImg.Source = FindResource("RandomButton") as ImageSource;
                collectionOfPlayLists.currentPlayList = prevPlayList;
            }
            if (playListWindow != null)
            {
                playListWindow.playListView.Items.Clear();
                playListWindow.InitPlayListView(collectionOfPlayLists.currentPlayList);
            }
        }

        private void labelPlaylist_MouseDown(object sender, MouseButtonEventArgs e)
        {
            playListWindow = new PlayListWindow();
            playListWindow.Show();
            playListWindow.Left = this.Left;
            playListWindow.Top = this.Top + this.Height + 5;
        }

        private void backImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            timer.Stop();
            waveOut.Stop();

            if (collectionOfPlayLists.currentPlayList.Items.IndexOf(collectionOfPlayLists.currentPlayItem) == 0)
            {
                collectionOfPlayLists.currentPlayItem = collectionOfPlayLists.currentPlayList.Items[collectionOfPlayLists.currentPlayList.Items.Count - 1];
            }
            else
            {
                collectionOfPlayLists.currentPlayItem = collectionOfPlayLists.currentPlayList.Items[collectionOfPlayLists.currentPlayList.Items.IndexOf(collectionOfPlayLists.currentPlayItem) - 1];
            }
            TextBlockInfo.Text = collectionOfPlayLists.currentPlayItem.ToString();

            stream = new AudioFileReader(collectionOfPlayLists.currentPlayItem.Path);
            waveOut = new WaveOut();
            waveOut.Init(stream);
            waveOut.Play();
            labelTime.Content = "00:00";
            progressOfSong.Maximum = stream.TotalTime.TotalMinutes * 60 + stream.TotalTime.TotalSeconds;
            progressOfSong.Value = stream.CurrentTime.TotalMinutes * 60 + stream.CurrentTime.TotalSeconds;
            
            timer.Start();
        }

        public void nextImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (timer != null)
            {
                timer.Stop();
            }
            if (waveOut != null)
            {
                waveOut.Stop();
            }
            if (collectionOfPlayLists.currentPlayList.Items.IndexOf(collectionOfPlayLists.currentPlayItem) + 1 == collectionOfPlayLists.currentPlayList.Items.Count)
            {
                collectionOfPlayLists.currentPlayItem = collectionOfPlayLists.currentPlayList.Items[0];
            }
            else
            {
                collectionOfPlayLists.currentPlayItem = collectionOfPlayLists.currentPlayList.Items[collectionOfPlayLists.currentPlayList.Items.IndexOf(collectionOfPlayLists.currentPlayItem) + 1];
            }
            TextBlockInfo.Text = collectionOfPlayLists.currentPlayItem.ToString();

            stream = new AudioFileReader(collectionOfPlayLists.currentPlayItem.Path);
            waveOut = new WaveOut();
            waveOut.Init(stream);
            waveOut.Play();
            labelTime.Content = "00:00";
            progressOfSong.Maximum = stream.TotalTime.TotalMinutes * 60 + stream.TotalTime.TotalSeconds;
            progressOfSong.Value = stream.CurrentTime.TotalMinutes * 60 + stream.CurrentTime.TotalSeconds;
            if (timer == null)
            {
                timer = new System.Windows.Threading.DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += timer_Tick;
                timer.Start();
            }
            timer.Start();
        }

        private void CloseImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void mainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            collectionOfPlayLists.Save("playlists.bin");
        }

    }
}
