using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Markup;
using System.Windows;

namespace EF
{
    /// <summary>
    /// Работа с базами данных
    /// </summary>
    public class WorkWithData
    {
        #region Поля
        /// <summary>
        /// Путь до базы Access
        /// </summary>
        private string oleDbDataSourcePath = Environment.CurrentDirectory + @"\Data\MSAccessLocalDB.accdb";
        #endregion

        #region Свойства
        /// <summary>
        /// Подключение к SQL
        /// </summary>
        public Microsoft.Data.SqlClient.SqlConnection sqlConnection { get; set; }
        /// <summary>
        /// Подключение к Acess
        /// </summary>
        public OleDbConnection oleDbConnection { get; set; }
        #endregion

        public WorkWithData()
        {
            Microsoft.Data.SqlClient.SqlConnectionStringBuilder sqlCon = new Microsoft.Data.SqlClient.SqlConnectionStringBuilder()
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = @"MSSQLLocalDemo",
                IntegratedSecurity = true,
                UserID = "sa",
                Password = "123",
                Pooling = false
            };
            sqlConnection = new Microsoft.Data.SqlClient.SqlConnection(sqlCon.ConnectionString);

            OleDbConnectionStringBuilder oleDbCon = new OleDbConnectionStringBuilder()
            {
                DataSource = oleDbDataSourcePath,
                Provider = @"Microsoft.ACE.OLEDB.12.0",
                PersistSecurityInfo = true,
                ["User ID"] = "Admin",
                ["Jet OLEDB:Database Password"] = "123"
            };
            oleDbConnection = new OleDbConnection(oleDbCon.ConnectionString);
        }
        /// <summary>
        /// Подключить/отключить источник данных
        /// </summary>
        /// <param name="select">Источник</param>
        public void ConnectionStateChanger(string select)
        {
            switch (select)
            {
                case "sql":
                    if (sqlConnection.State == ConnectionState.Closed)
                        sqlConnection.Open();
                    else
                        sqlConnection.Close();
                    break;
                case "ole":
                    if (oleDbConnection.State == ConnectionState.Closed)
                        oleDbConnection.Open();
                    else
                        oleDbConnection.Close();
                    break;
                default:
                    break;
            }
        }
    }
}
