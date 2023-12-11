namespace Domain.Models;

public class BasketItem {

    public int Id {get; set;}

    public required string ProductName {get; set;}

    public decimal Price {get; set;}

    public int Quantity {get; set;}

    public string? ImageUrl {get; set;}

    public string? Brand {get; set;}

    public string? Category {get; set;}
}