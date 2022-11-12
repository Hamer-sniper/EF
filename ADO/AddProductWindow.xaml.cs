using System;
using System.Collections.Generic;
using System.Data;
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
using System.Xml.Linq;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace EF
{
    /// <summary>
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        private AddProductWindow()
        {
            InitializeComponent();
        }

        public AddProductWindow(ProductContext dbProduct, string email) : this()
        {
            txtEmail.Text = email;

            cancelBtn.Click += delegate { this.DialogResult = false; };
            okBtn.Click += delegate
            {
                Product product = new Product(email, txtCode.Text, txtProductName.Text);
                dbProduct.Products.Add(product);
                this.DialogResult = !false;
            };
        }

        public AddProductWindow(ProductContext dbProduct) : this()
        {
            cancelBtn.Click += delegate { this.DialogResult = false; };
            okBtn.Click += delegate
            {
                Product product = new Product(txtEmail.Text, txtCode.Text, txtProductName.Text);
                
                dbProduct.Products.Add(product);
                this.DialogResult = !false;
            };
        }

        public AddProductWindow(DataRow row) : this()
        {
            cancelBtn.Click += delegate { this.DialogResult = false; };
            okBtn.Click += delegate
            {
                row["Email"] = txtEmail.Text;
                row["Code"] = txtCode.Text;
                row["ProductName"] = txtProductName.Text;
                this.DialogResult = !false;
            };
        }

    }
}
