using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Domain.Entities;

namespace Application.Mappers
{
    public static class AdopterMapper
    {
        public static AdopterDTO ToDTO(this Adopter adopter)
        {
            return new AdopterDTO(
                adopter.Name,
                adopter.Surname,
                adopter.TaxId.ToDTO(),
                adopter.Address.ToDTO(),
                adopter.PhoneNumber,
                adopter.Email
            );
        }
        public static Adopter ToEntity(this AdopterDTO adopterDto)
        {
            return new Adopter(
                adopterDto.Name,
                adopterDto.Surname,
                adopterDto.TaxId.ToEntity(),
                adopterDto.Address.ToEntity(),
                adopterDto.PhoneNumber,
                adopterDto.Email
            );
        }
    }
}
