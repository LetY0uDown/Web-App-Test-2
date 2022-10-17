using System.Windows;

namespace Client;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        productsList.SelectionChanged += ProductsList_SelectionChanged;
    }

    private void ProductsList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        productsList.Items.Refresh();
    }
}