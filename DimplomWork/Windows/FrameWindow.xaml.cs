using DimplomWork.Model;
using DimplomWork.Pages;
using DimplomWork.Windows;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace DimplomWork
{
    /// <summary>
    /// Логика взаимодействия для FrameWindow.xaml
    /// </summary>
    public partial class FrameWindow : Window
    {

        public FrameWindow()
        {   
            InitializeComponent();

            // Заполняем комбобокс тонов
            TonesCmb.Items.Add("Все");
            TonesCmb.Items.Add("Светлый");
            TonesCmb.Items.Add("Темный");
            TonesCmb.SelectedIndex = 0;

            // Заполняем комбобокс типов камня
            StoneTypesCmb.Items.Add("Все");
            StoneTypesCmb.Items.Add("Мрамор");
            StoneTypesCmb.Items.Add("Оникс");
            StoneTypesCmb.Items.Add("Гранит");
            StoneTypesCmb.Items.Add("Акриловый камень");
            StoneTypesCmb.Items.Add("Искусственный камень");
            StoneTypesCmb.Items.Add("Кварцит");
            StoneTypesCmb.SelectedIndex = 0;

            //TonesCmb.ItemsSource = App.context.Tones.ToList();
            //StoneTypesCmb.ItemsSource = App.context.Stone_Types.ToList();

        }

        private void Complete_ProductsFrameBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CompleteProductsPage());
        }

        private void TilesFrameBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new TilesPage());
        }

 

        private void TonesCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Проверяем какая страница сейчас открыта
            if (MainFrame.Content is CompleteProductsPage productsPage && TonesCmb.SelectedItem != null)
            {
                productsPage.SelectedTone = TonesCmb.SelectedItem.ToString();
                productsPage.RefreshData();
            }
            else if (MainFrame.Content is TilesPage tilesPage && TonesCmb.SelectedItem != null)
            {
                tilesPage.SelectedTone = TonesCmb.SelectedItem.ToString();
                tilesPage.RefreshData();
            }
        }

        private void StoneTypesCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainFrame.Content is CompleteProductsPage productsPage && StoneTypesCmb.SelectedItem != null)
            {
                productsPage.SelectedStoneType = StoneTypesCmb.SelectedItem.ToString();
                productsPage.RefreshData();
            }
            else if (MainFrame.Content is TilesPage tilesPage && StoneTypesCmb.SelectedItem != null)
            {
                tilesPage.SelectedStoneType = StoneTypesCmb.SelectedItem.ToString();
                tilesPage.RefreshData();
            }
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Получаем текущую страницу во фрейме
            var productsPage = MainFrame.Content as CompleteProductsPage;

            if (productsPage != null)
            {
                // Передаем текст поиска на страницу CompleteProductsPage
                productsPage.UpdateSearch(SearchTb.Text);
            }
            else if (MainFrame.Content is TilesPage tilesPage)
            {
                // Передаем текст поиска на страницу с плитками TilesPage
                tilesPage.UpdateSearch(SearchTb.Text);
            }

        }

        private void PersonalCabinetMI_Click(object sender, RoutedEventArgs e)
        {
            ProfileWindow profileWindow = new ProfileWindow();
            profileWindow.Show();
            this.Close();
        }
    }
}
