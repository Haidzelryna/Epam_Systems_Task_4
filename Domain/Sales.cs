﻿using System;

namespace Domain
{
    public class Sales: Entity
    {
        public System.Guid ClientId { get; set; }
        public System.Guid ProductId { get; set; }
        public decimal Sum { get; set; }
        public System.DateTime Date { get; set; }
    }
}
