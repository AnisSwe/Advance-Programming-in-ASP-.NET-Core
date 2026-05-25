using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
 public  class AlertDTO
    {
        public int AlertId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; } = "";
        public bool IsRead { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
