using System.Collections.Generic;

namespace BLL
{
    public class Contact: Entity
    {       
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Manager> Managers { get; set; }
    }
}
