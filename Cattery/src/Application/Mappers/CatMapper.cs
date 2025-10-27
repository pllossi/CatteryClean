using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Application.DTO;

namespace Application.Mappers
{
    public static class CatMapper
    {
        public static CatDto ToDTO(this Cat cat)
        {
            return new CatDto
            (
                cat.Name,
                cat.Breed,
                cat.Male,
                cat.Description,
                cat.ExitDate,
                cat.BirthDate,
                cat.CodeId
            );
        }
        public static Cat ToEntity(this CatDto catDto)
        {
            return new Cat
            (
                catDto.Name,
                catDto.Breed,
                catDto.Male,
                catDto.Description,
                catDto.ExitDate,
                catDto.BirthDate
            );
        }
    }
}
