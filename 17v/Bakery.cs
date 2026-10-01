using System;
using System.Collections.Generic;
using System.Text;

namespace _17v
{
    public class Bakery
    {
        /// <summary>
        /// Представляет выпечку (Bakery).
        /// </summary>
        private string _name;
        private decimal _price;
        private int _weight;

        /// <summary>
        /// Уникальный идентификатор изделия.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название изделия. Не может быть пустым.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                //Проверка корректности данных
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Название изделия не может быть пустым");
                _name = value;
            }
        }

        /// <summary>
        /// Идентификатор цеха (внешний ключ).
        /// </summary>
        public int WorkshopId { get; set; }

        /// <summary>
        /// Идентификатор пекаря (внешний ключ).
        /// </summary>
        public int BakerId { get; set; }

        /// <summary>
        /// Цена изделия. Должна быть больше нуля.
        /// </summary>
        public decimal Price
        {
            get { return _price; }
            set
            {
                // НОВОЕ ТРЕБОВАНИЕ: Проверка корректности данных
                if (value <= 0)
                    throw new ArgumentException("Цена должна быть больше нуля");
                _price = value;
            }
        }

        /// <summary>
        /// Вес изделия. Должен быть больше нуля.
        /// </summary>
        public int Weight
        {
            get { return _weight; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Вес должен быть больше нуля");
                _weight = value;
            }
        }

        /// <summary>
        /// Вычисляемое свойство: цена за грамм.
        /// </summary>
        public decimal PricePerGram
        {
            get { return Price / Weight; }
        }

        /// <summary>
        /// Вычисляемое свойство: является ли изделие тяжелым (больше 300 г).
        /// </summary>
        public bool IsHeavy
        {
            get { return Weight > 300; }
        }

        /// <summary>
        /// Возвращает строковое представление выпечки.
        /// </summary>
        public string GetInfo()
        {
            return $"{Name} ({Price} руб., {Weight} г)";
        }
    }
}
