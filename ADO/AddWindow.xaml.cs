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

namespace EF
{
    /// <summary>
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public AddWindow() 
        {  
            InitializeComponent();
        }

        public AddWindow(ClientContext dbClient) : this()
        {
            cancelBtn.Click += delegate { this.DialogResult = false; };
            okBtn.Click += delegate
            {
                Client сlient = new Client(txtSurname.Text, txtName.Text, txtMiddleName.Text, txtTelephone.Text, txtEmail.Text);
                dbClient.Clients.Add(сlient);
                this.DialogResult = !false;
            };
        }

        public AddWindow(DataRow row):this()
        {
            cancelBtn.Click += delegate { this.DialogResult = false; };
            okBtn.Click += delegate
            {
                row["Surname"] = txtSurname.Text;
                row["Name"] = txtName.Text;
                row["MiddleName"] = txtMiddleName.Text;
                row["Telephone"] = txtTelephone.Text;
                row["Email"] = txtEmail.Text;
                this.DialogResult = !false;
            };
        }

    }
}
