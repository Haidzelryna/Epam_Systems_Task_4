using System;

namespace BLL
{
    public class BaseEntity : Entity
    {
        public Guid? CreatedByUserId { get; set; }

        public DateTime? CreatedDateTime { get; set; }
    }
}
