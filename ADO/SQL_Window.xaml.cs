using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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

namespace EF
{
    /// <summary>
    /// Логика взаимодействия для SQL_Window.xaml
    /// </summary>
    public partial class SQL_Window : Window
    {
        ClientContext dbClient;

        WorkWithData workWithData = new WorkWithData();

        SqlConnection con;
        SqlDataAdapter da;
        DataTable dt;
        DataRowView row;

        public SQL_Window()
        {
            InitializeComponent();
            Preparing2();
        }

        /// <summary>
        /// Новый код (для Entity Framework)
        /// </summary>
        private void Preparing2()
        {
            dbClient = new ClientContext();

            if (dbClient.Database.Exists() == false)
            {
                //ExecuteSqlCommand("TRUNCATE TABLE [Clients]");

                Client сlient1 = new Client("Иванов", "Иван", "Иванович", "89280070701", "abstractEmail1@mail.ru");
                Client сlient2 = new Client("Петров", "Петр", "Петрович", "89280070702", "abstractEmail2@mail.ru");
                Client сlient3 = new Client("Сидоров", "Сидр", "Сидорович", "89280070703", "abstractEmail3@mail.ru");
                Client сlient4 = new Client("Маслов", "Масл", "Маслович", "89280070704", "abstractEmail4@mail.ru");

                var storageClients = new List<Client>() { сlient1, сlient2, сlient3, сlient4 }; ;

                foreach (var itemClient in storageClients)
                    dbClient.Clients.Add(itemClient);

                dbClient.SaveChanges();
            }

            dbClient.Clients.Load();
            gridView.ItemsSource = dbClient.Clients.Local.ToBindingList();
        }

        private void Preparing()
        {
            #region Init

            con = workWithData.sqlConnection;

            dt = new DataTable();
            da = new SqlDataAdapter();

            #endregion

            #region select
            var sql = @"SELECT * FROM SQL_Table";
            da.SelectCommand = new SqlCommand(sql, con);
            #endregion

            #region insert
            sql = @"INSERT INTO SQL_Table (Surname, Name,  MiddleName, Telephone, Email) 
                                 VALUES (@Surname, @Name, @MiddleName, @Telephone, @Email); 
                     SET @Id = @@IDENTITY;";

            da.InsertCommand = new SqlCommand(sql, con);

            da.InsertCommand.Parameters.Add("@Id", SqlDbType.Int, 10, "Id").Direction = ParameterDirection.Output;
            da.InsertCommand.Parameters.Add("@Surname", SqlDbType.NChar, 50, "Surname");
            da.InsertCommand.Parameters.Add("@Name", SqlDbType.NChar, 50, "Name");
            da.InsertCommand.Parameters.Add("@MiddleName", SqlDbType.NChar, 50, "MiddleName");
            da.InsertCommand.Parameters.Add("@Telephone", SqlDbType.NChar, 20, "Telephone");
            da.InsertCommand.Parameters.Add("@Email", SqlDbType.NChar, 50, "Email");

            #endregion

            #region update

            sql = @"UPDATE SQL_Table SET 
                           Surname = @Surname,
                           Name = @Name, 
                           MiddleName = @MiddleName, 
                           Telephone = @Telephone,
                           Email = @Email 
                    WHERE Id = @Id";

            da.UpdateCommand = new SqlCommand(sql, con);
            da.UpdateCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id").SourceVersion = DataRowVersion.Original;
            da.UpdateCommand.Parameters.Add("@Surname", SqlDbType.NChar, 50, "Surname");
            da.UpdateCommand.Parameters.Add("@Name", SqlDbType.NChar, 50, "Name");
            da.UpdateCommand.Parameters.Add("@MiddleName", SqlDbType.NChar, 50, "MiddleName");
            da.UpdateCommand.Parameters.Add("@Telephone", SqlDbType.NChar, 20, "Telephone");
            da.UpdateCommand.Parameters.Add("@Email", SqlDbType.NChar, 50, "Email");

            #endregion

            #region delete
            sql = "DELETE FROM SQL_Table WHERE Id = @Id";

            da.DeleteCommand = new SqlCommand(sql, con);
            da.DeleteCommand.Parameters.Add("@Id", SqlDbType.Int, 10, "Id");
            #endregion

            da.Fill(dt);

            gridView.DataContext = dt.DefaultView;
        }

        /// <summary>
        /// Редактирование записи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GVCurrentCellChanged(object sender, EventArgs e)
        {
            dbClient.SaveChanges();
        }

        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemDeleteClick(object sender, RoutedEventArgs e)
        {
            var client = (Client)gridView.SelectedItem;
            dbClient.Clients.Remove(client);
        }

        /// <summary>
        /// Добавление записи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemAddClick(object sender, RoutedEventArgs e)
        {            
            AddWindow add = new AddWindow(dbClient);

            add.ShowDialog();

            if (add.DialogResult.Value)
            {
                dbClient.SaveChanges();
            }
        }

        /// <summary>
        /// Посмотреть продукты по клиенту
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemProductClick(object sender, RoutedEventArgs e)
        {
            var client = (Client)gridView.SelectedItem;

            Products_Window pd = new Products_Window(client.Email);
            pd.Show();
        }
    }
}
