using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechCity_Lib.Concrete
{
    public class User
    {
        public string IdentityNumber { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public User(string identityNumber, string firstName, string lastName)
        {
            IdentityNumber = identityNumber;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
