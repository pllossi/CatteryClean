using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistance.DTO;
using Infrastructure.Persistance.Mapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text.Json;

namespace Infrastructure.Persistance.Repositories
{
    public class JsonCatRepository : ICatRepository
    {
        private List<Cat> _cache = new();
        private bool _initialized = false;
        private readonly string _filePath;
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = true
        };

        private void EnsureLoaded()
        {
            if (_initialized) return;

            if (!File.Exists(_filePath))
            {
                _initialized = true;
                return;
            }

            var json = File.ReadAllText(_filePath);
            var dtos = JsonSerializer.Deserialize<List<CatDtoPersistance>>(json) ?? new List<CatDtoPersistance>();

            //per ogni dto
            foreach (var dto in dtos)
            {
                
                Cat cat = dto.ToEntity();
                _cache.Add(cat);
            }

            _initialized = true;
        }

        public JsonCatRepository(string? filePath = null)
        {
            var baseFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Gattile");

            Directory.CreateDirectory(baseFolder);

            _filePath = filePath ?? Path.Combine(baseFolder, "cats.json");
        }

        public void addCat(Cat cat)
        {
            EnsureLoaded();
            if (cat is null) throw new ArgumentNullException(nameof(cat));

            _cache.Add(cat);
            SaveDtos();
        }

        public IEnumerable<Cat> getAllCats()
        { 
            foreach (var dto in _cache)
            {
                yield return dto;
            }
        }

        private void SaveDtos()
        {
            var directory = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var json = JsonSerializer.Serialize(_cache, _jsonOptions);
            File.WriteAllText(_filePath, json);
        }

        public bool existsByCodeId(string codeId)
        {
            return getAllCats().Any(c => c.CodeId == codeId);
        }
    }
}
