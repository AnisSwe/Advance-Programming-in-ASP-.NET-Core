using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class Budget
{
    public int BudgetId { get; set; }

    public int UserId { get; set; }

    public int CategoryId { get; set; }

    public string MonthYear { get; set; } = null!;

    public decimal LimitAmount { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
