using Application.DTO;
using System;
using System.Windows;

namespace GattileUI
{
    /// <summary>
    /// Interaction logic for CatDetailsWindow.xaml
    /// </summary>
    public partial class CatDetailsWindow : Window
    {
        public CatDetailsWindow(CatDto cat)
        {
            InitializeComponent();
            DataContext = new CatDetailsViewModel(cat);
        }

        // XAML usa BtnChiudi_Click
        private void BtnChiudi_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
