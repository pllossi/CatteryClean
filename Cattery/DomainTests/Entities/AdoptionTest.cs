using Domain.Entities;
using Domain.ValueObjects;

namespace DomainTests.Entities;

[TestClass]
public class AdoptionTest
{
    [TestMethod]
    public void Constructor_ValidValues_ReturnsInstance()
    {
        var phone = new PhoneNumber("3331234567");
        var email = new Email("test@email.com");
        var taxId = new TaxId("RSSMRA80A01H501U");
        var cap = new Cap("00100");
        string address = "Via Roma 1";

        var adopter = new Adopter("Mario", "Rossi", phone, email, taxId, cap, address);

        Assert.AreEqual("Mario", adopter.Name);
        Assert.AreEqual("Rossi", adopter.Surname);
        Assert.AreEqual(phone, adopter.Phone);
        Assert.AreEqual(email, adopter.Email);
        Assert.AreEqual(taxId, adopter.TaxId);
        Assert.AreEqual(cap, adopter.PostalAddress);
        Assert.AreEqual(address, adopter.Address);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Constructor_EmptyName_ThrowsArgumentException()
    {
        var phone = new PhoneNumber("3331234567");
        var email = new Email("test@email.com");
        var taxId = new TaxId("RSSMRA80A01H501U");
        var cap = new Cap("00100");
        string address = "Via Roma 1";

        var _ = new Adopter("", "Rossi", phone, email, taxId, cap, address);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Constructor_EmptySurname_ThrowsArgumentException()
    {
        var phone = new PhoneNumber("3331234567");
        var email = new Email("test@email.com");
        var taxId = new TaxId("RSSMRA80A01H501U");
        var cap = new Cap("00100");
        string address = "Via Roma 1";

        var _ = new Adopter("Mario", "", phone, email, taxId, cap, address);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Constructor_NullPhoneAndNullEmail_ThrowsArgumentNullException()
    {
        var taxId = new TaxId("RSSMRA80A01H501U");
        var cap = new Cap("00100");
        string address = "Via Roma 1";

        var _ = new Adopter("Mario", "Rossi", null, null, taxId, cap, address);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Constructor_NullPhone_ThrowsArgumentNullException()
    {
        var email = new Email("test@email.com");
        var taxId = new TaxId("RSSMRA80A01H501U");
        var cap = new Cap("00100");
        string address = "Via Roma 1";

        var _ = new Adopter("Mario", "Rossi", null, email, taxId, cap, address);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Constructor_NullEmail_ThrowsArgumentNullException()
    {
        var phone = new PhoneNumber("3331234567");
        var taxId = new TaxId("RSSMRA80A01H501U");
        var cap = new Cap("00100");
        string address = "Via Roma 1";

        var _ = new Adopter("Mario", "Rossi", phone, null, taxId, cap, address);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Constructor_NullTaxId_ThrowsArgumentNullException()
    {
        var phone = new PhoneNumber("3331234567");
        var email = new Email("test@email.com");
        var cap = new Cap("00100");
        string address = "Via Roma 1";

        var _ = new Adopter("Mario", "Rossi", phone, email, null, cap, address);
    }

    [TestMethod]
    public void ToString_ValidAdopter_ReturnsNameSurname()
    {
        var phone = new PhoneNumber("3331234567");
        var email = new Email("test@email.com");
        var taxId = new TaxId("RSSMRA80A01H501U");
        var cap = new Cap("00100");
        string address = "Via Roma 1";

        var adopter = new Adopter("Mario", "Rossi", phone, email, taxId, cap, address);

        Assert.AreEqual("Mario Rossi", adopter.ToString());
    }
}
