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
    /// Логика взаимодействия для AddItemsPage.xaml
    /// </summary>
    public partial class AddItemsPage : Page
    {
        public AddItemsPage()
        {
            InitializeComponent();

            StoneTypesCmb.ItemsSource = App.context.Stone_Types.ToList();
            StoneTypesCmb.DisplayMemberPath = "name";
            StoneTypesCmb.SelectedValuePath = "id";

            TonesCmb.ItemsSource = App.context.Tones.ToList();
            TonesCmb.DisplayMemberPath = "name";
            TonesCmb.SelectedValuePath = "id";


            TypeCmb.Items.Add("Готовый товар");
            TypeCmb.Items.Add("Плитка");
            TypeCmb.SelectedIndex = 0;

            StoneTypesCmb.Items.Add("Мрамор");
            StoneTypesCmb.Items.Add("Оникс");
            StoneTypesCmb.Items.Add("Гранит");
            StoneTypesCmb.Items.Add("Акриловый камень");
            StoneTypesCmb.Items.Add("Искусственный камень");
            StoneTypesCmb.Items.Add("Кварцит");

            TonesCmb.Items.Add("Светлый");
            TonesCmb.Items.Add("Темный");

            StoneTypesCmb.SelectedIndex = 0;
            TonesCmb.SelectedIndex = 0;
        }

        private void TypeCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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

            string type = TypeCmb.SelectedItem?.ToString();
            string stoneType = StoneTypesCmb.SelectedItem?.ToString();
            string tone = TonesCmb.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(type))
            {
                MessageBox.Show("Выберите тип товара!");
                return;
            }

            if (type == "Готовый товар")
            {
                var product = new Complete_Products
                {
                    Name = NameTb.Text,
                    price = price,
                    photo = PhotoTb.Text,
                    stone_type_id = (int)StoneTypesCmb.SelectedValue,
                    tone_id = (int)TonesCmb.SelectedValue
                };

                App.context.Complete_Products.Add(product);
            }
            else if (type == "Плитка")
            {
                var tile = new Tiles
                {
                    Name = NameTb.Text,
                    price = price,
                    photo = PhotoTb.Text,
                    stone_type_id = (int)StoneTypesCmb.SelectedValue,
                    tone_id = (int)TonesCmb.SelectedValue
                };

                App.context.Tiles.Add(tile);
            }

            NameTb.Clear();
            PriceTb.Clear();
            PhotoTb.Clear();
            TypeCmb.SelectedIndex = 0;
            StoneTypesCmb.SelectedIndex = 0;
            TonesCmb.SelectedIndex = 0;
        }
    }
}
    

