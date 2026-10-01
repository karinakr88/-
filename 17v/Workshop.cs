using System;
using System.Collections.Generic;
using System.Text;

namespace _17v
{
    /// <summary>
    /// Представляет цех (Workshop).
    /// </summary>
    public class Workshop
    {
            private string _name;
            private string _head;

            /// <summary>
            /// Уникальный идентификатор цеха.
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// Название цеха. Не может быть пустым.
            /// </summary>
            public string Name
            {
                get { return _name; }
                set
                {
                    // Проверка корректности данных
                    if (string.IsNullOrWhiteSpace(value))
                        throw new ArgumentException("Название цеха не может быть пустым");
                    _name = value;
                }
            }

            /// <summary>
            /// Заведующий цехом. Не может быть пустым.
            /// </summary>
            public string Head
            {
                get { return _head; }
                set
                {
                    // НОВОЕ ТРЕБОВАНИЕ: Проверка корректности данных
                    if (string.IsNullOrWhiteSpace(value))
                        throw new ArgumentException("Имя заведующего не может быть пустым");
                    _head = value;
                }
            }

            /// <summary>
            /// Вычисляемое свойство: является ли цех кондитерским.
            /// </summary>
            public bool IsConfectionery
            {
                get { return Name == "Кондитерский"; }
            }

            /// <summary>
            /// Возвращает строковое представление цеха.
            /// </summary>
            public string GetInfo()
            {
                return $"{Name} (зав.: {Head})";
            }
        }
    }
