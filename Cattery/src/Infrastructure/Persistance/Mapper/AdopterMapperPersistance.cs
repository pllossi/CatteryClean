using System;
using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Persistance.DTO;

namespace Infrastructure.Persistance.Mapper
{
    public static class AdopterMapperPersistance
    {
        public static AdopterDtoPersistance ToPersistanceDto(this Adopter adopter)
        {
            if (adopter is null) throw new ArgumentNullException(nameof(adopter));

            return new AdopterDtoPersistance(
                adopter.Name,
                adopter.Surname,
                new PhoneNumberDtoPersistance(adopter.Phone.ToString()),
                new EmailDtoPersistance(adopter.Email.ToString()),
                adopter.Address,
                new CapDtoPersistance(adopter.PostalAddress.ToString()),
                new TaxIdDtoPersistance(adopter.TaxId.ToString())
            );
        }

        public static Adopter ToDomain(this AdopterDtoPersistance dto)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));

            var phone = dto.PhoneNumber is not null ? new PhoneNumber(dto.PhoneNumber.PhoneNumber) : null;
            var email = dto.Email is not null ? new Email(dto.Email.Email) : null;
            var taxId = new TaxId(dto.TaxId.TaxId);
            var cap = new Cap(dto.Cap.Cap);

            return new Adopter(
                dto.Name,
                dto.Surname,
                phone,
                email,
                taxId,
                cap,
                dto.Address
            );
        }
    }
}