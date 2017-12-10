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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyMusicPlayer
{
    /// <summary>
    /// Логика взаимодействия для PlayListWindow.xaml
    /// </summary>
    
    public partial class PlayListWindow : Window
    {
        public bool IsAnimatible { get; set; }
        public MainWindow window;
        TextBlock playListCurrentAnimatable;
        TextBlock playListNextAnimatable;
        TextBlock playListPrevAnimatable;
        TextBlock playListFreeAnimatable;
        
        public PlayListWindow()
        {
            InitializeComponent();
            window = App.Current.MainWindow as MainWindow;
            if (window.collectionOfPlayLists.playLists.Count > 3)
            {
                IsAnimatible = true;
                if (window.collectionOfPlayLists.playLists.IndexOf(window.collectionOfPlayLists.currentPlayList) - 1 < 0)
                {
                    playListPrev.Text = window.collectionOfPlayLists.playLists[window.collectionOfPlayLists.playLists.Count - 1].Name;
                }
                else
                {
                    playListPrev.Text = window.collectionOfPlayLists.playLists[window.collectionOfPlayLists.playLists.IndexOf(window.collectionOfPlayLists.currentPlayList) - 1].Name;
                }
                playListCurrent.Text = window.collectionOfPlayLists.currentPlayList.Name;
                if (window.collectionOfPlayLists.playLists.IndexOf(window.collectionOfPlayLists.currentPlayList) + 1 > window.collectionOfPlayLists.playLists.Count)
                {
                    playListNext.Text = window.collectionOfPlayLists.playLists[0].Name;
                }
                else
                {
                    if (window.collectionOfPlayLists.playLists.IndexOf(window.collectionOfPlayLists.currentPlayList) + 1 >= window.collectionOfPlayLists.playLists.Count)
                    {
                        playListNext.Text = window.collectionOfPlayLists.playLists[0].Name;
                    }
                    else
                    {
                        playListNext.Text = window.collectionOfPlayLists.playLists[window.collectionOfPlayLists.playLists.IndexOf(window.collectionOfPlayLists.currentPlayList) + 1].Name;
                    }
               }
            }
            else
            {
                IsAnimatible = false;
                if (window.collectionOfPlayLists.playLists.Count == 1)
                {
                    playListPrev.Visibility = System.Windows.Visibility.Hidden;
                    playListNext.Visibility = System.Windows.Visibility.Hidden;
                    playListCurrent.Text = window.collectionOfPlayLists.playLists[0].Name;
                }
                else
                    if (window.collectionOfPlayLists.playLists.Count == 2)
                    {
                        playListNext.Visibility = System.Windows.Visibility.Hidden;
                        playListPrev.Text = window.collectionOfPlayLists.playLists[0].Name;
                        playListCurrent.Text = window.collectionOfPlayLists.playLists[1].Name;
                    }
                    else
                    {
                        playListNext.Text = window.collectionOfPlayLists.playLists[2].Name;
                        playListPrev.Text = window.collectionOfPlayLists.playLists[0].Name;
                        playListCurrent.Text = window.collectionOfPlayLists.playLists[1].Name;
                    }
            }
            
            InitPlayListView(window.collectionOfPlayLists.currentPlayList);
        }

        public void InitPlayListView(PlayList playList)
        {
            if (playList.Items.Count != 0)
            {
                string prevFolder = "";
                System.IO.FileInfo file = new System.IO.FileInfo(playList.Items[0].Path);
                prevFolder = file.Directory.Name;
                for (int i = 0; i < playList.Items.Count; i++)
                {
                    if (i == 0 || prevFolder != new System.IO.FileInfo(playList.Items[i].Path).Directory.Name)
                    {
                        prevFolder = new System.IO.FileInfo(playList.Items[i].Path).Directory.Name;
                        ListViewItem itemHeader = new ListViewItem();
                        itemHeader.Height = 48;

                        Grid gridHeader = new Grid();
                        gridHeader.Height = 48;

                        Label labelNameOfFolder = new Label();
                        labelNameOfFolder.Name = "NumberOfTrack";
                        labelNameOfFolder.Content = prevFolder;
                        labelNameOfFolder.FontSize = 19;
                        labelNameOfFolder.Foreground = new SolidColorBrush(Color.FromArgb(255, 191, 53, 23));
                        labelNameOfFolder.Margin = new Thickness(0, 0, 486, 0);

                        gridHeader.Children.Add(labelNameOfFolder);
                        itemHeader.Content = gridHeader;
                        playListView.Items.Add(itemHeader);
                    }

                    ListViewItem item = new ListViewItem();
                    item.Height = 48;

                    Grid grid = new Grid();
                    grid.Height = 48;
                    grid.Margin = new Thickness(20, 0, 0, 0);

                    Label labelNumberOfTrack = new Label();
                    labelNumberOfTrack.Name = "NumberOfTrack";
                    labelNumberOfTrack.Content = i + 1 + ".";
                    labelNumberOfTrack.FontSize = 13.5;
                    labelNumberOfTrack.Foreground = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                    labelNumberOfTrack.Margin = new Thickness(0, 0, 486, 0);

                    Label labelNameOfTrackAndComposer = new Label();
                    labelNameOfTrackAndComposer.Name = "NameOfTrackAndComposer";
                    labelNameOfTrackAndComposer.Content = playList.Items[i].Compositor + " - " + playList.Items[i].Name;
                    labelNameOfTrackAndComposer.FontSize = 13.5;
                    labelNameOfTrackAndComposer.Foreground = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                    labelNameOfTrackAndComposer.Margin = new Thickness(29, 0, 271, 0);

                    Label labelDurationOfTrack = new Label();
                    labelDurationOfTrack.Name = "DurationOfTrack";
                    labelDurationOfTrack.Content = TimeSpan.FromSeconds(playList.Items[i].Seconds).ToString(@"mm\:ss");
                    labelDurationOfTrack.FontSize = 13.5;
                    labelDurationOfTrack.Foreground = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                    labelDurationOfTrack.Margin = new Thickness(350, 0, 126, 0);

                    Label labelExtAndSize = new Label();
                    labelExtAndSize.Name = "ExtAndSize";
                    labelExtAndSize.Content = playList.Items[i].ShortToString();
                    labelExtAndSize.FontSize = 11;
                    labelExtAndSize.Foreground = new SolidColorBrush(Color.FromRgb(169, 169, 169));
                    labelExtAndSize.Margin = new Thickness(0, 23, 486, -23);

                    grid.Children.Add(labelNumberOfTrack);
                    grid.Children.Add(labelNameOfTrackAndComposer);
                    grid.Children.Add(labelDurationOfTrack);
                    grid.Children.Add(labelExtAndSize);

                    item.Content = grid as object;
                    item.MouseDoubleClick += ListViewItem_MouseDown;

                    playListView.Items.Add(item);
                }
            }
            labelCountOfItemsPlayList.Content = playList.Items.Count;
            labelSumTimeOfPlayList.Content = TimeSpan.FromSeconds(playList.TotalTimeInSeconds()).ToString("dd\\:hh\\:mm\\:ss");
            labelSizeOfItemsPlayList.Content = PlayItem.BytesToString(playList.TotalSizeInBytes());
        }

        private void Delete_Button_Click(object sender, MouseButtonEventArgs e)
        {
            window = App.Current.MainWindow as MainWindow;
            int index = IndexOfItemWithoutDirectoryItems();
            if (window.collectionOfPlayLists.currentPlayItem == window.collectionOfPlayLists.currentPlayList.Items[index])
            {
                window.Image_Stop_MouseDown(null, null);

                window.nextImg_MouseDown(null, null);
                window.collectionOfPlayLists.playLists.Where(p => p.Name == playListCurrent.Text).ElementAt(0).Items.Remove(window.collectionOfPlayLists.currentPlayList.Items[index]);

                playListView.Items.RemoveAt(playListView.SelectedIndex);
            }
            else
            {
                window.collectionOfPlayLists.playLists.Where(p => p.Name == playListCurrent.Text).ElementAt(0).Items.RemoveAt(index);
                playListView.Items.RemoveAt(playListView.SelectedIndex);
            }
            playListView.Items.Clear();
            InitPlayListView(window.collectionOfPlayLists.currentPlayList);
        }

        private void ListViewItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            window = App.Current.MainWindow as MainWindow;
            window.Image_Stop_MouseDown(null, null);
            try
            {
                window.collectionOfPlayLists.currentPlayList = window.collectionOfPlayLists.playLists.Where(p => p.Name == playListCurrent.Text).ElementAt(0);
                window.collectionOfPlayLists.currentPlayItem = window.collectionOfPlayLists.currentPlayList.Items.Where(p => (p.Compositor + " - " + p.Name) ==
                                                                                                         ((((playListView.SelectedItem as ListViewItem).Content as Grid).Children.OfType<Label>().Where(i => i.Name == "NameOfTrackAndComposer")).ElementAt(0).Content as string)).ElementAt(0);
                window.stream = new NAudio.Wave.AudioFileReader(window.collectionOfPlayLists.currentPlayItem.Path);
                window.waveOut = new NAudio.Wave.WaveOut();
                window.waveOut.Init(window.stream);

                window.TextBlockInfo.Text = window.collectionOfPlayLists.currentPlayItem.ToString();
            }
            catch
            {
                //window.collectionOfPlayLists.currentPlayList = window.collectionOfPlayLists.playLists.Where(p => p.Name == playListCurrent.Text).ElementAt(0);
                //window.collectionOfPlayLists.currentPlayItem = window.collectionOfPlayLists.currentPlayList.Items.Where(p => (p.Compositor + " - " + p.Name) ==
                //                                                                                        ((((playListView.SelectedItem as ListViewItem).Content as Grid).Children.OfType<Label>().Where(i => i.Name == "NameOfTrackAndComposer")).ElementAt(0).Content as string)).ElementAt(0);
            }
            
            window.TextBlockInfo.Text = window.collectionOfPlayLists.currentPlayItem.ToString();
            
            window.Image_MouseDown_PauseOrPlay(null, null);
        }

        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
        }

        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "   Search Here!";
        }

        private void Add_Button_Click(object sender, MouseButtonEventArgs e)
        {
            window = App.Current.MainWindow as MainWindow;
            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
            dlg.Filter = "(*.mp3)|*.mp3";
            dlg.Multiselect = true;
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PlayItem[] item = new PlayItem[dlg.FileNames.Length];
                int j = 0;
                foreach (var i in dlg.FileNames)
                {
                    item[j] = new PlayItem(TagLib.File.Create(i));
                    j++;
                }
                window.collectionOfPlayLists.playLists.Where(p => p.Name == playListCurrent.Text).ElementAt(0).Items.AddRange(item);
                window.collectionOfPlayLists.currentPlayList = window.collectionOfPlayLists.playLists.Where(p => p.Name == playListCurrent.Text).ElementAt(0);
                window.collectionOfPlayLists.currentPlayItem = window.collectionOfPlayLists.currentPlayList.Items[0];
            
                playListView.Items.Clear();
                InitPlayListView(window.collectionOfPlayLists.playLists.Where(p => p.Name == playListCurrent.Text).ElementAt(0));
            }
        }

        private void playListPrev_MouseDown(object sender, MouseButtonEventArgs e)
        {
            window = App.Current.MainWindow as MainWindow;
            playListNext.Text = playListCurrent.Text;
            playListCurrent.Text = playListPrev.Text;
            if (window.collectionOfPlayLists.playLists.IndexOf(window.collectionOfPlayLists.playLists.Where(p => p.Name == playListPrev.Text).ElementAt(0)) == 0)
            {
                playListPrev.Text = window.collectionOfPlayLists.playLists[window.collectionOfPlayLists.playLists.Count - 1].Name;
            }
            else
            {
                playListPrev.Text = window.collectionOfPlayLists.playLists[window.collectionOfPlayLists.playLists.IndexOf(window.collectionOfPlayLists.playLists.Where(p => p.Name == playListPrev.Text).ElementAt(0)) - 1].Name;
            }

            if (IsAnimatible)
            {
                playListCurrentAnimatable = InitNewTextBox(playListCurrent);
                playListNextAnimatable = InitNewTextBox(playListNext);
                playListPrevAnimatable = InitNewTextBox(playListPrev);
                playListFreeAnimatable = InitNewTextBox(playListPrev);

                playListCurrent.Visibility = System.Windows.Visibility.Hidden;
                playListPrev.Visibility = System.Windows.Visibility.Hidden;
                playListNext.Visibility = System.Windows.Visibility.Hidden;

                ThicknessAnimation animationOfLeft = new ThicknessAnimation();
                animationOfLeft.From = new Thickness(-726, 0, 0, 0);
                animationOfLeft.To = new Thickness(-326, 0, 0, 0);
                animationOfLeft.Duration = TimeSpan.FromSeconds(1);
                playListPrevAnimatable.BeginAnimation(TextBlock.MarginProperty, animationOfLeft);

                animationOfLeft = new ThicknessAnimation();
                animationOfLeft.From = new Thickness(-326, 0, 0, 0);
                animationOfLeft.To = new Thickness(0, 0, 0, 0);
                animationOfLeft.Duration = TimeSpan.FromSeconds(1);
                playListCurrentAnimatable.BeginAnimation(TextBlock.MarginProperty, animationOfLeft);

                animationOfLeft = new ThicknessAnimation();
                animationOfLeft.From = new Thickness(0, 0, 0, 0);
                animationOfLeft.To = new Thickness(326, 0, 0, 0);
                animationOfLeft.Duration = TimeSpan.FromSeconds(1);
                playListNextAnimatable.BeginAnimation(TextBlock.MarginProperty, animationOfLeft);

                animationOfLeft = new ThicknessAnimation();
                animationOfLeft.From = new Thickness(326, 0, 0, 0);
                animationOfLeft.To = new Thickness(672, 0, 0, 0);
                animationOfLeft.Duration = TimeSpan.FromSeconds(1);
                animationOfLeft.Completed += animationOfLeft_Completed;
                playListFreeAnimatable.BeginAnimation(TextBlock.MarginProperty, animationOfLeft);
            }
            else
            {
                playListView.Items.Clear();
                InitPlayListView(window.collectionOfPlayLists.playLists.Where(p => p.Name == playListCurrent.Text).ElementAt(0));
            }
        }

        void animationOfLeft_Completed(object sender, EventArgs e)
        {
            window = App.Current.MainWindow as MainWindow;
            playListView.Items.Clear();
            InitPlayListView(window.collectionOfPlayLists.playLists.Where(p => p.Name == playListCurrent.Text).ElementAt(0));

            playListCurrent.Visibility = System.Windows.Visibility.Visible;
            playListPrev.Visibility = System.Windows.Visibility.Visible;
            playListNext.Visibility = System.Windows.Visibility.Visible;

            playListCurrentAnimatable.Visibility = System.Windows.Visibility.Hidden;
            playListPrevAnimatable.Visibility = System.Windows.Visibility.Hidden;
            playListNextAnimatable.Visibility = System.Windows.Visibility.Hidden;
            playListFreeAnimatable.Visibility = System.Windows.Visibility.Hidden;
        }

        private void playListNext_MouseDown(object sender, MouseButtonEventArgs e)
        {
            window = App.Current.MainWindow as MainWindow;
            if (IsAnimatible)
            {
                playListCurrentAnimatable = InitNewTextBox(playListCurrent);
                playListNextAnimatable = InitNewTextBox(playListNext);
                playListPrevAnimatable = InitNewTextBox(playListPrev);
                playListFreeAnimatable = InitNewTextBox(playListNext);
                if (window.collectionOfPlayLists.playLists.IndexOf(window.collectionOfPlayLists.playLists.Where(p => p.Name == playListNext.Text).ElementAt(0)) == window.collectionOfPlayLists.playLists.Count - 1)
                {
                    playListFreeAnimatable.Text = window.collectionOfPlayLists.playLists[0].Name;
                }
                else
                {
                    playListFreeAnimatable.Text = window.collectionOfPlayLists.playLists[window.collectionOfPlayLists.playLists.IndexOf(window.collectionOfPlayLists.playLists.Where(p => p.Name == playListNext.Text).ElementAt(0)) + 1].Name;
                }

                playListCurrent.Visibility = System.Windows.Visibility.Hidden;
                playListPrev.Visibility = System.Windows.Visibility.Hidden;
                playListNext.Visibility = System.Windows.Visibility.Hidden;

                ThicknessAnimation animationOfLeft = new ThicknessAnimation();
                animationOfLeft.From = new Thickness(-326, 0, 0, 0);
                animationOfLeft.To = new Thickness(-726, 0, 0, 0);
                animationOfLeft.Duration = TimeSpan.FromSeconds(1);
                playListPrevAnimatable.BeginAnimation(TextBlock.MarginProperty, animationOfLeft);

                animationOfLeft = new ThicknessAnimation();
                animationOfLeft.From = new Thickness(0, 0, 0, 0);
                animationOfLeft.To = new Thickness(-326, 0, 0, 0);
                animationOfLeft.Duration = TimeSpan.FromSeconds(1);
                playListCurrentAnimatable.BeginAnimation(TextBlock.MarginProperty, animationOfLeft);

                animationOfLeft = new ThicknessAnimation();
                animationOfLeft.From = new Thickness(326, 0, 0, 0);
                animationOfLeft.To = new Thickness(0, 0, 0, 0);
                animationOfLeft.Duration = TimeSpan.FromSeconds(1);
                playListNextAnimatable.BeginAnimation(TextBlock.MarginProperty, animationOfLeft);

                animationOfLeft = new ThicknessAnimation();
                animationOfLeft.From = new Thickness(672, 0, 0, 0);
                animationOfLeft.To = new Thickness(326, 0, 0, 0);
                animationOfLeft.Duration = TimeSpan.FromSeconds(1);
                animationOfLeft.Completed += animationOfRight_Completed;
                playListFreeAnimatable.BeginAnimation(TextBlock.MarginProperty, animationOfLeft);
            }
            else
            {
                playListPrev.Text = playListCurrent.Text;
                playListCurrent.Text = playListNext.Text;
                if (window.collectionOfPlayLists.playLists.IndexOf(window.collectionOfPlayLists.playLists.Where(p => p.Name == playListNext.Text).ElementAt(0)) == window.collectionOfPlayLists.playLists.Count - 1)
                {
                    playListNext.Text = window.collectionOfPlayLists.playLists[0].Name;
                }
                else
                {
                    playListNext.Text = window.collectionOfPlayLists.playLists[window.collectionOfPlayLists.playLists.IndexOf(window.collectionOfPlayLists.playLists.Where(p => p.Name == playListNext.Text).ElementAt(0)) + 1].Name;
                }
                playListView.Items.Clear();
                InitPlayListView(window.collectionOfPlayLists.playLists.Where(p => p.Name == playListCurrent.Text).ElementAt(0));
            }
        }

        private void animationOfRight_Completed(object sender, EventArgs e)
        {
            window = App.Current.MainWindow as MainWindow;
            playListPrev.Text = playListCurrent.Text;
            playListCurrent.Text = playListNext.Text;
            if (window.collectionOfPlayLists.playLists.IndexOf(window.collectionOfPlayLists.playLists.Where(p => p.Name == playListNext.Text).ElementAt(0)) == window.collectionOfPlayLists.playLists.Count - 1)
            {
                playListNext.Text = window.collectionOfPlayLists.playLists[0].Name;
            }
            else
            {
                playListNext.Text = window.collectionOfPlayLists.playLists[window.collectionOfPlayLists.playLists.IndexOf(window.collectionOfPlayLists.playLists.Where(p => p.Name == playListNext.Text).ElementAt(0)) + 1].Name;
            }
            playListView.Items.Clear();
            InitPlayListView(window.collectionOfPlayLists.playLists.Where(p => p.Name == playListCurrent.Text).ElementAt(0));

            playListCurrent.Visibility = System.Windows.Visibility.Visible;
            playListPrev.Visibility = System.Windows.Visibility.Visible;
            playListNext.Visibility = System.Windows.Visibility.Visible;

            playListCurrentAnimatable.Visibility = System.Windows.Visibility.Hidden;
            playListPrevAnimatable.Visibility = System.Windows.Visibility.Hidden;
            playListNextAnimatable.Visibility = System.Windows.Visibility.Hidden;
            playListFreeAnimatable.Visibility = System.Windows.Visibility.Hidden;
        }

        private void NewPlayList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            window = App.Current.MainWindow as MainWindow;
            AddNewPlayList dlg = new AddNewPlayList(TypeOfWindow.Add);
            if (dlg.ShowDialog() == true)
            {
                if (!window.collectionOfPlayLists.playLists.Select(p => p.Name).Contains(dlg.NameOfPlayList))
                {
                    window.collectionOfPlayLists.playLists.Add(new PlayList() { Name = dlg.NameOfPlayList });

                    if (window.collectionOfPlayLists.playLists.Count > 3)
                    {
                        IsAnimatible = true;
                        if (window.collectionOfPlayLists.playLists.IndexOf(window.collectionOfPlayLists.currentPlayList) - 1 < 0)
                        {
                            playListPrev.Text = window.collectionOfPlayLists.playLists[window.collectionOfPlayLists.playLists.Count - 1].Name;
                        }
                        else
                        {
                            playListPrev.Text = window.collectionOfPlayLists.playLists[window.collectionOfPlayLists.playLists.IndexOf(window.collectionOfPlayLists.currentPlayList) - 1].Name;
                        }
                        playListCurrent.Text = window.collectionOfPlayLists.currentPlayList.Name;
                        if (window.collectionOfPlayLists.playLists.IndexOf(window.collectionOfPlayLists.currentPlayList) + 1 > window.collectionOfPlayLists.playLists.Count)
                        {
                            playListNext.Text = window.collectionOfPlayLists.playLists[0].Name;
                        }
                        else
                        {
                            if (window.collectionOfPlayLists.playLists.IndexOf(window.collectionOfPlayLists.currentPlayList) + 1 >= window.collectionOfPlayLists.playLists.Count)
                            {
                                playListNext.Text = window.collectionOfPlayLists.playLists[0].Name;
                            }
                            else
                            {
                                playListNext.Text = window.collectionOfPlayLists.playLists[window.collectionOfPlayLists.playLists.IndexOf(window.collectionOfPlayLists.currentPlayList) + 1].Name;
                            }
                        }
                        playListPrev.Visibility = System.Windows.Visibility.Visible;
                        playListNext.Visibility = System.Windows.Visibility.Visible;
                    }
                    else
                    {
                        IsAnimatible = false;
                        if (window.collectionOfPlayLists.playLists.Count == 1)
                        {
                            playListPrev.Visibility = System.Windows.Visibility.Hidden;
                            playListNext.Visibility = System.Windows.Visibility.Hidden;
                            playListCurrent.Text = window.collectionOfPlayLists.playLists[0].Name;
                        }
                        else
                            if (window.collectionOfPlayLists.playLists.Count == 2)
                            {
                                playListNext.Visibility = System.Windows.Visibility.Hidden;
                                playListPrev.Text = window.collectionOfPlayLists.playLists[0].Name;
                                playListCurrent.Text = window.collectionOfPlayLists.playLists[1].Name;
                                playListPrev.Visibility = System.Windows.Visibility.Visible;
                            }
                            else
                            {
                                playListNext.Text = window.collectionOfPlayLists.playLists[2].Name;
                                playListPrev.Text = window.collectionOfPlayLists.playLists[0].Name;
                                playListCurrent.Text = window.collectionOfPlayLists.playLists[1].Name;
                                playListPrev.Visibility = System.Windows.Visibility.Visible;
                                playListNext.Visibility = System.Windows.Visibility.Visible;
                            }
                    }
                    InitPlayListView(window.collectionOfPlayLists.currentPlayList);
                }
                else
                {
                    MessageBox.Show("You have already playlist with that name!", "Player", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public TextBlock InitNewTextBox(TextBlock textBlock)
        {
            TextBlock newTextBlock = new TextBlock();

            newTextBlock.Text = textBlock.Text;
            newTextBlock.Margin = textBlock.Margin;
            newTextBlock.Background = textBlock.Background;
            newTextBlock.Foreground = textBlock.Foreground;
            newTextBlock.Width = textBlock.Width;
            newTextBlock.Height = textBlock.Height;
            newTextBlock.LineHeight = 20;
            newTextBlock.TextAlignment = TextAlignment.Center;
            newTextBlock.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;
            newTextBlock.TextWrapping = TextWrapping.Wrap;

            PlayListsGrid.Children.Add(newTextBlock);

            return newTextBlock;
        }

        private void DeletePlayList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            window = App.Current.MainWindow as MainWindow;
            
            if (window.collectionOfPlayLists.playLists.Count > 1)
            {
                if (playListCurrent.Text == window.collectionOfPlayLists.currentPlayList.Name)
                {
                    window.collectionOfPlayLists.playLists.Remove(window.collectionOfPlayLists.currentPlayList);
                    window.collectionOfPlayLists.currentPlayList = window.collectionOfPlayLists.playLists[0];
                    try
                    {
                        window.collectionOfPlayLists.currentPlayItem = window.collectionOfPlayLists.playLists[0].Items[0];
                    }
                    catch
                    {

                    }
                    window.Image_Stop_MouseDown(null, null);
                }
                else
                {
                    window.collectionOfPlayLists.playLists.Remove(window.collectionOfPlayLists.playLists.Where(p => p.Name == playListCurrent.Text).ElementAt(0)); 
                }

                if (window.collectionOfPlayLists.playLists.Count > 3)
                {
                    IsAnimatible = true;
                    if (window.collectionOfPlayLists.playLists.IndexOf(window.collectionOfPlayLists.currentPlayList) - 1 < 0)
                    {
                        playListPrev.Text = window.collectionOfPlayLists.playLists[window.collectionOfPlayLists.playLists.Count - 1].Name;
                    }
                    else
                    {
                        playListPrev.Text = window.collectionOfPlayLists.playLists[window.collectionOfPlayLists.playLists.IndexOf(window.collectionOfPlayLists.currentPlayList) - 1].Name;
                    }
                    playListCurrent.Text = window.collectionOfPlayLists.currentPlayList.Name;
                    if (window.collectionOfPlayLists.playLists.IndexOf(window.collectionOfPlayLists.currentPlayList) + 1 > window.collectionOfPlayLists.playLists.Count)
                    {
                        playListNext.Text = window.collectionOfPlayLists.playLists[0].Name;
                    }
                    else
                    {
                        playListNext.Text = window.collectionOfPlayLists.playLists[window.collectionOfPlayLists.playLists.IndexOf(window.collectionOfPlayLists.currentPlayList) + 1].Name;
                    }
                    playListPrev.Visibility = System.Windows.Visibility.Visible;
                    playListNext.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    IsAnimatible = false;
                    if (window.collectionOfPlayLists.playLists.Count == 1)
                    {
                        playListPrev.Visibility = System.Windows.Visibility.Hidden;
                        playListNext.Visibility = System.Windows.Visibility.Hidden;
                        playListCurrent.Text = window.collectionOfPlayLists.playLists[0].Name;
                    }
                    else
                        if (window.collectionOfPlayLists.playLists.Count == 2)
                        {
                            playListNext.Visibility = System.Windows.Visibility.Hidden;
                            playListPrev.Text = window.collectionOfPlayLists.playLists[0].Name;
                            playListCurrent.Text = window.collectionOfPlayLists.playLists[1].Name;
                            playListPrev.Visibility = System.Windows.Visibility.Visible;
                        }
                        else
                        {
                            playListNext.Text = window.collectionOfPlayLists.playLists[2].Name;
                            playListPrev.Text = window.collectionOfPlayLists.playLists[0].Name;
                            playListCurrent.Text = window.collectionOfPlayLists.playLists[1].Name;
                            playListPrev.Visibility = System.Windows.Visibility.Visible;
                            playListNext.Visibility = System.Windows.Visibility.Visible;
                        }
                }
                playListView.Items.Clear();
                InitPlayListView(window.collectionOfPlayLists.currentPlayList);
            }
        }

        public int IndexOfItemWithoutDirectoryItems()
        {
            int index = 0;

            for (int i = 0; i < playListView.SelectedIndex; i++)
            {
                if (((playListView.Items[i] as ListViewItem).Content as Grid).Children.Count != 1)
                {
                    index++;
                }
            }

            return index;
        }

        private void RenamePlayList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            window = App.Current.MainWindow as MainWindow;
            AddNewPlayList dlg = new AddNewPlayList(TypeOfWindow.Rename);
            if (dlg.ShowDialog() == true)
            {
                if (window.collectionOfPlayLists.currentPlayList.Name == playListCurrent.Text)
                {
                    window.collectionOfPlayLists.currentPlayList.Name = dlg.NameOfPlayList;
                    //window.collectionOfPlayLists.playLists.Where(p => p.Name == playListCurrent.Text).ElementAt(0).Name = dlg.NameOfPlayList;
                }
                else
                {
                    window.collectionOfPlayLists.playLists.Where(p => p.Name == playListCurrent.Text).ElementAt(0).Name = dlg.NameOfPlayList;
                }
                playListCurrent.Text = dlg.NameOfPlayList;
                playListView.Items.Clear();
                InitPlayListView(window.collectionOfPlayLists.currentPlayList);
            }
        }

        private void SearchPlayItems_MouseDown(object sender, MouseButtonEventArgs e)
        {
            window = App.Current.MainWindow as MainWindow;

            PlayList playList = new PlayList();

            playList.Name = "Search Results";
            playList.Items = window.collectionOfPlayLists.playLists.Where(p => p.Name == playListCurrent.Text).ElementAt(0).Items.Where(i => i.Name.Contains(SearchTextBox.Text)).ToList();

            playListView.Items.Clear();
            InitPlayListView(playList);
        }
    }
}

