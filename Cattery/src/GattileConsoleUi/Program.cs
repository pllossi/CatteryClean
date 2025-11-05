using Application.DTO;
using Application.Interfaces;
using Application.Mappers;
using Application.UseCases;
using Infrastructure.Persistance.Repositories;
using System.ComponentModel.DataAnnotations;

namespace GattileConsoleUi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var catRepo = new JsonCatRepository();
            var adopterRepo = new JsonAdopterPersistance();
            var adoptionRepo = new JsonAdoptionPersistance();

            var service = new CatteryService(catRepo, adoptionRepo, adopterRepo);

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("=== Gattile Console UI ===");
                Console.WriteLine("1) Elenca gatti");
                Console.WriteLine("2) Aggiungi gatto ");
                Console.WriteLine("3) Registra adottante ");
                Console.WriteLine("4) Registra adozione ");
                Console.WriteLine("5) Restituisci gatto (inserire codeId)");
                Console.WriteLine("0) Esci");
                Console.Write("Scelta: ");
                var choice = Console.ReadLine()?.Trim();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            var cats = service.GetAllCats();
                            if (!cats.Any())
                            {
                                Console.WriteLine("Nessun gatto presente.");
                            }
                            else
                            {
                                int i = 1;
                                foreach (var c in cats)
                                {
                                    Console.WriteLine($"{i++}. {c}");
                                }
                            }
                            break;

                        case "2":
                            // Raccolta interattiva dei campi del gatto
                            Console.Write("Nome: ");
                            var catName = Console.ReadLine()?.Trim() ?? string.Empty;

                            Console.Write("Razza: ");
                            var catBreed = Console.ReadLine()?.Trim() ?? string.Empty;

                            Console.Write("Maschio? (s/n): ");
                            var maleInput = Console.ReadLine()?.Trim().ToLower() ?? "s";
                            var catMale = maleInput == "s" || maleInput == "si" || maleInput == "y" || maleInput == "yes";

                            Console.Write("Descrizione (lascia vuoto se nessuna): ");
                            var catDescription = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(catDescription)) catDescription = null;

                            Console.Write("Data di nascita (yyyy-MM-dd, opzionale): ");
                            DateTime? catBirth = null;
                            var birthInput = Console.ReadLine()?.Trim();
                            if (!string.IsNullOrWhiteSpace(birthInput) && DateTime.TryParse(birthInput, out var parsedBirth))
                            {
                                catBirth = parsedBirth;
                            }
                            var catDto = new CatDto
                            (
                                Name: catName,
                                Breed: catBreed,
                                IsMale: catMale,
                                Description: catDescription,
                                ExitDate: null,
                                BirthDate: catBirth,
                                CodeId: null
                            );

                            service.AddCat(catDto);
                            Console.WriteLine("Gatto aggiunto (richiamata AddCat).");
                            break;

                        case "3":
                            // Raccolta interattiva dei campi dell'adottante
                            Console.Write("Nome: ");
                            var adopterName = Console.ReadLine()?.Trim() ?? string.Empty;

                            Console.Write("Cognome: ");
                            var adopterSurname = Console.ReadLine()?.Trim() ?? string.Empty;

                            Console.Write("Telefono: ");
                            var adopterPhone = Console.ReadLine()?.Trim();
                            if (string.IsNullOrWhiteSpace(adopterPhone)) adopterPhone = null;

                            Console.Write("Email: ");
                            var adopterEmail = Console.ReadLine()?.Trim();
                            if (string.IsNullOrWhiteSpace(adopterEmail)) adopterEmail = null;

                            Console.Write("Codice fiscale (TaxId): ");
                            var adopterTaxId = Console.ReadLine()?.Trim() ?? string.Empty;

                            Console.Write("CAP (PostalCode): ");
                            var adopterPostal = Console.ReadLine()?.Trim();
                            if (string.IsNullOrWhiteSpace(adopterPostal)) adopterPostal = null;

                            Console.Write("Città: ");
                            var adopterCity = Console.ReadLine()?.Trim();
                            if (string.IsNullOrWhiteSpace(adopterCity)) adopterCity = null;

                            Console.Write("Indirizzo: ");
                            var adopterAddress = Console.ReadLine()?.Trim();
                            if (string.IsNullOrWhiteSpace(adopterAddress)) adopterAddress = null;
                            
                            var adopterNumberPhone = new PhoneNumberDTO
                            (
                                PhoneNumber: adopterPhone
                            );
                            var emailDto = new EmailDTO
                            (
                                Email: adopterEmail
                            );
                            var taxIdDto = new TaxIdDTO
                            (
                                TaxId: adopterTaxId
                            );
                            var capDto = new CapDTO
                            (
                                Cap: adopterPostal
                            );


                            var adopterDto = new AdopterDTO
                            (
                                Name:adopterName,
                                Surname: adopterSurname,
                                PhoneNumber: adopterNumberPhone,
                                Email: emailDto,
                                TaxId: taxIdDto,
                                Cap: capDto,
                                Address: adopterAddress
                            );
                            service.RegisterAdopter(adopterDto);
                            Console.WriteLine("Adottante registrato (richiamata RegisterAdopter).");
                            break;

                        case "4":
                            // Raccolta interattiva dei campi dell'adozione
                            Console.Write("TaxId (o identificativo adottante): ");
                            var adopTax = Console.ReadLine()?.Trim() ?? string.Empty;

                            Console.Write("CodeId del gatto da adottare: ");
                            var adopCatCode = Console.ReadLine()?.Trim() ?? string.Empty;

                            Console.Write("Data adozione (yyyy-MM-dd, opzionale): ");
                            DateTime? adoptionDate = null;
                            var adoptionDateInput = Console.ReadLine()?.Trim();
                            if (!string.IsNullOrWhiteSpace(adoptionDateInput) && DateTime.TryParse(adoptionDateInput, out var parsedAdp))
                            {
                                adoptionDate = parsedAdp;
                            }
                            var adopter= adopterRepo.getAllAdopters().FirstOrDefault(a => a.TaxId.ToString() == adopTax);

                            var adoptionDto = new AdoptionDTO
                            (
                                Cat: catRepo.getAllCats().First(c => c.CodeId == adopCatCode).ToDTO(),
                                Adopter: adopter.ToDTO(),
                                AdoptionDate: adoptionDate ?? DateTime.Now
                            );

                            service.AdoptCat(adoptionDto);
                            Console.WriteLine("Adozione registrata (richiamata AdoptCat).");
                            break;

                        case "5":
                            Console.Write("Inserisci codeId del gatto da restituire: ");
                            var codeId = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(codeId))
                            {
                                Console.WriteLine("codeId vuoto, operazione annullata.");
                                break;
                            }
                            service.ReturnCat(codeId.Trim());
                            Console.WriteLine("Richiamata ReturnCat su codeId: " + codeId);
                            break;

                        case "0":
                            Console.WriteLine("Uscita.");
                            return;

                        default:
                            Console.WriteLine("Scelta non valida.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Errore: " + ex.Message);
                }
            }
        }
    }
}
