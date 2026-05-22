using DimplomWork.Model;
using System;
using System.Collections.Generic;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DimplomWork.Pages
{
    /// <summary>
    /// Логика взаимодействия для TilesPage.xaml
    /// </summary>
    public partial class TilesPage : Page

    {

        public ListBox TilesListBox => TilesLb; // Для доступа из главного окна





        public string SelectedTone = "Все";
        public string SelectedStoneType = "Все";
        private List<Tiles> _allTiles;
        public TilesPage()
        {
            InitializeComponent();
            RefreshData();

        }
        public void RefreshData()
        {
            // Загружаем плитку вместе с тонами и типом камня
            _allTiles = App.context.Tiles
                .Include(t => t.Tones)
                .Include(t => t.Stone_Types)
                .ToList();

            // Применяем оба фильтра
            var filtered = _allTiles;

            // Фильтр по тону
            if (SelectedTone != "Все")
            {
                filtered = filtered.Where(t => t.Tones != null && t.Tones.Name == SelectedTone).ToList();
            }

            // Фильтр по типу камня
            if (SelectedStoneType != "Все")
            {
                filtered = filtered.Where(t => t.Stone_Types != null && t.Stone_Types.Name == SelectedStoneType).ToList();
            }

            TilesLb.ItemsSource = filtered; 
        }



        public void UpdateSearch(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                // Показываем все товары
                TilesLb.ItemsSource = App.context.Tiles.ToList();
            }
            else
            {
                // Фильтруем товары
                TilesLb.ItemsSource = App.context.Tiles
                    .Where(u => u.Name.ToLower().Contains(searchText.ToLower()))
                    .ToList();
            }

        }
    }
}
