﻿using ecommerce_api.Domain.Models.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce_api.Data.Config;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.OwnsOne(i => i.ItemOrdered,
            io => {io.WithOwner();});

        builder.Property(i => i.Price).HasColumnType("decimal(18,2)");
    }
}