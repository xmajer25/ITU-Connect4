﻿using Connect4.Services;
using Connect4.ViewModel;
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

namespace Connect4.Views.UserViews
{
    /// <summary>
    /// Interaction logic for LogInView.xaml
    /// </summary>
    public partial class LogInView : Page
    {
        public LogInView()
        {
            InitializeComponent();
            DataContext = new LogInViewModel();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            AudioManager.PlaySound();
        }
    }
}
