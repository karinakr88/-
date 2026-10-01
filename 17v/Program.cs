namespace _17v
{
    internal class Program
    {
            static void Main(string[] args)
            {
                try
                {
                    Console.WriteLine("Выберите источник данных:");
                    Console.WriteLine("1 - InMemoryRepository");
                    Console.WriteLine("2 - CsvRepository (папка data)");
                    Console.Write("Ваш выбор: ");

                    string choice = Console.ReadLine();

                    List<Workshop> workshops = null;
                    List<Baker> bakers = null;
                    List<Bakery> bakeries = null;

                    switch (choice)
                    {
                        case "1":
                            InMemoryRepository memoryRepo = new InMemoryRepository();
                            workshops = memoryRepo.GetWorkshops();
                            bakers = memoryRepo.GetBakers();
                            bakeries = memoryRepo.GetBakery();
                            break;
                        case "2":
                            // Путь к папке data 
                            CsvRepository csvRepo = new CsvRepository("data");
                            workshops = csvRepo.GetWorkshops();
                            bakers = csvRepo.GetBakers();
                            bakeries = csvRepo.GetBakery();
                            break;
                        default:
                            Console.WriteLine("Неверный выбор");
                            return; 
                    }

                    Console.WriteLine("\nРезультаты работы программы ");

                    // 1. Поиск пекаря изделия
                    string testItem = "Круассан";
                    Baker foundBaker = FindBaker(bakeries, bakers, testItem);
                    Console.WriteLine($"1. FindBaker(\"{testItem}\"): {(foundBaker != null ? foundBaker.GetInfo() : "Не найдено")}");

                    // 2. Поиск цеха изделия
                    Workshop foundWorkshop = FindWorkshop(bakeries, workshops, testItem);
                    Console.WriteLine($"2. FindWorkshop(item \"{testItem}\"): {(foundWorkshop != null ? foundWorkshop.GetInfo() : "Не найдено")}");

                    // 3. Общий вес изделий
                    int totalWeight = GetTotalWeight(bakeries);
                    Console.WriteLine($"3. GetTotalWeight: {totalWeight} г");

                // 4. Пекарь с максимальным весом
                string bakerResult = GetBakerWithMaxWeightInfo(bakeries, bakers);
                Console.WriteLine($"4. GetBakerWithMaxWeight: {bakerResult}");

                // 5. Вывод всех изделий
                Console.WriteLine("5. PrintAllBakery:");
                    PrintAllBakery(bakeries, bakers, workshops);

                    // Дополнительный пример из задания
                    Baker notFound = FindBaker(bakeries, bakers, "Неизвестное изделие");
                    Console.WriteLine("\nНе найдено: FindBaker(\"Неизвестное изделие\")  " +
                        (notFound == null ? "null" : ""));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nОШИБКА " + ex.Message);
                    return;
                }
            }

            /// <summary>
            /// 1. Поиск пекаря изделия. Не найдено — null.
            /// </summary>
            public static Baker FindBaker(List<Bakery> bakeries, List<Baker> bakers, string itemName)
            {
                if (bakeries == null || bakers == null) return null;

                foreach (var bakery in bakeries)
                {
                    if (bakery == null) continue;
                    if (bakery.Name == itemName)
                    {
                        foreach (var baker in bakers)
                        {
                            if (baker == null) continue;
                            if (baker.Id == bakery.BakerId)
                                return baker;
                        }
                    }
                }
                return null;
            }

            /// <summary>
            /// 2. Поиск цеха изделия. Не найдено — null.
            /// </summary>
            public static Workshop FindWorkshop(List<Bakery> bakeries, List<Workshop> workshops, string itemName)
            {
                if (bakeries == null || workshops == null) return null;

                foreach (var bakery in bakeries)
                {
                    if (bakery == null) continue;
                    if (bakery.Name == itemName)
                    {
                        foreach (var workshop in workshops)
                        {
                            if (workshop == null) continue;
                            if (workshop.Id == bakery.WorkshopId)
                                return workshop;
                        }
                    }
                }
                return null;
            }

            /// <summary>
            /// 3. Общий вес изделий. Пустой список — 0.
            /// </summary>
            public static int GetTotalWeight(List<Bakery> bakeries)
            {
            
                if (bakeries == null) return 0;

                int total = 0;
                foreach (var bakery in bakeries)
                {
                    if (bakery == null) continue;
                    total += bakery.Weight;
                }
                return total;
            }

        /// <summary>
        /// 4. Пекарь с максимальным весом. При равенстве — первый. Нет изделий — null.
        /// </summary>
        /// /// <summary>
        /// 4. Пекарь с максимальным весом. Возвращает готовую строку для вывода.
        /// При равенстве — первый. Нет изделий — "null".
        /// </summary>
        public static string GetBakerWithMaxWeightInfo(List<Bakery> bakeries, List<Baker> bakers)
        {
            if (bakeries == null || bakers == null || bakeries.Count == 0) return "null";

            int maxWeight = -1;
            int bestBakerId = -1;

            foreach (var bakery in bakeries)
            {
                if (bakery == null) continue;
                if (bakery.Weight > maxWeight)
                {
                    maxWeight = bakery.Weight;
                    bestBakerId = bakery.BakerId;
                }
            }

            foreach (var baker in bakers)
            {
                if (baker == null) continue;
                if (baker.Id == bestBakerId)
                    return $"{baker.FullName} ({maxWeight} г)";
            }

            return "null";
        }

        /// <summary>
        /// 5. Вывод всех изделий. <GetInfo()> — пекарь <FullName>, цех "<Name>". Не найдено — "—".
        /// </summary>
        public static void PrintAllBakery(List<Bakery> bakeries, List<Baker> bakers, List<Workshop> workshops)
            {
                if (bakeries == null || bakers == null || workshops == null)
                {
                    Console.WriteLine("Ошибка: один из списков пуст (null).");
                    return;
                }

                foreach (var bakery in bakeries)
                {
                    if (bakery == null) continue;

                    string bakerName = "—";
                    string workshopName = "—";

                    foreach (var baker in bakers)
                    {
                        if (baker == null) continue;
                        if (baker.Id == bakery.BakerId)
                        {
                            bakerName = baker.FullName;
                            break;
                        }
                    }

                    foreach (var workshop in workshops)
                    {
                        if (workshop == null) continue;
                        if (workshop.Id == bakery.WorkshopId)
                        {
                            workshopName = workshop.Name;
                            break;
                        }
                    }

                    Console.WriteLine($"\"{bakery.GetInfo()}\" — пекарь {bakerName}, цех \"{workshopName}\"");
                }
            }
        }
    }