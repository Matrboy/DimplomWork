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
    /// Логика взаимодействия для SupliersPage.xaml
    /// </summary>
    public partial class SupliersPage : Page
    {
        public SupliersPage()
        {
            InitializeComponent();
            //RefreshData();
        }

        //public void RefreshData(string searchText = null)
        //{
        //    //var allSuppliers = App.context.Supliers.ToList();

        //    if (!string.IsNullOrWhiteSpace(searchText))
        //    {
        //        allSuppliers = allSuppliers.Where(s =>
        //            s.Name.ToLower().Contains(searchText.ToLower()) ||
        //            (s.Phone != null && s.Phone.Contains(searchText)) ||
        //            (s.Email != null && s.Email.ToLower().Contains(searchText.ToLower()))
        //        ).ToList();
        //    }

        //    SuppliersLv.ItemsSource = allSuppliers;
        //}

        //private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    RefreshData(SearchTb.Text);
        //}
    }
}

