using System;
using System.Collections.Generic;

namespace DoanThucTapChuyenNganh.Models;

public partial class OrderDetail
{
    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
