using Application.DTO;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public static class CapMapper
    {
        public static CapDTO ToDTO(this Cap cap)
        {
            return new CapDTO(
                cap.Value
            );
        }
        public static Cap ToEntity(this CapDTO capDto)
        {
            return new Cap(
                capDto.Cap
            );
        }
    }
}
