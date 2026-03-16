using System;
using System.Collections.Generic;

namespace BookShop.EF.Tables;

public partial class Book
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Isbn { get; set; } = null!;

    public decimal Price { get; set; }

    public string AuthorName { get; set; } = null!;
}
