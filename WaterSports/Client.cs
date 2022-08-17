namespace WaterSports
{
    public class Client
    {
        private string name;
        private int age;
        private string gender;
        private string phoneNumber;
        private string creditCardNumber;
        private Sports sport;

        public string Name { get => name; set => name = value; }
        public int Age { get => age; set => age = value; }
        public string Gender { get => gender; set => gender = value; }

        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string CreditCardNumber { get => creditCardNumber; set => creditCardNumber = value; }
        public Sports Sport { get => sport; set => sport = value; }

        public Client()
        {

        }
        public Client(string personName, int personAge, string personGender, string personPhoneNumber, string personCreditCardNumber)
        {
            Name = personName;
            Age = personAge;
            Gender = personGender;
            PhoneNumber = personPhoneNumber;
            CreditCardNumber = personCreditCardNumber;
        }
    }
}
