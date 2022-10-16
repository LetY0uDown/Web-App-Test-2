using System.Text.Json.Serialization;

namespace Models;

public class Product : ICloneable
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

    public object Clone()
    {
        return new Product
        {
            ID = ID,
            Title = Title,
            Price = Price
        };
    }
}