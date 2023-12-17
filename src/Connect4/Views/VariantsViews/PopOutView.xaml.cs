﻿using Connect4.ViewModel;
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

namespace Connect4.Views.VariantsViews
{
    /*
    * Author   : Ivan Mahut (xmahut01)
    * File     : PopOutView
    * Brief    : Interaction logic for PopOutView.xaml
    */
    public partial class PopOutView : Page
    {
        public PopOutView()
        {

            InitializeComponent();
            PopOutViewModel viewModel = new PopOutViewModel();
            viewModel.MainGrid = MainGrid;
            viewModel.GameCanvas = GameCanvas;
            viewModel.GameGrid = GameGrid;

            DataContext = viewModel;
        }
        private void ColumnButton1_Click(object sender, RoutedEventArgs e)
            => StartAnimation(sender, e, 2);
        private void ColumnButton2_Click(object sender, RoutedEventArgs e)
            => StartAnimation(sender, e, 3);
        private void ColumnButton3_Click(object sender, RoutedEventArgs e)
            => StartAnimation(sender, e, 4);
        private void ColumnButton4_Click(object sender, RoutedEventArgs e)
            => StartAnimation(sender, e, 5);
        private void ColumnButton5_Click(object sender, RoutedEventArgs e)
            => StartAnimation(sender, e, 6);
        private void ColumnButton6_Click(object sender, RoutedEventArgs e)
            => StartAnimation(sender, e, 7);
        private void ColumnButton7_Click(object sender, RoutedEventArgs e)
            => StartAnimation(sender, e, 8);
        private void ColumnButton8_Click(object sender, RoutedEventArgs e)
            => StartAnimation(sender, e, 9);

        private void StartAnimation(object sender, RoutedEventArgs e, int column)
        {
            if (sender is Button)
            {
                if (DataContext is PopOutViewModel viewModel)
                {
                    viewModel.DropBallCommand.Execute(column);
                }
            }
        }

        private void ColumnBottomButton1_Click(object sender, RoutedEventArgs e)
            => StartAnimation(sender, e, 2);
        private void ColumnBottomButton2_Click(object sender, RoutedEventArgs e)
            => StartAnimation(sender, e, 3);
        private void ColumnBottomButton3_Click(object sender, RoutedEventArgs e)
            => StartAnimation(sender, e, 4);
        private void ColumnBottomButton4_Click(object sender, RoutedEventArgs e)
            => StartAnimation(sender, e, 5);
        private void ColumnBottomButton5_Click(object sender, RoutedEventArgs e)
            => StartAnimation(sender, e, 6);
        private void ColumnBottomButton6_Click(object sender, RoutedEventArgs e)
            => StartAnimation(sender, e, 7);
        private void ColumnBottomButton7_Click(object sender, RoutedEventArgs e)
            => StartAnimation(sender, e, 8);
        private void ColumnBottomButton8_Click(object sender, RoutedEventArgs e)
            => StartAnimation(sender, e, 9);

        private void Show1Drop(object sender, MouseEventArgs e)
        {
            Drop1.Visibility = Visibility.Visible;
        }
        private void Show2Drop(object sender, MouseEventArgs e)
        {
            Drop2.Visibility = Visibility.Visible;
        }
        private void Show3Drop(object sender, MouseEventArgs e)
        {
            Drop3.Visibility = Visibility.Visible;
        }
        private void Show4Drop(object sender, MouseEventArgs e)
        {
            Drop4.Visibility = Visibility.Visible;
        }
        private void Show5Drop(object sender, MouseEventArgs e)
        {
            Drop5.Visibility = Visibility.Visible;
        }
        private void Show6Drop(object sender, MouseEventArgs e)
        {
            Drop6.Visibility = Visibility.Visible;
        }
        private void Show7Drop(object sender, MouseEventArgs e)
        {
            Drop7.Visibility = Visibility.Visible;
        }
        private void Show8Drop(object sender, MouseEventArgs e)
        {
            Drop8.Visibility = Visibility.Visible;
        }

        private void Hide1tDrop(object sender, MouseEventArgs e)
        {
            Drop1.Visibility = Visibility.Collapsed;
        }

        private void Hide2tDrop(object sender, MouseEventArgs e)
        {
            Drop2.Visibility = Visibility.Collapsed;
        }
        private void Hide3tDrop(object sender, MouseEventArgs e)
        {
            Drop3.Visibility = Visibility.Collapsed;
        }
        private void Hide4tDrop(object sender, MouseEventArgs e)
        {
            Drop4.Visibility = Visibility.Collapsed;
        }
        private void Hide5tDrop(object sender, MouseEventArgs e)
        {
            Drop5.Visibility = Visibility.Collapsed;
        }
        private void Hide6tDrop(object sender, MouseEventArgs e)
        {
            Drop6.Visibility = Visibility.Collapsed;
        }
        private void Hide7tDrop(object sender, MouseEventArgs e)
        {
            Drop7.Visibility = Visibility.Collapsed;
        }
        private void Hide8tDrop(object sender, MouseEventArgs e)
        {
            Drop8.Visibility = Visibility.Collapsed;
        }

        private void Show1Pop(object sender, MouseEventArgs e)
        {
            Pop1.Visibility = Visibility.Visible;
        }
        private void Show2Pop(object sender, MouseEventArgs e)
        {
            Pop2.Visibility = Visibility.Visible;
        }
        private void Show3Pop(object sender, MouseEventArgs e)
        {
            Pop3.Visibility = Visibility.Visible;
        }
        private void Show4Pop(object sender, MouseEventArgs e)
        {
            Pop4.Visibility = Visibility.Visible;
        }
        private void Show5Pop(object sender, MouseEventArgs e)
        {
            Pop5.Visibility = Visibility.Visible;

        }
        private void Show6Pop(object sender, MouseEventArgs e)
        {
            Pop6.Visibility = Visibility.Visible;
        }

        private void Show7Pop(object sender, MouseEventArgs e)
        {
            Pop7.Visibility = Visibility.Visible;
        }

        private void Show8Pop(object sender, MouseEventArgs e)
        {
            Pop8.Visibility = Visibility.Visible;
        }

        private void Hide1Pop(object sender, MouseEventArgs e)
        {
            Pop1.Visibility = Visibility.Collapsed;
        }
        private void Hide2Pop(object sender, MouseEventArgs e)
        {
            Pop2.Visibility = Visibility.Collapsed;
        }
        private void Hide3Pop(object sender, MouseEventArgs e)
        {
            Pop3.Visibility = Visibility.Collapsed;
        }
        private void Hide4Pop(object sender, MouseEventArgs e)
        {
            Pop4.Visibility = Visibility.Collapsed;
        }
        private void Hide5Pop(object sender, MouseEventArgs e)
        {
            Pop5.Visibility = Visibility.Collapsed;
        }
        private void Hide6Pop(object sender, MouseEventArgs e)
        {
            Pop6.Visibility = Visibility.Collapsed;
        }
        private void Hide7Pop(object sender, MouseEventArgs e)
        {
            Pop7.Visibility = Visibility.Collapsed;
        }
        private void Hide8Pop(object sender, MouseEventArgs e)
        {
            Pop8.Visibility = Visibility.Collapsed;
        }
    }
}
