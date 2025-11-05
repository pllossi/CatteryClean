using Application.DTO;
using Application.Interfaces;
using Application.UseCases;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationTests
{
    public class FakeAdopterRepository : IAdopterRepository
    {
        public List<Adopter> Adopters = new();

        public void addAdopter(Adopter adopter)
        {
            if (adopter is null) throw new ArgumentNullException(nameof(adopter));
            Adopters.Add(adopter);
        }

        public IEnumerable<Adopter> getAllAdopters() => Adopters;
    }
    public class FakeCatRepository : ICatRepository
    {
        public List<Cat> Cats = new();
        public void addCat(Cat cat)
        {
            if(cat is null) throw new ArgumentNullException(nameof(cat));
            foreach(var existingCat in Cats)
            {
                if(existingCat.CodeId == cat.CodeId)
                {
                    throw new ArgumentException("A cat with the same CodeId already exists.");
                }
            }
            Cats.Add(cat);
        }
        public IEnumerable<Cat> getAllCats() => Cats;
        public bool existsByCodeId(string codeId) => Cats.Any(c => c.CodeId == codeId);
    }

    // Repository fittizio per le adozioni
    public class FakeAdoptionRepository : IAdoptionRepository
    {
        public List<Adoption> Adoptions = new();
        public void addAdoption(Adoption adoption) => Adoptions.Add(adoption);
        public void deleteAdoption(Adoption adoption) => Adoptions.Remove(adoption);
        public IEnumerable<Adoption> getAllAdoptions() => Adoptions;
    }

    // Stub di mapping per i test (non usato da CatteryService compilato, ma utile per creare entità nei test)
    public static class TestMappingExtensions
    {
        public static Cat ToEntity(this CatDto dto)
        {
            var cat = new Cat(dto.Name, dto.Breed, dto.IsMale, dto.Description, dto.ExitDate, dto.BirthDate);
            return cat;
        }

        public static CatDto ToDTO(this Cat cat)
        {
            return new CatDto(
                cat.Name,
                cat.Breed,
                cat.Male,
                cat.Description,
                cat.ExitDate,
                cat.BirthDate,
                cat.CodeId
            );
        }

        public static Adoption ToEntity(this AdoptionDTO dto)
        {
            // Create a minimal Adopter for tests
            var phone = new PhoneNumber("1234567");
            var email = new Email("a@b.cd");
            var taxId = new TaxId("AAAAAAAAAAAAAAAA");
            var cap = new Cap("12345");
            var adopter = new Adopter("Test", "User", phone, email, taxId, cap, "addr");

            return new Adoption(
                adopter,
                dto.Cat.ToEntity(),
                dto.AdoptionDate
            );
        }
    }

    [TestClass]
    public class CatteryServiceTests
    {
        private FakeCatRepository _catRepo = null!;
        private FakeAdoptionRepository _adoptionRepo = null!;
        private FakeAdopterRepository _adopterRepo = null!;
        private CatteryService _service = null!;

        [TestInitialize]
        public void Setup()
        {
            _catRepo = new FakeCatRepository();
            _adoptionRepo = new FakeAdoptionRepository();
            _adopterRepo = new FakeAdopterRepository();
            // Costruttore con tre repository (ICatRepository, IAdoptionRepository, IAdopterRepository)
            _service = new CatteryService(_catRepo, _adoptionRepo, _adopterRepo);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddCat_NullDto_Throws()
        {
            _service.AddCat(null);
        }

        [TestMethod]
        public void AddCat_Valid_AddsCat()
        {
            var dto = new CatDto("Name", "Breed", true, null, null, null, "C2");
            _service.AddCat(dto);
            Assert.IsTrue(_service.GetAllCats().Any(c => c.CodeId == "C2"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AdoptCat_NullDto_Throws()
        {
            _service.AdoptCat(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AdoptCat_CatDoesNotExist_Throws()
        {
            var catDto = new CatDto("Name", "Breed", true, null, null, null, "C3");
            var adopterDto = new AdopterDTO("Name", "Surname", new PhoneNumberDTO("1234567"), new EmailDTO("a@b.cd"), "addr", new CapDTO("12345"), new TaxIdDTO("AAAAAAAAAAAAAAAA"));
            var adoptionDto = new AdoptionDTO(catDto, adopterDto, DateTime.Today);
            _service.AdoptCat(adoptionDto);
        }

        [TestMethod]
        public void AdoptCat_Valid_AddsAdoption()
        {
            var catDto = new CatDto("Name", "Breed", true, null, null, null, "C4");
            var adopterDto = new AdopterDTO("Name", "Surname", new PhoneNumberDTO("1234567"), new EmailDTO("a@b.cd"), "addr", new CapDTO("12345"), new TaxIdDTO("AAAAAAAAAAAAAAAA"));
            var adoptionDto = new AdoptionDTO(catDto, adopterDto, DateTime.Today);
            _service.AddCat(catDto);
            _service.AdoptCat(adoptionDto);
            Assert.AreEqual(1, _adoptionRepo.Adoptions.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReturnCat_NullOrWhitespace_Throws()
        {
            _service.ReturnCat(" ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReturnCat_CatNotAdopted_Throws()
        {
            _service.ReturnCat("C5");
        }

        [TestMethod]
        public void ReturnCat_Valid_DeletesAdoption()
        {
            var cat = new Cat("Name", "Breed", true);
            cat.setCodeId("C6");
            var phone = new PhoneNumber("1234567");
            var email = new Email("a@b.cd");
            var taxId = new TaxId("AAAAAAAAAAAAAAAA");
            var cap = new Cap("12345");
            var adopter = new Adopter("Name", "Surname", phone, email, taxId, cap, "addr");
            _catRepo.addCat(cat);
            var adoption = new Adoption(adopter, cat, DateTime.Today);
            _adoptionRepo.addAdoption(adoption);
            _service.ReturnCat("C6");
            Assert.AreEqual(0, _adoptionRepo.Adoptions.Count);
        }

        [TestMethod]
        public void GetAllCats_ReturnsDtos()
        {
            var c1 = new Cat("Name", "Breed", true);
            var c2 = new Cat("Name", "Breed", true);
            _catRepo.addCat(c1);
            _catRepo.addCat(c2);
            var result = _service.GetAllCats().ToList();
            Assert.AreEqual(2, result.Count);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegisterAdopter_NullDto_Throws()
        {
            _service.RegisterAdopter(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RegisterAdopter_DuplicateTaxId_Throws()
        {
            var phone = new PhoneNumber("1234567");
            var email = new Email("a@b.cd");
            var taxId = new TaxId("DUPLICATETAXID01");
            var cap = new Cap("12345");
            var existingAdopter = new Adopter("Existing", "User", phone, email, taxId, cap, "addr");
            _adopterRepo.addAdopter(existingAdopter);

            var adopterDto = new AdopterDTO(
                "New",
                "User",
                new PhoneNumberDTO("1234567"),
                new EmailDTO("a@b.cd"),
                "addr",
                new CapDTO("12345"),
                new TaxIdDTO("DUPLICATETAXID01")
            );

            _service.RegisterAdopter(adopterDto);
        }

        [TestMethod]
        public void RegisterAdopter_Valid_AddsAdopter()
        {
            var adopterDto = new AdopterDTO(
                "Valid",
                "User",
                new PhoneNumberDTO("1234567"),
                new EmailDTO("valid@b.cd"),
                "addr",
                new CapDTO("12345"),
                new TaxIdDTO("UNIQUETAXID01")
            );

            _service.RegisterAdopter(adopterDto);

            Assert.IsTrue(_adopterRepo.Adopters.Any(a => a.TaxId.ToString() == "UNIQUETAXID01" || a.TaxId.Equals(new TaxId("UNIQUETAXID01"))));
            Assert.AreEqual(1, _adopterRepo.Adopters.Count);
        }
    }
}