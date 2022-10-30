using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF
{
    class Client
    {
        /// <summary>
        /// Id
        /// </summary>
        public string ClientId { get; set; }

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
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="clientId">Id</param>
        /// <param name="surname">Фамилия</param>
        /// <param name="name">Имя</param>
        /// <param name="lastName">Отчество</param>
        /// <param name="telephone">Телефон</param>
        /// <param name="email">Электронная почта</param>
        public Client(string clientId, string surname, string name, string lastName, string telephone, string email)
        {
            ClientId = clientId;
            Surname = surname;
            Name = name;
            LastName = lastName;
            Telephone = telephone;
            Email = email;
        }

        public Client() { }

        public override string ToString()
        {
            return $"{ClientId} {Surname} {Name} {LastName} {Telephone} {Email}";
        }
    }
}
