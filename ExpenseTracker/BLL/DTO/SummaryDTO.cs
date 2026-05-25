using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
   public  class SummaryDTO
    {
        public decimal TotalSpent { get; set; }
        public decimal DailyAverage { get; set; }
        public string TopCategory { get; set; } = "";
        public int TotalTransactions { get; set; }
        public string MonthYear { get; set; } = "";
    }
}
