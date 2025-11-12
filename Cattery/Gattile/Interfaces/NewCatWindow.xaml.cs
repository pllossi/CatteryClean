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
    }
}
