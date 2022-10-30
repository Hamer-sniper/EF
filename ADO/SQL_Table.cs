using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF
{
    class SQL_Table
    {
        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Отчество
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// Электронная почта
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="surname">Фамилия</param>
        /// <param name="name">Имя</param>
        /// <param name="lastName">Отчество</param>
        /// <param name="telephone">Телефон</param>
        /// <param name="email">Электронная почта</param>
        public SQL_Table(string surname, string name, string lastName, string telephone, string email)
        {
            Surname = surname;
            Name = name;
            LastName = lastName;
            Telephone = telephone;
            Email = email;
        }

        public SQL_Table() { }
    }
}
