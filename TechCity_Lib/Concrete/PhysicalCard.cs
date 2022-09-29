using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCity_Lib.Abstract;

namespace TechCity_Lib.Concrete
{
    public class PhysicalCard : Card
    {
        public PhysicalCard(string cardNumber, CardType cardType, decimal fixedFare, decimal balance, User user) : base(cardNumber, cardType, fixedFare, balance, user)
        {

        }
    }
}
