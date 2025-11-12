using Application.DTO;
using Application.Interfaces;
using Application.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class CatteryService
    {
        private readonly ICatRepository _catRepository;
        private readonly IAdoptionRepository _adoptionRepository;
        private readonly IAdopterRepository _adopterRepository;
        public CatteryService(ICatRepository repository, IAdoptionRepository adoptionRepository,IAdopterRepository adopterRepository)
        {
            _catRepository = repository;
            _adoptionRepository = adoptionRepository;
            _adopterRepository = adopterRepository;
        }
        public void AddCat(CatDto? dto)
        {
            if(dto is null) throw new ArgumentNullException(nameof(dto));
            var cat = dto.ToEntity();
            if(_catRepository.existsByCodeId(cat.CodeId)) throw new ArgumentException("A cat with the same CodeId already exists.");
            _catRepository.addCat(cat);
        }
        public void AdoptCat(AdoptionDTO adoption)
        {
            if(adoption is null) throw new ArgumentNullException(nameof(adoption));
            var adoptio = adoption.ToEntity();
            if(!_catRepository.existsByCodeId(adoptio.Cat.CodeId)) throw new ArgumentException("The cat to be adopted does not exist.");
            _adoptionRepository.addAdoption(adoptio);
        }
        public void ReturnCat(string codeId)
        {
            if(string.IsNullOrWhiteSpace(codeId)) throw new ArgumentNullException(nameof(codeId));
            var adoptions = _adoptionRepository.getAllAdoptions();
            var adoption = adoptions.FirstOrDefault(a => a.Cat.CodeId == codeId);
            if(adoption is null) throw new ArgumentException("The cat with the given CodeId is not adopted.");
            _adoptionRepository.deleteAdoption(adoption);
        }
        public void RegisterAdopter(AdopterDTO adopterDto)
        {
            if(adopterDto is null) throw new ArgumentNullException(nameof(adopterDto));
            var adopter = adopterDto.ToEntity();
            if(_adopterRepository.getAllAdopters().Any(a=> a.TaxId==adopter.TaxId)) throw new ArgumentException("An adopter with the same TaxId already exists.");
            _adopterRepository.addAdopter(adopter);
        }
        public void DeleteCat(string codeId)
        {
            if(string.IsNullOrWhiteSpace(codeId)) throw new ArgumentNullException(nameof(codeId));
            var cat = _catRepository.getCatByCodeId(codeId);
            if(cat is null) throw new ArgumentException("The cat with the given CodeId does not exist.");
            _catRepository.deleteCat(cat);
        }
        public IEnumerable<CatDto> GetAllCatsAdopted()
        {
            var adoptions = _adoptionRepository.getAllAdoptions();
            var cats = adoptions.Select(a => a.Cat);
            return cats.Select(cat => cat.ToDTO());
        }
        public IEnumerable<AdopterDTO> GetAllAdopter()
        {
            var adopters = _adopterRepository.getAllAdopters();
            return adopters.Select(adopter => adopter.ToDTO());
        }
        public IEnumerable<CatDto> GetAllCats()
        {
            var cats = _catRepository.getAllCats();
            return cats.Select(cats => cats.ToDTO());
        }


    }
}
