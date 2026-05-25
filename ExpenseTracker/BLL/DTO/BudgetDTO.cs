using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTO
{
   public class BudgetDTO
    {
        public int BudgetId { get; set; }

        public int UserId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public string? CategoryName { get; set; }

        [Required]
        public string MonthYear { get; set; } = "";

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Limit must be greater than 0")]
        public decimal LimitAmount { get; set; }

        // extra — filled in BLL for report
        public decimal TotalSpent { get; set; }
        public bool IsExceeded => TotalSpent > LimitAmount;
    }
}
