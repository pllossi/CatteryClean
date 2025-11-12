using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using Application.UseCases;
using Application.DTO;

namespace GattileUI
{
    public partial class AdoptionsWindow : Window
    {
        private readonly CatteryService _manager;

        public AdoptionsWindow(CatteryService manager)
        {
            InitializeComponent();
            _manager = manager ?? throw new ArgumentNullException(nameof(manager));
            RefreshList();
            lstAdoptions.SelectionChanged += LstAdoptions_SelectionChanged;
        }

        private void RefreshList()
        {
            var adopted = _manager.GetAllCatsAdopted() ?? Enumerable.Empty<CatDto>();
            lstAdoptions.ItemsSource = adopted.Select(cat => cat.Name).ToList();
        }

        private void LstAdoptions_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lstAdoptions.SelectedItem is string catName)
            {
                var cat = _manager.GetAllCatsAdopted().FirstOrDefault(c => c.Name == catName);
                if (cat != null)
                {
                    var detailsWindow = new CatDetailsWindow(cat);
                    detailsWindow.Owner = this;
                    detailsWindow.ShowDialog();
                }
            }
        }

        private void BtnViewDetails_Click(object sender, RoutedEventArgs e)
        {
            LstAdoptions_SelectionChanged(sender,null);
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e) => Close();

        // --- Menu handlers (aprono le finestre corrispondenti) ---
        private void MenuPrincipale_ViewCats_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var win = new CatsWindow(_manager);
                win.Owner = this;
                win.ShowDialog();
                RefreshList();
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
                var win = new NewCatWindow(_manager);
                win.Owner = this;
                win.ShowDialog();
                RefreshList();
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
                var win = new ViewAdoptersWindow(_manager);
                win.Owner = this;
                win.ShowDialog();
                // non possiamo ottenere automaticamente la lista di AdopterDTO dal service (manca GetAllAdopters),
                // quindi se vuoi aggiornare qui, aggiungi GetAllAdopters() al service.
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
                var win = new NewAdopterWindow(_manager);
                win.Owner = this;
                win.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore aprendo la finestra Nuovo Adottante: {ex.Message}");
            }
        }

        private void MenuPrincipale_ViewAdoptions_Click(object sender, RoutedEventArgs e)
        {
            // siamo già in AdoptionsWindow: eventualmente aggiorniamo la lista
            RefreshList();
        }

        private void MenuPrincipale_NewAdoption_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // NewAdoptionWindow nella codebase potrebbe richiedere anche la lista di AdopterDTO.
                // Passo le informazioni disponibili; se serve, adatta il costruttore della finestra.
                var cats = _manager.GetAllCats() ?? Enumerable.Empty<CatDto>();
                IEnumerable<AdopterDTO> adopters = Enumerable.Empty<AdopterDTO>();
                var win = new NewAdoptionWindow(_manager, cats, adopters);
                win.Owner = this;
                win.ShowDialog();
                RefreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore aprendo la finestra Nuova Adozione: {ex.Message}");
            }
        }
    }
}
