﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Final.Project.DAL;
public class Order
{
    public int Id { get; set; }
    
    public OrderStatus OrderStatus  { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public DateTime? DeliverdDate { get; set; } = null;
    public int UserId { get; set; } 
    public User User { get; set; } = null!;
    public IEnumerable<OrderProductDetails> OrdersProductDetails { get; set; } = new HashSet<OrderProductDetails>();


}

public enum OrderStatus
{
    Pending,
    Processing,
    Shipped,
    Delivered,
    Cancelled
}