using _17v;
using System;
using System.Collections.Generic;
using System.Text;

namespace _17v
{
    public class InMemoryRepository
    {
        /// <summary>
        /// Репозиторий для работы с данными в памяти.
        /// </summary>
        private List<Workshop> _workshops;
        private List<Baker> _bakers;
        private List<Bakery> _bakeries;

        /// <summary>
        /// Конструктор заполняет списки тестовыми данными.
        /// </summary>
        public InMemoryRepository()
        {
            _workshops = new List<Workshop>
            {
                new Workshop { Id = 1, Name = "Кондитерский", Head = "Петрова А.А." },
                new Workshop { Id = 2, Name = "Хлебный", Head = "Иванов И.И." },
                new Workshop { Id = 3, Name = "Пекарня №1", Head = "Сидоров С.С." }
            };

            _bakers = new List<Baker>
            {
                new Baker { Id = 1, FullName = "Петрова А.А.", Experience = 5, Shift = "Утренняя" },
                new Baker { Id = 2, FullName = "Иванов И.И.", Experience = 2, Shift = "Ночная" },
                new Baker { Id = 3, FullName = "Смирнов В.В.", Experience = 8, Shift = "Дневная" },
                new Baker { Id = 4, FullName = "Кузнецова Е.Е.", Experience = 4, Shift = "Утренняя" },
                new Baker { Id = 5, FullName = "Попов Д.Д.", Experience = 1, Shift = "Вечерняя" }
            };

            _bakeries = new List<Bakery>
            {
                new Bakery { Id = 1, Name = "Круассан", WorkshopId = 1, BakerId = 1, Price = 80, Weight = 100 },
                new Bakery { Id = 2, Name = "Батон", WorkshopId = 2, BakerId = 2, Price = 40, Weight = 400 },
                new Bakery { Id = 3, Name = "Пирожное", WorkshopId = 1, BakerId = 3, Price = 120, Weight = 150 },
                new Bakery { Id = 4, Name = "Хлеб Бородинский", WorkshopId = 2, BakerId = 4, Price = 60, Weight = 350 },
                new Bakery { Id = 5, Name = "Кекс", WorkshopId = 3, BakerId = 5, Price = 90, Weight = 200 }
            };
        }

        /// <summary>
        /// Возвращает список всех цехов.
        /// </summary>
        public List<Workshop> GetWorkshops() { return _workshops; }

        /// <summary>
        /// Возвращает список всех пекарей.
        /// </summary>
        public List<Baker> GetBakers() { return _bakers; }

        /// <summary>
        /// Возвращает список всей выпечки.
        /// </summary>
        public List<Bakery> GetBakery() { return _bakeries; }
    }
}
