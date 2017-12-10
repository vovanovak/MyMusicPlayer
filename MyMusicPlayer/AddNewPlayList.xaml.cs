﻿using System;
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

namespace MyMusicPlayer
{
    /// <summary>
    /// Логика взаимодействия для AddNewPlayList.xaml
    /// </summary>
    public enum TypeOfWindow
    {
        Add, Rename
    }

    public partial class AddNewPlayList : Window
    {
        public string NameOfPlayList { get; set; }
        public AddNewPlayList(TypeOfWindow type)
        {
            InitializeComponent();
            if (type == TypeOfWindow.Rename)
            {
                labelTitle.Content = "Rename playlist";
            }
        }

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            NameOfPlayList = textBox.Text;
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
