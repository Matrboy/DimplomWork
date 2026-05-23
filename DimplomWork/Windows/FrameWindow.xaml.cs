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

            // Проверяем роль пользователя
            if (App.currentUser != null && App.currentUser.role_id == 2)  // Если администратор (роль 2)
            {
                // Показываем все 4 кнопки для администратора
                DeleteProductTileBtn.Visibility = Visibility.Visible;
                EditProductTileBtn.Visibility = Visibility.Visible;
                AddProductTileBtn.Visibility = Visibility.Visible;
                SupliersInfBtn.Visibility = Visibility.Visible;
            }
            else
            {
                // Скрываем кнопки для обычных пользователей
                DeleteProductTileBtn.Visibility = Visibility.Collapsed;
                EditProductTileBtn.Visibility = Visibility.Collapsed;
                AddProductTileBtn.Visibility = Visibility.Collapsed;
                SupliersInfBtn.Visibility = Visibility.Collapsed;
            }

            // Заполняем комбобокс тонов
            TonesCmb.Items.Add("Все Тона");
            TonesCmb.Items.Add("Светлый");
            TonesCmb.Items.Add("Темный");
            TonesCmb.SelectedIndex = 0;

            // Заполняем комбобокс типов камня
            StoneTypesCmb.Items.Add("Все Камни");
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

        private void DeleteProductTileBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.Content is CompleteProductsPage productsPage)
            {
                // Прямой доступ к SelectedItem ListBox
                var selectedProduct = productsPage.Complete_ProductsListBox.SelectedItem as Complete_Products;

                if (selectedProduct != null)
                {
                    if (MessageBox.Show($"Удалить '{selectedProduct.Name}'?", "Подтверждение",
                        MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        var product = App.context.Complete_Products.Find(selectedProduct.id);
                        App.context.Complete_Products.Remove(product);
                        App.context.SaveChanges();
                        productsPage.RefreshData();
                        MessageBox.Show("Удалено!");
                    }
                }
                else
                {
                    MessageBox.Show("Выберите товар!");
                }
            }
            else if (MainFrame.Content is TilesPage tilesPage)
            {
                var selectedTile = tilesPage.TilesListBox.SelectedItem as Tiles;

                if (selectedTile != null)
                {
                    if (MessageBox.Show($"Удалить '{selectedTile.Name}'?", "Подтверждение",
                        MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        var tile = App.context.Tiles.Find(selectedTile.id);
                        App.context.Tiles.Remove(tile);
                        App.context.SaveChanges();
                        tilesPage.RefreshData();
                        MessageBox.Show("Удалено!");
                    }
                }
                else
                {
                    MessageBox.Show("Выберите плитку!");
                }
            }
        }

        private void EditProductTileBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.Content is CompleteProductsPage productsPage)
            {
                var selectedProduct = productsPage.Complete_ProductsListBox.SelectedItem as Complete_Products;

                if (selectedProduct != null)
                {
                    MainFrame.Navigate(new EditItemPage(selectedProduct));
                }
                else
                {
                    MessageBox.Show("Выберите товар!");
                }
            }
            else if (MainFrame.Content is TilesPage tilesPage)
            {
                var selectedTile = tilesPage.TilesListBox.SelectedItem as Tiles;

                if (selectedTile != null)
                {
                    MainFrame.Navigate(new EditItemPage(selectedTile));
                }
                else
                {
                    MessageBox.Show("Выберите плитку!");
                }
            }
            else
            {
                MessageBox.Show("Откройте список товаров или плитки!");
            }
        }

        private void AddProductTileBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AddItemsPage());
        }

        private void SupliersInfBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SupliersPage());
        }

        private void AboutBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AboutPage());
        }
    }
}
