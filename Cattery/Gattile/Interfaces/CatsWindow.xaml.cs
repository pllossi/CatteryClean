using Application.DTO;
using System.Linq;
using System.Windows;
using Application.UseCases;

namespace GattileUI
{
    public partial class CatsWindow : Window
    {
        private CatteryService manager;

        public CatsWindow(CatteryService manager)
        {
            InitializeComponent();
            this.manager = manager;
            UpdateCatsList();
        }

        private void UpdateCatsList()
        {
            // Show both present and adopted cats
            lstCats.ItemsSource = null;
            lstCats.ItemsSource = manager.GetAllCats().ToList();
        }

        private void lstCats_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lstCats.SelectedItem is CatDto cat)
            {
                string info = $"Name: {cat.Name}\n" +
                              $"Breed: {cat.Breed}\n" +
                              $"Male: {(cat.IsMale ? "Yes" : "No")}\n" +
                              $"Description: {cat.Description}\n" +
                              $"Birth date: {cat.BirthDate?.ToShortDateString() ?? "N/A"}\n" +
                              $"Arrival date: {cat.ArrivialDate.ToString()}\n" +
                              $"Exit date: {cat.ExitDate?.ToShortDateString() ?? "Still present"}\n" +
                              $"Code ID: {cat.CodeId}";
                MessageBox.Show(info, "Cat Details", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
