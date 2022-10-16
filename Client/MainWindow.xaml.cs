using System.Windows;

namespace Client;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void EditButtonClick(object sender, RoutedEventArgs e)
    {
        productsList.Items.Refresh();
    }
}