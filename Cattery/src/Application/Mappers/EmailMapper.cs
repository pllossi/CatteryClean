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
            if (email == null) return null;
            return new EmailDTO
            (
               email.Value
            );
        }
    }
}
