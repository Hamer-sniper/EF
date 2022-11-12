using System.Data.OleDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
using System.Data.Entity;

namespace EF
{
    /// <summary>
    /// Логика взаимодействия для SQL_Window.xaml
    /// </summary>
    public partial class Products_Window : Window
    {
        ProductContext dbProduct;

        public Products_Window()
        {
            InitializeComponent();
            Preparing();
        }

        public Products_Window(string email)
        {
            InitializeComponent();

            dbProduct = new ProductContext();

            dbProduct.Products.Where(p => p.Email == email).Load();

            gridView.ItemsSource = dbProduct.Products.Local.ToBindingList();
        }

        /// <summary>
        /// Новый код (для Entity Framework)
        /// </summary>
        private void Preparing()
        {
            dbProduct = new ProductContext();

            if (dbProduct.Database.Exists() == false)
            {
                //dbProduct.Database.ExecuteSqlCommand("TRUNCATE TABLE [Products]");

                Product product1 = new Product("abstractEmail1@mail.ru", "34", "Iphone телефон");
                Product product2 = new Product("abstractEmail1@mail.ru", "32", "Samsung телевизор");
                Product product3 = new Product("abstractEmail1@mail.ru", "23", "Вентилятор");
                Product product4 = new Product("abstractEmail2@mail.ru", "34", "Iphone телефон");

                var storageProducts = new List<Product>() { product1, product2, product3, product4 }; ;

                foreach (var itemProduct in storageProducts)
                    dbProduct.Products.Add(itemProduct);

                dbProduct.SaveChanges();
            }

            dbProduct.Products.Load();
            gridView.ItemsSource = dbProduct.Products.Local.ToBindingList();
        }

        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemDeleteClick(object sender, RoutedEventArgs e)
        {
            var product = (Product)gridView.SelectedItem;
            dbProduct.Products.Remove(product);
        }

        /// <summary>
        /// Добавление записи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemAddClick(object sender, RoutedEventArgs e)
        {
            var product = (Product)gridView.SelectedItem;

            AddProductWindow add;

            add = product == null ? new AddProductWindow(dbProduct) : new AddProductWindow(dbProduct, product.Email);
             

            add.ShowDialog();

            if (add.DialogResult.Value)
            {
                dbProduct.SaveChanges();
            }
        }

        /// <summary>
        /// Редактирование записи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AccessGVCurrentCellChanged(object sender, EventArgs e)
        {
            dbProduct.SaveChanges();
        }
    }
}
