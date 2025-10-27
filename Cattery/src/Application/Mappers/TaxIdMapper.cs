using Application.DTO;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public static class TaxIdMapper
    {
        public static TaxIdDTO ToDTO(this string taxId)
        {
            return new TaxIdDTO(
                taxId
            );
        }
        public static TaxId ToEntity(this TaxIdDTO taxIdDto)
        {
            return new TaxId(
                taxIdDto.TaxId
            );
        }

    }
}
