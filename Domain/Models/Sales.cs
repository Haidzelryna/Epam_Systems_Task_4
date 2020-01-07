using System;

namespace BLL
{
    public class Sales: Entity
    {
        public Guid ClientId { get; set; }

        public Guid ProductId { get; set; }

        public decimal Sum { get; set; }

        public DateTime Date { get; set; }

        public Guid CreatedByUserId { get; set; }
    }
}
