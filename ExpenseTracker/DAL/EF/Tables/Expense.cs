using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class Expense
{
    public int ExpenseId { get; set; }

    public int UserId { get; set; }

    public int CategoryId { get; set; }

    public decimal Amount { get; set; }

    public string? Description { get; set; }

    public DateOnly Date { get; set; }

    public bool? IsRecurring { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
