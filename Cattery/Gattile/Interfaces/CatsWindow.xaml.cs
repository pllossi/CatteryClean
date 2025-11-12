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
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAddCat_Click(object sender, RoutedEventArgs e)
        {
            var newCatWindow = new NewCatWindow(manager);
            newCatWindow.ShowDialog();
            UpdateCatsList();
        }

        private void btnDeleteCat_Click(object sender, RoutedEventArgs e)
        {
            if (lstCats.SelectedItem is CatDto selectedCat)
            {
                var result = MessageBox.Show($"Are you sure you want to delete {selectedCat.Name}?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    manager.DeleteCat(selectedCat.CodeId);
                    UpdateCatsList();
                }
            }
            else
            {
                MessageBox.Show("Please select a cat to delete.", "No Cat Selected", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void btnChangeInfos_Click(object sender, RoutedEventArgs e)
        {
            if (lstCats.SelectedItem is CatDto selectedCat)
            {
                var editCatWindow = new EditCatWindow(manager, selectedCat.CodeId);
                editCatWindow.ShowDialog();
                UpdateCatsList();
            }
            else
            {
                MessageBox.Show("Please select a cat to edit.", "No Cat Selected", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }

        private void btnAdopt_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
