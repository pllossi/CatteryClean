using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Domain.Entities;

namespace Application.Mappers
{
    public static class AdoptionMapper
    {
        public static AdoptionDTO ToDTO(this Adoption adoption)
        {
            return new AdoptionDTO(
                adoption.Cat.ToDTO(),
                adoption.Adopter.ToDTO(),
                adoption.AdoptionDate
            );
        }
        public static Adoption ToEntity(this AdoptionDTO adoptionDto)
        {
            return new Adoption(
                adoptionDto.Adopter.ToEntity(),
                adoptionDto.Cat.ToEntity(),
                adoptionDto.AdoptionDate
            );
        }
    }
}
