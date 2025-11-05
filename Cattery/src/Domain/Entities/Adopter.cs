using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Adopter
    {
        private string _name;
        private string _surname;
        private PhoneNumber _phone;
        private Email _email;
        private TaxId _taxId;
        private Cap _postalCode;
        private string _city;
        private string _address;

        public string Address
        {
            get => _address;
            set => _address = value;
        }

        public string Name
        {
            get => _name;
            set {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be empty.");
                _name = value;
            }
        }

        public string Surname
        {
            get => _surname;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be empty.");
                _surname = value;
            }
        }

        public PhoneNumber Phone
        {
            get => _phone;
            set => _phone = value;
        }

        public Email Email
        {
            get => _email;
            set => _email = value;
        }

        public TaxId TaxId
        {
            get => _taxId;
            set => _taxId = value;
        }

        public Cap PostalAddress
        {
            get => _postalCode;
            set => _postalCode = value;
        }

        public string City
        {
            get => _city;
            set => _city = value;
        }

        public Adopter(string name, string surname, PhoneNumber? phone, Email? email, TaxId taxId,Cap postalcode, string address)
        {
            Name = name;
            Surname = surname;
            Phone = phone ?? throw new ArgumentNullException(nameof(phone));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            TaxId = taxId ?? throw new ArgumentNullException(nameof(taxId));
            PostalAddress = postalcode ?? throw new ArgumentNullException(nameof(postalcode));
            Address = address;
        }

        public override string ToString()
        {
            return $"{Name} {Surname}, Phone: {Phone}, Email: {Email}, TaxId: {TaxId}, PostalCode: {PostalAddress}, City: {City}";
        }
    }
}
