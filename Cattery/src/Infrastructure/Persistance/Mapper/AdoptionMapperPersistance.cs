using Application.Mappers;
using Domain.Entities;
using Infrastructure.Persistance.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Mapper
{
    public static class AdoptionMapperPersistance
    {
        public static AdoptionDtoPersistance ToPersistanceDto(this Adoption adoption)
        {
            if (adoption is null) throw new ArgumentNullException(nameof(adoption));

            return new AdoptionDtoPersistance(
                adoption.Cat.ToPersistanceDto(),
                adoption.Adopter.ToPersistanceDto(),
                adoption.AdoptionDate
            );
        }

        public static Adoption ToDomain(this AdoptionDtoPersistance dto)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));

            return new Adoption(
                dto.Adopter.ToDomain(),
                dto.Cat.ToEntity(),
                dto.AdoptionDate
            );
        }
    }
}