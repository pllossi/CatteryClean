using Application;
using Application.DTO;
using System;
using Infrastructure;
using System.Windows;
using Application.UseCases;

namespace GattileUI
{
    public partial class NewCatWindow : Window
    {
        private CatteryService manager;

        public NewCatWindow(CatteryService manager)
        {
            InitializeComponent();
            this.manager = manager;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtName.Text == "" || txtBreed.Text == "" || !dpBirthDate.SelectedDate.HasValue)
                {
                    throw new ArgumentException();
                }
                var newCat = new CatDto(
                    Name:txtName.Text,
                    Breed:txtBreed.Text,
                    IsMale:chkMale.IsChecked == true,
                    Description:txtDescription.Text,
                    ExitDate:null,
                    ArrivialDate:DateTime.Today,
                    BirthDate: dpBirthDate.SelectedDate,
                    null
                );
                manager.AddCat(newCat);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Check the entered fields.");
            }
            Close();
        }

        private void MenuPrincipale_ViewCats_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var win = new CatsWindow(manager);
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
                var win = new NewCatWindow(manager);
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
                var win = new ViewAdoptersWindow(manager);
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
                var win = new NewAdopterWindow(manager);
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
                var win = new AdoptionsWindow(manager);
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
                var win = new NewAdoptionWindow(manager);
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
