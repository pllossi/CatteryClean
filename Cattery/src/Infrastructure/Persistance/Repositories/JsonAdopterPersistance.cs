using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Entities;
using Application.DTO;
using Application.Mappers;

namespace Infrastructure.Persistance.Repositories
{
    public class JsonAdopterPersistance : IAdopterRepository
    {
        private readonly string _filePath = "adopters.json";
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = true
        };
        public JsonAdopterPersistance(string? filePath = null)
        {
            if (filePath != null)
                _filePath = filePath;
            EnsureLoaded();
        }
        private List<AdopterDTO> _cache = new List<AdopterDTO>();
        public void addAdopter(Adopter adopter)
        {
            EnsureLoaded();
            if(adopter == null)
                throw new ArgumentNullException(nameof(adopter));
            if(_cache.Any(a => a.ToEntity().TaxId == adopter.TaxId))
                throw new InvalidOperationException("Adopter with the same Tax ID already exists.");
            _cache.Add(adopter.ToDTO());
            AddAdopterToFile();
        }
        public IEnumerable<Adopter> getAllAdopters()
        {
            EnsureLoaded();
            List<Adopter> adopters = new List<Adopter>();
            foreach (var dto in _cache)
            {
                adopters.Add(dto.ToEntity());
            }
            return adopters;
        }
        private void EnsureLoaded()
        {
            if (_cache.Count > 0)
                return;
            if (!File.Exists(_filePath))
            {

                _cache = new List<AdopterDTO>();
                return;
            }
            var json = File.ReadAllText(_filePath);
            _cache = JsonSerializer.Deserialize<List<AdopterDTO>>(json, _jsonOptions) ?? new List<AdopterDTO>();
        }
        public IEnumerable<AdopterDTO> GetAllAdopters()
        {
            return getAllAdopters().Select(a => a.ToDTO());
        }
        private void AddAdopterToFile()
        {
            var json = JsonSerializer.Serialize(_cache, _jsonOptions);
            File.WriteAllText(_filePath, json);
        }

    }
}
