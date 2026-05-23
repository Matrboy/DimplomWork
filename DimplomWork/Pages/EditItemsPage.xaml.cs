using DimplomWork.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DimplomWork.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditItemsPage.xaml
    /// </summary>
    public partial class EditItemsPage : Page
    {
        private Complete_Products currentProduct;
        private Tiles currentTile;
        private bool isProduct;

        public EditItemsPage(Complete_Products product)
        {
            InitializeComponent();
            isProduct = true;
            currentProduct = product;
            LoadComboBoxes();
            FillData();
        }

        public EditItemsPage(Tiles tile)
        {
            InitializeComponent();
            isProduct = false;
            currentTile = tile;
            LoadComboBoxes();
            FillData();
        }

        private void LoadComboBoxes()
        {
            StoneTypeCmb.ItemsSource = App.context.Stone_Types.ToList();
            StoneTypeCmb.DisplayMemberPath = "name";
            StoneTypeCmb.SelectedValuePath = "id";

            ToneCmb.ItemsSource = App.context.Tones.ToList();
            ToneCmb.DisplayMemberPath = "name";
            ToneCmb.SelectedValuePath = "id";
        }

        private void FillData()
        {
            if (isProduct)
            {
                NameTb.Text = currentProduct.Name;
                PriceTb.Text = currentProduct.price.ToString();
                PhotoTb.Text = currentProduct.photo;

                StoneTypeCmb.SelectedValue = currentProduct.stone_type_id;
                ToneCmb.SelectedValue = currentProduct.tone_id;
            }
            else
            {
                NameTb.Text = currentTile.Name;
                PriceTb.Text = currentTile.price.ToString();
                PhotoTb.Text = currentTile.photo;

                StoneTypeCmb.SelectedValue = currentTile.stone_type_id;
                ToneCmb.SelectedValue = currentTile.tone_id;
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTb.Text))
            {
                MessageBox.Show("Введите название!");
                return;
            }

            if (!decimal.TryParse(PriceTb.Text, out decimal price))
            {
                MessageBox.Show("Введите корректную цену!");
                return;
            }

            if (StoneTypeCmb.SelectedValue == null)
            {
                MessageBox.Show("Выберите тип камня!");
                return;
            }

            if (ToneCmb.SelectedValue == null)
            {
                MessageBox.Show("Выберите тон!");
                return;
            }

            if (isProduct)
            {
                currentProduct.Name = NameTb.Text;
                currentProduct.price = price;
                currentProduct.photo = PhotoTb.Text;
                currentProduct.stone_type_id = (int)StoneTypeCmb.SelectedValue;
                currentProduct.tone_id = (int)ToneCmb.SelectedValue;

                App.context.SaveChanges();
                MessageBox.Show("Готовый товар изменён!");
            }
            else
            {
                currentTile.Name = NameTb.Text;
                currentTile.price = price;
                currentTile.photo = PhotoTb.Text;
                currentTile.stone_type_id = (int)StoneTypeCmb.SelectedValue;
                currentTile.tone_id = (int)ToneCmb.SelectedValue;

                App.context.SaveChanges();
                MessageBox.Show("Плитка изменена!");
            }

            NavigationService?.GoBack();
        }
    }
}
