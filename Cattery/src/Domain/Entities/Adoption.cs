using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Entities
{
    public class Adoption
    {
        private DateTime _adoptionDate;

        public Adopter Adopter;
        public Cat Cat;
        public DateTime AdoptionDate
        {
            get => _adoptionDate;
            private set
            {
                if (value == DateTime.Today.AddDays(1) || value > DateTime.Today || value < new DateTime(1900, 1, 1)) throw new ArgumentException();
                _adoptionDate = value;
            }
        }
        public DateTime FailedAdoptionStartDate
        {
            get => _failedAdoptionStartDate;
            set
            {
                if (value > FailedAdoptionEndDate) throw new ArgumentException();
                _failedAdoptionStartDate = value;
            }
        }
        private DateTime _failedAdoptionStartDate;

        public DateTime FailedAdoptionEndDate
        {
            get => _failedAdoptionEndDate;
            set
            {
                if (value < FailedAdoptionStartDate) throw new ArgumentException();
                _failedAdoptionEndDate = value;
            }
        }
        private DateTime _failedAdoptionEndDate;

        public Adoption(Adopter adopter, Cat cat, DateTime adoptionDate, DateTime failedAdoptionStartDate, DateTime failedAdoptionEndDate) : this(adopter, cat, adoptionDate)
        {
            FailedAdoptionStartDate = failedAdoptionStartDate;
            FailedAdoptionEndDate = failedAdoptionEndDate;
        }
        public Adoption(Adopter adopter, Cat cat, DateTime dateTime)
        {
            Adopter = adopter;
            Cat = cat;
            AdoptionDate = dateTime;
        }

    }
}
