using System;
using System.Collections.Generic;

namespace api.Models;

public partial class CartItem
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ItemId { get; set; }

    public int Quantity { get; set; }

    public virtual Item Item { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
