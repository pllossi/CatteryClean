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
        private readonly ICatRepository _repository ;
        public CatteryService(ICatRepository repository)
        {
            _repository = repository;
        }
        public void AddCat(CatDto dto)
        {
            if(dto is null) throw new ArgumentNullException(nameof(dto));
            var cat = dto.ToEntity();
            if(_repository.existsByCodeId(cat.CodeId)) throw new ArgumentException("A cat with the same CodeId already exists.");
            _repository.addCat(cat);
        }
        public void AdoptCat(string codeId,AdopterDTO adopter)
        {
            throw new NotImplementedException();
        }
        public void ReturnCat(string codeId)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<CatDto> GetAllCats()
        {
            var cats = _repository.getAllCats();
            return cats.Select(cats => cats.ToDTO());
        }


    }
}
