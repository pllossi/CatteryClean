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
    }
}
