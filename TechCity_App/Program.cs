using TechCity_Lib.Abstract;
using TechCity_Lib.Concrete;

#region Variables
CardType cardType = CardType.Normal;
Type type = null;
User user = null;
#endregion
Console.WriteLine("TechCity Kart Arayüzü");
DisplayBaseCardsUI();
type = GetBaseCardType();
if (type != typeof(AnonimCard))
{
    DisplayCardTypeUI();
    cardType = GetCardType();
    user = DisplayUserInformationUI();
}
Card card = DisplayCardSettingsUI(type, user, cardType);
Console.WriteLine("\nKart oluşturuldu... İlerlemek için bir tuşa basın.");
Console.ReadKey();
DisplayCardUI(card);
Console.ReadKey();


CardType GetCardType()
{
    string value;
    int number;
    do
    {
        Console.Write("Oluşturmak istediğiniz kart tipini aşağıdan seçin: ");
        value = Console.ReadLine();
    } while (!int.TryParse(value, out number) || (number > 4 || number < 1));
    number--;
    CardType cardType = number switch
    {
        0 => CardType.Öğrenci,
        1 => CardType.Yaşlı,
        2 => CardType.Çalışan,
        3 => CardType.Normal,
    };
    return cardType;
}
Type GetBaseCardType()
{
    string value;
    int number;
    do
    {
        Console.Write("Oluşturmak istediğiniz kart tipini aşağıdan seçiniz: ");
        value = Console.ReadLine();
    } while (!int.TryParse(value, out number) || (number > 3 || number < 1));
    number--;
    Type type = number switch
    {
        0 => typeof(PhysicalCard),
        1 => typeof(DigitalCard),
        2 => typeof(AnonimCard),
    };

    return type;
}

Decimal GetDecimalValue(string message)
{
    string value;
    decimal number;
    do
    {
        Console.Write(message);
        value = Console.ReadLine();
    } while (!decimal.TryParse(value, out number));

    return number;
}
void DisplayCardTypeUI()
{
    Console.WriteLine("===============================================================");
    Console.WriteLine("1- Öğrenci");
    Console.WriteLine("2- 60 yaş üzeri");
    Console.WriteLine("3- Çalışan");
    Console.WriteLine("4- Normal");
}

void DisplayBaseCardsUI()
{
    Console.WriteLine("===============================================================");
    Console.WriteLine("1- Fiziksel");
    Console.WriteLine("2- Dijital");
    Console.WriteLine("3- Anonim");
}

User DisplayUserInformationUI()
{
    string[] userInfo = new string[3];
    Console.WriteLine("===============================================================");
    Console.Write("{0,-11}: ", "Kimlik Numaranız");
    userInfo[0] = Console.ReadLine();
    Console.Write("{0,-16}: ", "Adınız");
    userInfo[1] = Console.ReadLine();
    Console.Write("{0,-16}: ", "Soyadınız");
    userInfo[2] = Console.ReadLine();
    User user = new User(userInfo[0], userInfo[1], userInfo[2]);
    return user;
}

Card DisplayCardSettingsUI(Type t, User user, CardType cardType)
{
    Card card = null;
    string cardNumber = "";
    decimal[] balanceSettings = new decimal[2];
    Console.WriteLine("===============================================================");
    balanceSettings[0] = GetDecimalValue(string.Format("{0,-11}: ", "Sabit Ücret"));
    balanceSettings[1] = GetDecimalValue(string.Format("{0,-11}: ", "Bakiye"));
    while (cardNumber.Length != 16 || !cardNumber.All(char.IsDigit))
    {
        Console.Write("{0,-11}: ", "Kart Numarası");
        cardNumber = Console.ReadLine();
    }
    if (type == typeof(AnonimCard))
        card = new AnonimCard(cardNumber, balanceSettings[0], balanceSettings[1]);
    else if (type == typeof(DigitalCard))
        card = new DigitalCard(cardNumber, cardType, balanceSettings[0], balanceSettings[1], user);
    else if (type == typeof(PhysicalCard))
        card = new PhysicalCard(cardNumber, cardType, balanceSettings[0], balanceSettings[1], user);

    return card;
}

int GetInt(string message, int min, int max)
{
    string value;
    int number;
    do
    {
        Console.Write(message);
        value = Console.ReadLine();
    } while (!int.TryParse(value, out number) || number < min || number > max);

    return number;
}
void DisplayCardUI(Card card)
{
    bool status = true;
    while (status)
    {
        Console.Clear();
        Console.WriteLine("===============================================================");
        Console.WriteLine("1- Bakiye Görüntüleme");
        Console.WriteLine("2- Yükleme Yapma");
        Console.WriteLine("3- Ücret Hesaplama");
        Console.WriteLine("4- Ödeme Yapma");
        int secim = GetInt("Lütfen yapmak istediğiniz işlemi seçin: ", 1, 4);
        switch (secim)
        {
            case 1:
                DisplayBalance(card);
                break;
            case 2:
                AddBalance(card);
                break;
            case 3:
                DisplayFare(card);
                break;
            case 4:
                Pay(card);
                break;
        }
        Console.Write("\nBaşka bir işlem yapmak istiyor musunuz? [E-e]: ");
        string key = Console.ReadLine();
        status = "E".Equals(key) || "e".Equals(key);
    }

}

void DisplayBalance(Card card)
{
    Console.WriteLine("\nKart Bakiyesi: {0}", card.Balance);
}

void DisplayFare(Card card)
{
    if (card.CardType == CardType.Normal)
    {
        Console.WriteLine("\nNormal karta sahip olduğunuz için indirim uygulanamadı.", card.CardType);
    }
    else
    {
        Console.WriteLine("\n{0} kartına sahip olduğunuz için indirim uygulandı.", card.CardType);
        Console.WriteLine("Sabit ücret: {0}", card.FixedFare);
    }
    Console.WriteLine("Ödemeniz gereken ücret: {0}", card.ReducedFare);
}

void AddBalance(Card card)
{
    Console.WriteLine();
    decimal price = GetDecimalValue("Yüklemek istediğiniz tutarı giriniz: ");
    card.AddBalance(price);
    Console.WriteLine("Yükleme gerçekleşti.");
    DisplayBalance(card);
}

void Pay(Card card)
{
    Console.WriteLine("\nÖdeme yapılıyor. İlerlemek için bir tuşa basın.");
    Console.ReadKey();
    bool status = card.Pay();
    if (status)
        Console.WriteLine("Ödeme yapıldı.");
    else
        Console.WriteLine("Yetersiz bakiye.");

}
