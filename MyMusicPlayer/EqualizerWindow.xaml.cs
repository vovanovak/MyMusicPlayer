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
using System.Windows.Shapes;
using NAudio;

namespace MyMusicPlayer
{
    /// <summary>
    /// Логика взаимодействия для EqualizerWindow.xaml
    /// </summary>
    public partial class EqualizerWindow : Window
    {
        public EqualizerWindow()
        {
            InitializeComponent();
            NAudio.Gui.WaveViewer view = new NAudio.Gui.WaveViewer();
        }
    }
}
