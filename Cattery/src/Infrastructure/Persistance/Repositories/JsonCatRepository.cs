using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistance.DTO;
using Infrastructure.Persistance.Mapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Infrastructure.Persistance.Repositories
{
    public class JsonCatRepository : ICatRepository
    {
        private readonly string _filePath;
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = true
        };

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
            if (cat is null) throw new ArgumentNullException(nameof(cat));

            var list = LoadDtos().ToList();
            list.Add(cat.ToPersistanceDto());
            SaveDtos(list);
        }

        public IEnumerable<Cat> getAllCats()
        {
            var dtos = LoadDtos();
            foreach (var dto in dtos)
            {
                yield return dto.ToDomain();
            }
        }

        private IEnumerable<CatDtoPersistance> LoadDtos()
        {
            if (!File.Exists(_filePath))
                return Enumerable.Empty<CatDtoPersistance>();

            try
            {
                var json = File.ReadAllText(_filePath);
                return string.IsNullOrWhiteSpace(json)
                    ? Enumerable.Empty<CatDtoPersistance>()
                    : (JsonSerializer.Deserialize<List<CatDtoPersistance>>(json, _jsonOptions)
                       ?? Enumerable.Empty<CatDtoPersistance>());
            }
            catch
            {
                // In caso di file corrotto restituiamo lista vuota
                return Enumerable.Empty<CatDtoPersistance>();
            }
        }

        private void SaveDtos(IEnumerable<CatDtoPersistance> dtos)
        {
            var json = JsonSerializer.Serialize(dtos, _jsonOptions);
            File.WriteAllText(_filePath, json);
        }

        public bool existsByCodeId(string codeId)
        {
            return getAllCats().Any(c => c.CodeId == codeId);
        }
    }
}
