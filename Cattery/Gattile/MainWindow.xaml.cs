using GattileUI;
using System.Windows;
using System.ComponentModel;
using Application.UseCases;
using Application.DTO;
using Application.Interfaces;
using System;
using Infrastructure.Persistance.Repositories;

namespace GattileUI
{
    public partial class MainWindow : Window
    {
        private int _catCount;
        public int CatCount
        {
            get => _catCount;
            set
            {
                if (_catCount != value)
                {
                    _catCount = value;
                    OnPropertyChanged(nameof(CatCount));
                }
            }
        }
        public ICatRepository catRepository;
        public IAdopterRepository adopterRepository;
        public IAdoptionRepository adoptionRepository;
        public CatteryService catteryService;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            catRepository = new JsonCatRepository();
            adopterRepository = new JsonAdopterPersistance();
            adoptionRepository = new JsonAdoptionPersistance();
            catteryService = new CatteryService(catRepository, adoptionRepository, adopterRepository);
            UpdateCatCount();
        }
        public void UpdateCatCount()
        {
            CatCount= catteryService.GetAllCats().Count();
        }
        private void MenuPrincipale_ViewCats_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuPrincipale_AddCat_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuPrincipale_ViewAdopters_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuPrincipale_AddAdopter_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuPrincipale_ViewAdoptions_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuPrincipale_NewAdoption_Click(object sender, RoutedEventArgs e)
        {

        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
