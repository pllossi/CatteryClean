using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Repositories
{
    public class JsonAdoptionPersistance : IAdoptionRepository
    {
        private List<Adoption> _cache = new();
        private readonly string _filePath;
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = true
        };
        public JsonAdoptionPersistance(string? filePath = null)
        {
            var baseFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Gattile");

            Directory.CreateDirectory(baseFolder);

            _filePath = filePath ?? Path.Combine(baseFolder, "adoption.json");
            EnsureLoaded();
        }

        public void addAdoption(Adoption adoption)
        {
            EnsureLoaded();
            _cache.Add(adoption);
            SaveDtos();
        }

        public void deleteAdoption(Adoption adoption)
        {
            EnsureLoaded();
            _cache.Remove(adoption);
            SaveDtos();
        }

        public IEnumerable<Adoption> getAllAdoptions()
        {
            EnsureLoaded();
            return _cache;
        }

        private void SaveDtos()
        {
            EnsureLoaded();
            var json = JsonSerializer.Serialize(_cache, _jsonOptions);
            File.WriteAllText(_filePath, json);
        }

        private void EnsureLoaded()
        {
            if (_cache.Count > 0) return;
            if (!File.Exists(_filePath))
            {
                return;
            }
            var json = File.ReadAllText(_filePath);
            var dtos = JsonSerializer.Deserialize<List<Adoption>>(json) ?? new List<Adoption>();
            //per ogni dto
            foreach (var dto in dtos)
            {
                Adoption adoption = dto;
                _cache.Add(adoption);
            }
        }

    }
}
