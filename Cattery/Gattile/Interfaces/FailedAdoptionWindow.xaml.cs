using System;
using System.Collections.Generic;
using System.Windows;
using Application.DTO;
using Application.UseCases;

namespace GattileUI
{
    public partial class FailedAdoptionWindow : Window
    {
        private readonly CatteryService _service;

        public FailedAdoptionWindow(CatteryService service)
        {
            InitializeComponent();
            _service = service;
        }

        private void btnRegistraFallita_Click(object sender, RoutedEventArgs e)
        {
            if (cmbCat.SelectedItem is CatDto cat && dpStart.SelectedDate.HasValue && dpEnd.SelectedDate.HasValue)
            {
                try
                {
                    _service.ReturnCat(cat.CodeId);
                    MessageBox.Show("Adozione fallita registrata.");
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Errore: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Compila tutti i campi.");
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
