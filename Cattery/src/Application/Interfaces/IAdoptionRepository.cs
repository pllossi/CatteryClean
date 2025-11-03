using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAdoptionRepository
    {
        void addAdoption(Adoption adoption);
        void deleteAdoption(Adoption adoption);
        IEnumerable<Adoption> getAllAdoptions();
    }
}
