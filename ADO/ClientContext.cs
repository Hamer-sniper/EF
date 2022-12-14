using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace EF
{
    public class ClientContext : DbContext
    {
        public static string ConnectionString { get; set; }

        static ClientContext()
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

        public ClientContext() : base(ConnectionString) { }

        public DbSet<Client> Clients { get; set; }
    }
}
