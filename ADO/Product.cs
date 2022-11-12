using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF
{
    public class Product
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
        /// Конструктор со всеми параметрами
        /// </summary>
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

        /// <summary>
        /// Конструктор с автоподстановкой GIUD
        /// </summary>
        /// <param name="email">Электронная почта</param>
        /// <param name="productCode">Код товара</param>
        /// <param name="productName">Наименование товара</param>
        public Product(string email, string productCode, string productName)
            : this (Guid.NewGuid().ToString(), email, productCode, productName)
        {
            Email = email;
            ProductCode = productCode;
            ProductName = productName;
        }

        /// <summary>
        /// Конструктор без параметров
        /// </summary>       
        public Product() { }

        public override string ToString()
        {
            return $"{ProductId} {Email} {ProductCode} {ProductName}";
        }
    }
}
