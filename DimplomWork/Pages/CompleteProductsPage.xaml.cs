using DimplomWork.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DimplomWork.Pages
{
    /// <summary>
    /// Логика взаимодействия для CompleteProductsPage.xaml
    /// </summary>
    public partial class CompleteProductsPage : Page
    {

        public string SelectedTone = "Все";
        public string SelectedStoneType = "Все";
        private List<Complete_Products> _allProducts;

        public CompleteProductsPage()
        {
            InitializeComponent();
            RefreshData();
        }


        public void RefreshData()
        {
            // Загружаем товары вместе с тонами и типом камня
            _allProducts = App.context.Complete_Products
                .Include(p => p.Tones)
                .Include(p => p.Stone_Types)
                .ToList();

            // Применяем оба фильтра
            var filtered = _allProducts;

            // Фильтр по тону
            if (SelectedTone != "Все")
            {
                filtered = filtered.Where(p => p.Tones != null && p.Tones.Name == SelectedTone).ToList();
            }

            // Фильтр по типу камня
            if (SelectedStoneType != "Все")
            {
                filtered = filtered.Where(p => p.Stone_Types != null && p.Stone_Types.Name == SelectedStoneType).ToList();
            }

            Complete_ProductsLb.ItemsSource = filtered;
        }
    





        // Один метод для обновления поиска
        public void UpdateSearch(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                // Показываем все товары
                Complete_ProductsLb.ItemsSource = App.context.Complete_Products.ToList();
            }
            else
            {
                // Фильтруем товары
                Complete_ProductsLb.ItemsSource = App.context.Complete_Products.Where(u => u.Name.ToLower().Contains(searchText.ToLower())).ToList();
            }

        }
    }
}