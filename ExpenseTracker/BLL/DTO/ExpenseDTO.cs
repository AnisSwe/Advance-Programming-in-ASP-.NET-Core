using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTO
{
  
        public class ExpenseDTO
        {
            public int ExpenseId { get; set; }

            [Required]
            public int UserId { get; set; }

            [Required]
            public int CategoryId { get; set; }

            public string? CategoryName { get; set; }

            [Required]
            [Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
            public decimal Amount { get; set; }

            public string? Description { get; set; }

            [Required]
            public DateOnly Date { get; set; }

            public bool IsRecurring { get; set; }
        }
    
}
