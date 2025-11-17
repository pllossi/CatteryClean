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
            _adopters = service.GetAllAdopter();

            // Popola i ComboBox quando la finestra viene costruita passando solo il service
            cmbCat.ItemsSource = _cats;
            cmbAdopter.ItemsSource = _adopters;
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

        private void MenuPrincipale_ViewCats_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var win = new CatsWindow(_service);
                win.Owner = this;
                win.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore aprendo la finestra Gatti: {ex.Message}");
            }
        }

        private void MenuPrincipale_AddCat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var win = new NewCatWindow(_service);
                win.Owner = this;
                win.ShowDialog();
                // dopo chiusura potremmo volere ricaricare la lista di gatti
                cmbCat.ItemsSource = _service.GetAllCats();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore aprendo la finestra Aggiungi Gatto: {ex.Message}");
            }
        }

        private void MenuPrincipale_ViewAdopters_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var win = new ViewAdoptersWindow(_service);
                win.Owner = this;
                win.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore aprendo la finestra Adottanti: {ex.Message}");
            }
        }

        private void MenuPrincipale_AddAdopter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var win = new NewAdopterWindow(_service);
                win.Owner = this;
                win.ShowDialog();
                // ricarica adottanti dopo possibile aggiunta
                cmbAdopter.ItemsSource = _service.GetAllAdopter();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore aprendo la finestra Aggiungi Adottante: {ex.Message}");
            }
        }

        private void MenuPrincipale_ViewAdoptions_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var win = new AdoptionsWindow(_service);
                win.Owner = this;
                win.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore aprendo la finestra Adozioni: {ex.Message}");
            }
        }

        private void MenuPrincipale_NewAdoption_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var win = new NewAdoptionWindow(_service);
                win.Owner = this;
                win.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore aprendo la finestra Nuova Adozione: {ex.Message}");
            }
        }
    }
}
