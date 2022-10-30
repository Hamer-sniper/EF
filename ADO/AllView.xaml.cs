using EF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using Microsoft.Data.SqlClient;
using System.Data.OleDb;

namespace EF
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class AllView : Window
    {
        WorkWithData workWithData = new WorkWithData();

        public AllView()
        {
            InitializeComponent();

            DataTable dt = new DataTable();

            Microsoft.Data.SqlClient.SqlDataAdapter da = new Microsoft.Data.SqlClient.SqlDataAdapter();

            Microsoft.Data.SqlClient.SqlConnection con = workWithData.sqlConnection;

            var sql = @"SELECT * FROM SQL_Table";

            da.SelectCommand = new Microsoft.Data.SqlClient.SqlCommand(sql, con);
            da.Fill(dt);

            gridAllView.DataContext = dt.DefaultView;
        }

        public AllView(DataRowView row)
        {
            InitializeComponent();

            DataTable dt = new DataTable();

            OleDbDataAdapter da = new OleDbDataAdapter();

            OleDbConnection oleDbCon = workWithData.oleDbConnection;

            var email = row.Row[5].ToString();

            var ole = @"SELECT * FROM Access_Table WHERE Email = @Email";

            da.SelectCommand = new OleDbCommand(ole, oleDbCon);

            da.SelectCommand.Parameters.Add("@Email", email);

            da.Fill(dt);

            gridAllView.DataContext = dt.DefaultView;
        }
    }
}
