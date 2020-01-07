using System;

namespace DAL
{
    public interface IEntity
    {
        Guid Id { get; set; }

        byte[] Version { get; set; }

        bool IsNew();
    }
}
