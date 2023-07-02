﻿namespace Final.Project.BL;
public class ProductFilterationResultDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Image { get; set; } = string.Empty;
    public decimal Discount { get; set; }
    public decimal PriceAfter => Price - (Price * Discount / 100);
    public decimal AvgRating { get; set; }
    public decimal AvgRatingRounded => Math.Round(AvgRating, 1);
    public int ReviewCount { get; set; }
}
