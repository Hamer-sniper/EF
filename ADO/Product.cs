using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF
{
    class Product
    {
        /// <summary>
        /// Электронная почта
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Код товара
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// Наименование товара
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email">Электронная почта</param>
        /// <param name="productCode">Код товара</param>
        /// <param name="productName">Наименование товара</param>
        public Product(string email, string productCode, string productName)
        {
            Email = email;
            ProductCode = productCode;
            ProductName = productName;
        }

        public Product() { }
    }
}
