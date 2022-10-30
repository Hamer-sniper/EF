using Microsoft.Data.SqlClient;
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

namespace EF
{
    /// <summary>
    /// Логика взаимодействия для SQL_Window.xaml
    /// </summary>
    public partial class SQL_Window : Window
    {
        WorkWithData workWithData = new WorkWithData();

        SqlConnection con;
        SqlDataAdapter da;
        DataTable dt;
        DataRowView row;

        public SQL_Window()
        {
            InitializeComponent();
            Preparing();
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
        /// Начало редактирования 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GVCellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

            row = (DataRowView)gridView.SelectedItem;
            row.BeginEdit();
            //da.Update(dt);
        }

        /// <summary>
        /// Редактирование записи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GVCurrentCellChanged(object sender, EventArgs e)
        {
            if (row == null) return;
            row.EndEdit();
            da.Update(dt);
        }

        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemDeleteClick(object sender, RoutedEventArgs e)
        {
            row = (DataRowView)gridView.SelectedItem;
            row.Row.Delete();
            da.Update(dt);
        }

        /// <summary>
        /// Добавление записи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemAddClick(object sender, RoutedEventArgs e)
        {
            DataRow r = dt.NewRow();
            AddWindow add = new AddWindow(r);
            add.ShowDialog();


            if (add.DialogResult.Value)
            {
                dt.Rows.Add(r);
                da.Update(dt);
            }
        }

        /// <summary>
        /// Посмотреть продукты по клиенту
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemProductClick(object sender, RoutedEventArgs e)
        {
            row = (DataRowView)gridView.SelectedItem;
            new AllView(row).Show();
        }

        /*private void AllViewShow(object sender, RoutedEventArgs e)
        {
            new AllView().ShowDialog();
        }*/
    }
}
