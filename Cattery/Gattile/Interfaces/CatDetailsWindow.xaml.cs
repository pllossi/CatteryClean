using Application.DTO;
using Application.UseCases;
using System;
using System.Windows;

namespace GattileUI
{
    /// <summary>
    /// Interaction logic for CatDetailsWindow.xaml
    /// </summary>
    public partial class CatDetailsWindow : Window
    {
        CatteryService _catteryService;
        public CatDetailsWindow(CatDto cat, CatteryService catteryService)
        {
            InitializeComponent();
            DataContext = new CatDetailsViewModel(cat);
            _catteryService = catteryService;
        }

        // XAML usa BtnChiudi_Click
        private void BtnChiudi_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void MenuPrincipale_ViewCats_Click(object sender, RoutedEventArgs e)
        {
            var win = new CatsWindow(_catteryService);
            win.ShowDialog();
        }

        private void MenuPrincipale_AddCat_Click(object sender, RoutedEventArgs e)
        {
            var newCatWindow = new NewCatWindow(_catteryService);
            newCatWindow.ShowDialog();
        }

        private void MenuPrincipale_ViewAdopters_Click(object sender, RoutedEventArgs e)
        {
            var win = new ViewAdoptersWindow(_catteryService);
            win.ShowDialog();
        }

        private void MenuPrincipale_AddAdopter_Click(object sender, RoutedEventArgs e)
        {
            var win = new NewAdopterWindow(_catteryService);
            win.ShowDialog();
        }

        private void MenuPrincipale_ViewAdoptions_Click(object sender, RoutedEventArgs e)
        {
            var win = new AdoptionsWindow(_catteryService);
            win.ShowDialog();
        }

        private void MenuPrincipale_NewAdoption_Click(object sender, RoutedEventArgs e)
        {
            var win = new NewAdoptionWindow(_catteryService);
            win.ShowDialog();
        }

    }

    public class CatDetailsViewModel
    {
        public string Nome { get; }
        public string Razza { get; }
        public string Sesso { get; }
        public DateTime? DataNascita { get; }
        public string? Descrizione { get; }
        public string CodiceIdentificativo { get; }

        public CatDetailsViewModel(CatDto cat)
        {
            Nome = cat.Name;
            Razza = cat.Breed;
            Sesso = cat.IsMale ? "Maschio" : "Femmina";
            DataNascita = cat.BirthDate;
            Descrizione = cat.Description;
            CodiceIdentificativo = cat.CodeId ?? string.Empty;
        }
    }
}
