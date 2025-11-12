using System;
using System.Linq;
using System.Windows;
using Application.UseCases;
using Application.DTO;

namespace GattileUI
{
    /// <summary>
    /// Logica di interazione per EditCatWindow.xaml
    /// </summary>
    public partial class EditCatWindow : Window
    {
        private readonly CatteryService _service;
            private readonly string _originalCodeId;

        public EditCatWindow(CatteryService service, CatDto cat)
        {
            InitializeComponent();
            _service = service ?? throw new ArgumentNullException(nameof(service));
            if (cat is null) throw new ArgumentNullException(nameof(cat));

            // populate controls
            txtName.Text = cat.Name;
            txtBreed.Text = cat.Breed;
            chkIsMale.IsChecked = cat.IsMale;
            dpBirth.SelectedDate = cat.BirthDate;
            dpArrival.SelectedDate = cat.ArrivialDate;
            dpExit.SelectedDate = cat.ExitDate;
            txtCodeId.Text = cat.CodeId ?? string.Empty;
            txtDescription.Text = cat.Description ?? string.Empty;

            _originalCodeId = cat.CodeId ?? string.Empty;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // basic validation
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Il nome non può essere vuoto.");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtCodeId.Text))
                {
                    MessageBox.Show("Il Codice ID non può essere vuoto.");
                    return;
                }

                var newCatDto = new CatDto(
                    Name: txtName.Text,
                    Breed: txtBreed.Text ?? string.Empty,
                    IsMale: chkIsMale.IsChecked == true,
                    Description: string.IsNullOrWhiteSpace(txtDescription.Text) ? null : txtDescription.Text,
                    ExitDate: dpExit.SelectedDate,
                    ArrivialDate: dpArrival.SelectedDate,
                    BirthDate: dpBirth.SelectedDate,
                    CodeId: txtCodeId.Text
                );

                var allCats = _service.GetAllCats() ?? Enumerable.Empty<CatDto>();

                // se il nuovo CodeId è diverso e già esiste => errore
                if (!string.Equals(_originalCodeId, newCatDto.CodeId, StringComparison.OrdinalIgnoreCase)
                    && allCats.Any(c => string.Equals(c.CodeId, newCatDto.CodeId, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("Impossibile salvare: un gatto con lo stesso Codice ID esiste già.");
                    return;
                }

                // procedo: elimino il vecchio e aggiungo il nuovo
                // nota: se l'eliminazione o l'aggiunta fallisce verrà mostrato l'errore
                if (!string.IsNullOrWhiteSpace(_originalCodeId))
                {
                    _service.DeleteCat(_originalCodeId);
                }
                _service.AddCat(newCatDto);

                MessageBox.Show("Gatto aggiornato con successo.");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante il salvataggio: {ex.Message}");
            }
        }
    }
}
