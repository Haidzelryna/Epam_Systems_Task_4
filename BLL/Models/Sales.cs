using System;

namespace BLL
{
    public class Sales: Entity
    {
        public string ClientName { get; set; }

        public string ProductName { get; set; }

        public decimal Sum { get; set; }

        public DateTime Date { get; set; }

        public Guid CreatedByUserId { get; set; }

        public DateTime CreatedDateTime { get; set; }
    }
}
