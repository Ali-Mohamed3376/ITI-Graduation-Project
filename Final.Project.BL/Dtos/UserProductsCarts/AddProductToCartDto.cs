﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.BL;

public class AddProductToCartDto
{
    public string UserId { get; set; }
    public int ProductId { get; set;}
    public int Quantity { get; set;}
}
