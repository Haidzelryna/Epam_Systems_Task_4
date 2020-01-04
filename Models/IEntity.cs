using System;

namespace Models
{
    public interface IEntity
    {
        Guid Id { get; set; }

        byte[] Version { get; set; }

        bool IsNew();
    }
}
