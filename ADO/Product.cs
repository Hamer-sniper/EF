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
        /// Id
        /// </summary>
        public string ProductId { get; set; }

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
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="productId">Id</param>
        /// <param name="email">Электронная почта</param>
        /// <param name="productCode">Код товара</param>
        /// <param name="productName">Наименование товара</param>
        public Product(string productId, string email, string productCode, string productName)
        {
            ProductId = productId;
            Email = email;
            ProductCode = productCode;
            ProductName = productName;
        }

        public Product() { }

        public override string ToString()
        {
            return $"{ProductId} {Email} {ProductCode} {ProductName}";
        }
    }
}
