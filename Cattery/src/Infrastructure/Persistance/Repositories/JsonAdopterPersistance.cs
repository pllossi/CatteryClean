using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Persistance.Repositories
{
    public class JsonAdopterPersistance : IAdopterRepository
    {
        private readonly string _filePath = "adopters.json";
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = true
        };
        private List<Adopter> _cache = new();
        public void addAdopter(Adopter adopter)
        {
            EnsureLoaded();
            if(adopter == null)
                throw new ArgumentNullException(nameof(adopter));
            if(_cache.Any(a => a.TaxId == adopter.TaxId))
                throw new InvalidOperationException("Adopter with the same Tax ID already exists.");
            _cache.Add(adopter);
            AddAdopterToFile();
        }
        public IEnumerable<Adopter> getAllAdopters()
        {
            EnsureLoaded();
            return _cache;
        }
        private void EnsureLoaded()
        {
            if (_cache.Count > 0)
                return;
            if (!File.Exists(_filePath))
            {
                _cache = new List<Adopter>();
                return;
            }
            var json = File.ReadAllText(_filePath);
            _cache = JsonSerializer.Deserialize<List<Adopter>>(json, _jsonOptions) ?? new List<Adopter>();
        }
        private void AddAdopterToFile()
        {
            var json = JsonSerializer.Serialize(_cache, _jsonOptions);
            File.WriteAllText(_filePath, json);
        }

    }
}
