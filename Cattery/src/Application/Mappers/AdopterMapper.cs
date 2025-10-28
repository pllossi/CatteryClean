using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Mappers
{
    public static class AdopterMapper
    {
        public static AdopterDTO ToDTO(this Adopter adopter)
        {
            return new AdopterDTO(
                adopter.Name,
                adopter.Surname,
                adopter.Phone.ToDTO(),
                adopter.Email.ToDTO(),
                adopter.Address,
                adopter.PostalAddress.ToDTO(),
                adopter.TaxId.ToDTO()
            );
        }
        public static Adopter ToEntity(this AdopterDTO adopterDto)
        {
            return new Adopter(
                adopterDto.Name,
                adopterDto.Surname,
                adopterDto.PhoneNumber.ToEntity(),
                adopterDto.Email.ToEntity(),
                adopterDto.TaxId.ToEntity(),
                adopterDto.Cap.ToEntity(),
                adopterDto.Address
            );
        }
    }
}
