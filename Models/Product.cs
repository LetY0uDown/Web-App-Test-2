using System.Text.Json.Serialization;

namespace Models;

public class Product
{
    [JsonConstructor]
    public Product() { }

    public Product(string title, decimal price)
    {
        ID = Guid.NewGuid();

        Title = title;
        Price = price;
    }

    public Guid ID { get; set; }

    public string Title { get; set; }

    public decimal Price { get; set; }
}