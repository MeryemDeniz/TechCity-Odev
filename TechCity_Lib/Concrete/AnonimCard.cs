using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCity_Lib.Abstract;

namespace TechCity_Lib.Concrete
{
    public class AnonimCard : Card
    {
        public AnonimCard(string cardNumber, decimal fixedFare, decimal balance) : base(cardNumber, CardType.Normal, fixedFare, balance)
        {
        }

    }
}
