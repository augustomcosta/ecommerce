﻿namespace ecommerce_api.Dtos;

public class OrderDto
{
    public string BasketId { get; set; }
    public int DeliveryMethodId { get; set; }
    public AddressDto ShipToAddress { get; set; }
    public string PaymentIntentId { get; set; }
}