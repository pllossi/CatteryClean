using System;
using Domain.Entities;
using Infrastructure.Persistance.DTO;

namespace Infrastructure.Persistance.Mapper
{
    public static class CatMapperPersistance
    {
        public static CatDtoPersistance ToPersistanceDto(this Cat cat)
        {
            if (cat is null) throw new ArgumentNullException(nameof(cat));

            return new CatDtoPersistance(
                cat.Name,
                cat.Breed,
                cat.Male,
                cat.Description,
                cat.ExitDate,
                cat.BirthDate,
                cat.CodeId
            );
        }

        public static Cat ToEntity(this CatDtoPersistance dto)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));

            var cat = new Cat(
                dto.Name,
                dto.Breed,
                dto.Male,
                dto.Description,
                dto.ExitDate,
                dto.BirthDate
            );

            cat.CodeId = dto.CodeId;
            return cat;
        }
    }
}