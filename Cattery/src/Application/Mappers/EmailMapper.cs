using Application.DTO;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public static class EmailMapper
    {
        public static EmailDTO ToDTO(this Email email)
        {
            if (email == null) 
                throw new ArgumentNullException(nameof(email));
            return new EmailDTO
            (
               email.Value
            );
        }
        public static Email ToEntity(this EmailDTO emailDto)
        {
            if (emailDto == null) 
                throw new ArgumentNullException(nameof(emailDto));
            if (string.IsNullOrWhiteSpace(emailDto.Email))
                throw new ArgumentException("Email value is null or empty", nameof(emailDto));
            return new Email
            (
                emailDto.Email
            );
        }
    }
}
