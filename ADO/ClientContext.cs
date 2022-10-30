using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF
{
    class SQL_TableContext : DbContext
    {
        public static string ConnectionString { get; set; }

        public void GetConnectionString()
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

            ConnectionString = sqlCon.ConnectionString;
        }

        public SQL_TableContext() : base(ConnectionString) { GetConnectionString(); }

        public DbSet<SQL_Table> SQL_Table { get; set; }
    }
}
