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
using Triarch.Definitions.Editor.WPF.ViewModels;

namespace Triarch.Definitions.Editor.WPF.Views;
/// <summary>
/// Interaction logic for EditTypesView.xaml
/// </summary>
public partial class EditGenresView : Window
{
    public EditGenresView()
    {
        InitializeComponent();
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        ((EditGenresViewModel)DataContext).Save();
    }

    private void CreateButton_Click(object sender, RoutedEventArgs e)
    {
        ((EditGenresViewModel)DataContext).Create();
    }

    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
        ((EditGenresViewModel)DataContext).Edit();
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        ((EditGenresViewModel)DataContext).Delete();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        ((EditGenresViewModel)DataContext).CancelEdit();
    }

    private void MoveUpButton_Click(object sender, RoutedEventArgs e)
    {
        ((EditGenresViewModel)DataContext).MoveUp();
    }

    private void MoveDownButton_Click(object sender, RoutedEventArgs e)
    {
        ((EditGenresViewModel)DataContext).MoveDown();
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
