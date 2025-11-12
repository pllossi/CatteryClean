using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Application.DTO;
using Application.UseCases;

namespace GattileUI
{
    /// <summary>
    /// Logica di interazione per ViewAdoptersWindow.xaml
    /// </summary>
    public partial class ViewAdoptersWindow : Window
    {
        private readonly CatteryService _service;
        private List<AdopterDTO> _adopters;

        public ViewAdoptersWindow(CatteryService service, IEnumerable<AdopterDTO>? adopters = null)
        {
            InitializeComponent();
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _adopters = adopters?.ToList() ?? new List<AdopterDTO>();
            lstAdopters.ItemsSource = _adopters;

            // doppio click per dettagli
            lstAdopters.MouseDoubleClick += (s, e) =>
            {
                if (lstAdopters.SelectedItem is AdopterDTO selected)
                    ShowDetails(selected);
            };
        }

        // Permette al chiamante di aggiornare la lista dopo modifiche esterne
        public void Refresh(IEnumerable<AdopterDTO> adopters)
        {
            _adopters = adopters?.ToList() ?? new List<AdopterDTO>();
            lstAdopters.ItemsSource = _adopters;
        }

        private void BtnDetails_Click(object sender, RoutedEventArgs e)
        {
            if (lstAdopters.SelectedItem is AdopterDTO selected)
            {
                ShowDetails(selected);
            }
            else
            {
                MessageBox.Show("Seleziona un adottante per visualizzarne i dettagli.", "Dettagli adottante", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ShowDetails(AdopterDTO a)
        {
            var phone = a.PhoneNumber?.PhoneNumber ?? string.Empty;
            var email = a.Email?.Email ?? string.Empty;
            var taxId = a.TaxId?.TaxId ?? string.Empty;
            var cap = a.Cap?.Cap ?? string.Empty;
            var sb = new StringBuilder();
            sb.AppendLine($"{a.Name} {a.Surname}");
            if (!string.IsNullOrWhiteSpace(phone)) sb.AppendLine($"Telefono: {phone}");
            if (!string.IsNullOrWhiteSpace(email)) sb.AppendLine($"Email: {email}");
            if (!string.IsNullOrWhiteSpace(taxId)) sb.AppendLine($"TaxId: {taxId}");
            if (!string.IsNullOrWhiteSpace(cap)) sb.AppendLine($"CAP: {cap}");
            if (!string.IsNullOrWhiteSpace(a.Address)) sb.AppendLine($"Indirizzo: {a.Address}");

            MessageBox.Show(sb.ToString(), "Dettagli adottante", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var wnd = new NewAdopterWindow(_service);
                wnd.Owner = this;
                wnd.ShowDialog();
                // non posso ricavare automaticamente la nuova lista dal service (manca GetAllAdopters),
                // quindi lascio il chiamante responsabile di invocare Refresh(...) dopo aver aggiornato i dati persistenti.
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore: {ex.Message}");
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e) => Close();

        // Menu handlers: stub semplici per evitare errori di binding XAML;
        // se vuoi li reindirizzo ai comportamenti di MainWindow, dimmi quale preferisci.
        private void MenuPrincipale_ViewCats_Click(object sender, RoutedEventArgs e) { /* Implementa se necessario */ }
        private void MenuPrincipale_AddCat_Click(object sender, RoutedEventArgs e) { /* Implementa se necessario */ }
        private void MenuPrincipale_ViewAdopters_Click(object sender, RoutedEventArgs e) { /* Implementa se necessario */ }
        private void MenuPrincipale_AddAdopter_Click(object sender, RoutedEventArgs e) { /* Implementa se necessario */ }
        private void MenuPrincipale_ViewAdoptions_Click(object sender, RoutedEventArgs e) { /* Implementa se necessario */ }
        private void MenuPrincipale_NewAdoption_Click(object sender, RoutedEventArgs e) { /* Implementa se necessario */ }
    }
}
