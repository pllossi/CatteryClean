using System;
using System.Windows;
using Application.DTO;
using Application.UseCases;

namespace GattileUI
{
    public partial class NewAdopterWindow : Window
    {
        private readonly CatteryService _service;

        public NewAdopterWindow(CatteryService service)
        {
            InitializeComponent();
            _service = service;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtSurname.Text) ||
                    (string.IsNullOrWhiteSpace(txtPhone.Text) && string.IsNullOrWhiteSpace(txtEmail.Text)))
                {
                    throw new ArgumentException();
                }

                // I DTO richiedono oggetti non-null; se il campo è vuoto creo DTO con stringa vuota
                var phoneDto = string.IsNullOrWhiteSpace(txtPhone.Text) ? new PhoneNumberDTO("") : new PhoneNumberDTO(txtPhone.Text);
                var emailDto = string.IsNullOrWhiteSpace(txtEmail.Text) ? new EmailDTO("") : new EmailDTO(txtEmail.Text);

                // Campi non presenti nella UI: Address, Cap, TaxId -> valorizzati con stringhe vuote
                var adopterDto = new AdopterDTO(
                    Name: txtName.Text,
                    Surname: txtSurname.Text,
                    PhoneNumber: phoneDto,
                    Email: emailDto,
                    Address: string.Empty,
                    Cap: new CapDTO(string.Empty),
                    TaxId: new TaxIdDTO(string.Empty)
                );

                // Chiamata coerente con la firma del service
                _service.RegisterAdopter(adopterDto);

                MessageBox.Show("Adottante registrato con successo.");
                Close();
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Controlla i campi inseriti.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore: {ex.Message}");
            }
        }

        // Handlers per i MenuItem presenti nel XAML (implementati come in MainWindow, ma usando _service)
        private void MenuPrincipale_ViewCats_Click(object sender, RoutedEventArgs e)
        {
            var win = new CatsWindow(_service);
            win.ShowDialog();
        }

        private void MenuPrincipale_AddCat_Click(object sender, RoutedEventArgs e)
        {
            var newCatWindow = new NewCatWindow(_service);
            newCatWindow.ShowDialog();
        }

        private void MenuPrincipale_ViewAdopters_Click(object sender, RoutedEventArgs e)
        {
            var win = new ViewAdoptersWindow(_service);
            win.ShowDialog();
        }

        private void MenuPrincipale_AddAdopter_Click(object sender, RoutedEventArgs e)
        {
            var win = new NewAdopterWindow(_service);
            win.ShowDialog();
        }

        private void MenuPrincipale_ViewAdoptions_Click(object sender, RoutedEventArgs e)
        {
            var win = new AdoptionsWindow(_service);
            win.ShowDialog();
        }

        private void MenuPrincipale_NewAdoption_Click(object sender, RoutedEventArgs e)
        {
            var win = new NewAdoptionWindow(_service);
            win.ShowDialog();
        }
    }
}
