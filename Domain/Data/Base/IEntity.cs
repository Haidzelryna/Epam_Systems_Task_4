using System;

namespace BLL
{
    public interface IEntity
    {
        Guid Id { get; set; }

        byte[] Version { get; set; }

        bool IsNew();
    }
}
