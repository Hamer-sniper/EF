using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace EF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WorkWithData workWithData = new WorkWithData();

        public MainWindow()
        {
            InitializeComponent();

            SqlConnectionStringTextBox.Text = workWithData.sqlConnection.ConnectionString;
            AccessConnectionStringTextBox.Text = workWithData.oleDbConnection.ConnectionString;

            SqlConnectionStateTextBox.Text = workWithData.sqlConnection.State.ToString();
            AccessConnectionStateTextBox.Text = workWithData.oleDbConnection.State.ToString();

            workWithData.sqlConnection.StateChange += new StateChangeEventHandler(OnStateChange);
            workWithData.oleDbConnection.StateChange += new StateChangeEventHandler(OnStateChange);
        }

        #region Методы
        /// <summary>
        /// Событие смены статуса подключения
        /// </summary>
        /// <param name="sender">Отправитель</param>
        /// <param name="args">Аргументы события</param>
        public void OnStateChange(object sender, StateChangeEventArgs args)
        {
            if (sender.ToString() == "Microsoft.Data.SqlClient.SqlConnection")
            {
                SqlConnectionStateTextBox.Text = args.CurrentState.ToString();
                if (args.CurrentState == ConnectionState.Closed)
                    SqlInfoShowButton.Content = "Открыть соединение с SQL";
                else
                    SqlInfoShowButton.Content = "Закрыть соединение с SQL";
            }

            if (sender.ToString() == "System.Data.OleDb.OleDbConnection")
            {
                AccessConnectionStateTextBox.Text = args.CurrentState.ToString();
                if (args.CurrentState == ConnectionState.Closed)
                    AccessInfoShowButton.Content = "Открыть соединение с Access";
                else
                    AccessInfoShowButton.Content = "Закрыть соединение с Access";
            }
        }
        #endregion

        private void SqlInfoShowButton_Click(object sender, RoutedEventArgs e)
        {
            workWithData.ConnectionStateChanger("sql");
        }

        private void AccessInfoShowButton_Click(object sender, RoutedEventArgs e)
        {
            workWithData.ConnectionStateChanger("ole");
        }

        private void SqlTableShowButton_Click(object sender, RoutedEventArgs e)
        {
            new SQL_Window().Show();
        }

        private void AccessTableShowButton_Click(object sender, RoutedEventArgs e)
        {
            new Access_Window().Show();
        }
    }
}
