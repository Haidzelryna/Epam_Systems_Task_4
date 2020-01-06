using System;

namespace BLL
{
    public interface IBaseEntity
    {
        Guid? CreatedByUserId { get; set; }

        DateTime? CreatedDateTime { get; set; }

        Guid? UpdatedByUserId { get; set; }

        DateTime? UpdatedDateTime { get; set; }
    }
}