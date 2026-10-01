using System;
using System.Collections.Generic;
using System.Text;

namespace _17v
{
    public class Baker
    {
        /// <summary>
        /// Представляет пекаря (Baker).
        /// </summary>
        private string _fullName;
        private int _experience;
        private string _shift;

        /// <summary>
        /// Уникальный идентификатор пекаря.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Полное имя пекаря. Не может быть пустым.
        /// </summary>
        public string FullName
        {
            get { return _fullName; }
            set
            {

                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Имя пекаря не может быть пустым");
                _fullName = value;
            }
        }

        /// <summary>
        /// Стаж работы. Не может быть отрицательным.
        /// </summary>
        public int Experience
        {
            get { return _experience; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Стаж не может быть отрицательным");
                _experience = value;
            }
        }

        /// <summary>
        /// Смена пекаря.
        /// </summary>
        public string Shift
        {
            get { return _shift; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Смена не может быть пустой");
                _shift = value;
            }
        }

        /// <summary>
        ///  является ли пекарь опытным (больше 3 лет).
        /// </summary>
        public bool IsExperienced
        {
            get { return Experience > 3; }
        }

        /// <summary>
        /// Возвращает строковое представление пекаря.
        /// </summary>
        public string GetInfo()
        {
            return $"{FullName} ({Experience} лет опыта)";
        }
    }
}
