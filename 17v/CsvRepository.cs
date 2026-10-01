using _17v;
using System;
using System.Collections.Generic;
using System.Text;

namespace _17v
{
    /// <summary>
    /// Репозиторий для загрузки данных из CSV-файлов.
    /// </summary>
    public class CsvRepository
    {
        private string _basePath;

        /// <summary>
        /// Конструктор принимает базовый путь к папке с CSV-файлами.
        /// </summary>
        public CsvRepository(string basePath)
        {
            _basePath = basePath;
        }

        /// <summary>
        /// Загружает список цехов из файла workshops.csv.
        /// </summary>
        public List<Workshop> GetWorkshops()
        {
            List<Workshop> result = new List<Workshop>();
            string path = Path.Combine(_basePath, "workshops.csv");
            if (!File.Exists(path)) return result;

            string[] lines = File.ReadAllLines(path);
            if (lines.Length < 2) return result;

            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                if (parts.Length < 3) continue;

                Workshop w = new Workshop();
                w.Id = int.Parse(parts[0]);
                w.Name = parts[1];
                w.Head = parts[2];
                result.Add(w);
            }
            return result;
        }

        /// <summary>
        /// Загружает список пекарей из файла bakers.csv.
        /// </summary>
        public List<Baker> GetBakers()
        {
            List<Baker> result = new List<Baker>();
            string path = Path.Combine(_basePath, "bakers.csv");
            if (!File.Exists(path)) return result;

            string[] lines = File.ReadAllLines(path);
            if (lines.Length < 2) return result;

            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                if (parts.Length < 4) continue;

                Baker b = new Baker();
                b.Id = int.Parse(parts[0]);
                b.FullName = parts[1];
                b.Experience = int.Parse(parts[2]);
                b.Shift = parts[3];
                result.Add(b);
            }
            return result;
        }

        /// <summary>
        /// Загружает список выпечки из файла bakeries.csv.
        /// </summary>
        public List<Bakery> GetBakery()
        {
            List<Bakery> result = new List<Bakery>();
            string path = Path.Combine(_basePath, "bakeries.csv");
            if (!File.Exists(path)) return result;

            string[] lines = File.ReadAllLines(path);
            if (lines.Length < 2) return result;

            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                if (parts.Length < 6) continue;

                Bakery b = new Bakery();
                b.Id = int.Parse(parts[0]);
                b.Name = parts[1];
                b.WorkshopId = int.Parse(parts[2]);
                b.BakerId = int.Parse(parts[3]);
                b.Price = decimal.Parse(parts[4]);
                b.Weight = int.Parse(parts[5]);
                result.Add(b);
            }
            return result;
        }
    }
}