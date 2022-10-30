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

namespace EF
{
    /// <summary>
    /// Логика взаимодействия для SQL_Window.xaml
    /// </summary>
    public partial class Access_Window : Window
    {
        WorkWithData workWithData = new WorkWithData();

        OleDbConnection con;
        OleDbDataAdapter da;
        DataTable dt;
        DataRowView row;

        public Access_Window()
        {
            InitializeComponent();
            Preparing();
        }

        private void Preparing()
        {
            #region Init

            con = workWithData.oleDbConnection;

            dt = new DataTable();
            da = new OleDbDataAdapter();
            #endregion

            #region select
            var sql = @"SELECT * FROM Access_Table";
            da.SelectCommand = new OleDbCommand(sql, con);
            #endregion

            #region insert
            sql = @"INSERT INTO Access_Table (Email, Code, ProductName) 
                                 VALUES (@Email, @Code, @ProductName); 
                     SET @ID = @@IDENTITY;";

            da.InsertCommand = new OleDbCommand(sql, con);

            da.InsertCommand.Parameters.Add("@ID", OleDbType.Integer, 10, "ID").Direction = ParameterDirection.Output;
            da.InsertCommand.Parameters.Add("@Email", OleDbType.WChar, 255, "Email");
            da.InsertCommand.Parameters.Add("@Code", OleDbType.WChar, 255, "Code");
            da.InsertCommand.Parameters.Add("@ProductName", OleDbType.WChar, 255, "ProductName");
            #endregion

            #region update
            sql = @"UPDATE Access_Table SET 
                           Email = @Email,
                           Code = @Code, 
                           ProductName = @ProductName 
                    WHERE ID = @ID";

            da.UpdateCommand = new OleDbCommand(sql, con);

            da.UpdateCommand.Parameters.Add("@ID", OleDbType.Integer, 0, "ID").SourceVersion = DataRowVersion.Original;
            da.UpdateCommand.Parameters.Add("@Email", OleDbType.WChar, 255, "Email");
            da.UpdateCommand.Parameters.Add("@Code", OleDbType.WChar, 255, "Code");
            da.UpdateCommand.Parameters.Add("@ProductName", OleDbType.WChar, 255, "ProductName");
            #endregion

            #region delete
            sql = "DELETE FROM Access_Table WHERE ID = @ID";

            da.DeleteCommand = new OleDbCommand(sql, con);
            da.DeleteCommand.Parameters.Add("@ID", OleDbType.Integer, 10, "ID");
            #endregion

            da.Fill(dt);

            gridView.DataContext = dt.DefaultView;
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
            AddProductWindow add = new AddProductWindow(r);
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

        /// <summary>
        /// Начало редактирования 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AccessGVCellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
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
        private void AccessGVCurrentCellChanged(object sender, EventArgs e)
        {
            if (row == null) return;
            row.EndEdit();
            da.Update(dt);
        }

        /*private void AllViewShow(object sender, RoutedEventArgs e)
        {
            new AllView().ShowDialog();
        }*/
    }
}
