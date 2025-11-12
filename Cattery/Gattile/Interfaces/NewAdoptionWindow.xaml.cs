using System;
using System.Collections.Generic;
using System.Windows;
using Application.DTO;
using Application.UseCases;

namespace GattileUI
{
    public partial class NewAdoptionWindow : Window
    {
        private readonly CatteryService _service;
        private readonly IEnumerable<CatDto> _cats;
        private readonly IEnumerable<AdopterDTO> _adopters;

        // Ricevo le liste dal chiamante: il service espone GetAllCats(), ma non GetAllAdopters(),
        // quindi conviene che il chiamante fornisca gli adottanti.
        public NewAdoptionWindow(CatteryService service, IEnumerable<CatDto> cats, IEnumerable<AdopterDTO> adopters)
        {
            InitializeComponent();
            _service = service;
            _cats = cats;
            _adopters = adopters;

            cmbCat.ItemsSource = _cats;
            cmbAdopter.ItemsSource = _adopters;
        }
        public NewAdoptionWindow(CatteryService service)
        {
            InitializeComponent();
            _service = service;
            _cats = service.GetAllCats();
            _adopters = service.Get
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (cmbCat.SelectedItem is CatDto cat && cmbAdopter.SelectedItem is AdopterDTO adopter)
            {
                var adoptionDto = new AdoptionDTO(cat, adopter, DateTime.Now);
                try
                {
                    _service.AdoptCat(adoptionDto);
                    MessageBox.Show("Adozione registrata.");
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Errore: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Seleziona gatto e adottante.");
            }
        }
    }
}
