using System;

namespace BLL
{
    public class Client : Entity
    {
        public string Name { get; set; }

        public Guid? ContactId { get; set; }
    }
}
