using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.DTO
{
    public record AdoptionDtoPersistance
        (
        CatDtoPersistance Cat,
        AdopterDtoPersistance Adopter,
        DateTime AdoptionDate
        );    
}
