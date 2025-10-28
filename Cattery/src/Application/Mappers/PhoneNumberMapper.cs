using Application.DTO;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public static class PhoneNumberMapper
    {
        public static PhoneNumberDTO ToDTO (this PhoneNumber phoneNumber)
        {
            return new PhoneNumberDTO(
                phoneNumber.Value
            );
        }
        public static PhoneNumber ToEntity(this PhoneNumberDTO phoneNumberDto)
        {
            return new PhoneNumber(
                phoneNumberDto.PhoneNumber
            );
        }
    }
}
