using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCity_Lib.Concrete;

namespace TechCity_Lib.Abstract
{
    public enum CardType
    {
        Öğrenci,
        Yaşlı,
        Çalışan,
        Normal
    }
    public abstract class Card
    {
        private string cardNumber;
        private decimal balance;
        public string CardNumber
        {
            get { return cardNumber; }
            init
            {
                if (value.Length != 16 || !value.All(char.IsDigit))
                    throw new Exception("Lütfen 16 haneli kart numaranızı tuşlayınız.");
                cardNumber = value;
            }
        }
        public CardType CardType { get; }
        public decimal FixedFare { get; }
        public decimal ReducedFare { get; }
        public decimal Balance { get { return balance; } }
        public User User { get; }
        public Card(string cardNumber, CardType cardType, decimal fixedFare, decimal balance, User user):this(cardNumber,cardType,fixedFare,balance)
        {
            User = user;
        }
        public Card(string cardNumber, CardType cardType, decimal fixedFare, decimal balance)
        {
            CardNumber = cardNumber;
            CardType = cardType;
            FixedFare = fixedFare;
            this.balance = balance;
            ReducedFare = cardType switch
            {
                CardType.Öğrenci => fixedFare - (fixedFare * 0.75m),
                CardType.Yaşlı => fixedFare - (fixedFare * 0.50m),
                CardType.Çalışan => fixedFare - (fixedFare * 0.25m),
                CardType.Normal => fixedFare
            };
        }
        public bool Pay()
        {
            bool status = false;
            if (FixedFare <= balance)
            {
                balance -= FixedFare;
                status = true;
            }
            return status;
        }
        public void AddBalance(decimal amount)
        {
            balance += amount;
        }

    }
}
