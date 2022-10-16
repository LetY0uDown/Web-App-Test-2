using Client.Core;
using Models;
using MVVM_Classes;
using System.Collections.ObjectModel;
using System.Windows;

namespace Client;

public sealed class MainViewModel : ObservableObject
{
    public MainViewModel()
    {
        SetProducts();

        InitCommands();
    }

    private Product _selectedProduct;
    public Product SelectedProduct
    {
        get => _selectedProduct;
        set
        {
            if (value is not null)
            {
                ProductTitle = value.Title;
                ProductPrice = value.Price;
            }

            _selectedProduct = value;
        }
    }

    public string ProductTitle { get; set; }
    public decimal ProductPrice { get; set; }

    public ObservableCollection<Product> Products { get; set; } = new();

    public Command RemoveCommand { get; private set; }
    public Command AddCommand { get; private set; }
    public Command EditCommand { get; private set; }

    private async void SetProducts()
    {
        Products = await DataProvider.GetAsync<ObservableCollection<Product>>(nameof(Product));
    }

    private void InitCommands()
    {
        AddCommand = new(async o =>
        {
            Product product = new(ProductTitle, ProductPrice);

            var status = await DataProvider.PostAsync(product, nameof(Product));

            if (status == System.Net.HttpStatusCode.OK)
            {
                Products.Add(product);

                ProductTitle = string.Empty;
                ProductPrice = decimal.Zero;
            }
            else
                MessageBox.Show($"Cannot add product. Status code: {status}", "Operation failed");
        },
        b => !string.IsNullOrWhiteSpace(ProductTitle) && ProductPrice > decimal.Zero);

        RemoveCommand = new(async o =>
        {
            var status = await DataProvider.DeleteAsync(SelectedProduct.ID, nameof(Product));

            if (status == System.Net.HttpStatusCode.OK)
                Products.Remove(SelectedProduct);
            else
                MessageBox.Show($"Cannot remove product. Status code: {status}", "Operation failed");
        },
        b => SelectedProduct is not null);

        EditCommand = new(async o =>
        {
            Product product = new(ProductTitle, ProductPrice);

            var status = await DataProvider.PutAsync(SelectedProduct.Clone(), nameof(Product));

            if (status == System.Net.HttpStatusCode.OK)
            {
                SelectedProduct.Price = ProductPrice;
                SelectedProduct.Title = ProductTitle;

                ProductTitle = string.Empty;
                ProductPrice = decimal.Zero;
            }
            else
                MessageBox.Show($"Cannot update product. Status code: {status}", "Operation failed");
        },
        b => SelectedProduct is not null && !string.IsNullOrWhiteSpace(ProductTitle) && ProductPrice > decimal.Zero);
    }
}