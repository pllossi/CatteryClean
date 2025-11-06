using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;

namespace Infrastructure.Persistance.DTO
{
    public record AdopterDtoPersistance(
        string Name,
        string Surname,
        string PhoneNumber,
        string Email,
        string Address,
        string Cap,
        string TaxId
        );
}
