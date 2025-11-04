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
        private readonly IAdoptionRepository _adopterRepository;
        public CatteryService(ICatRepository repository, IAdoptionRepository adoptionRepository)
        {
            _catRepository = repository;
            _adopterRepository = adoptionRepository;
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
            _adopterRepository.addAdoption(adoptio);
        }
        public void ReturnCat(string codeId)
        {
            if(string.IsNullOrWhiteSpace(codeId)) throw new ArgumentNullException(nameof(codeId));
            var adoptions = _adopterRepository.getAllAdoptions();
            var adoption = adoptions.FirstOrDefault(a => a.Cat.CodeId == codeId);
            if(adoption is null) throw new ArgumentException("The cat with the given CodeId is not adopted.");
            _adopterRepository.deleteAdoption(adoption);
        }
        public IEnumerable<CatDto> GetAllCats()
        {
            var cats = _catRepository.getAllCats();
            return cats.Select(cats => cats.ToDTO());
        }


    }
}
