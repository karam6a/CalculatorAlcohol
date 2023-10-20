using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CalculatorAlcohol
{
    class Calculating
    {
        private static Dictionary<string, int> capacityDictionary = new Dictionary<string, int>
        {
            { "Shot glass", 50 },
            { "Glass", 250 },
            { "Goblet", 300 },
            { "Mug", 400},
            { "Beer mug", 500 },
            { "Wine glass", 200 },
            { "Pitcher", 2000 },
            { "Bottle", 1000 }
        };

        public string ContainerType { get; set; } //Тип посуды
        public int ContainerCapacity { get; set; } //Вместимость посуды
        public int NumberOfContainers { get; set; } //Количество посуды
        public int AlcoholProcent { get; set; } //Процент алкоголя

        //ВАЖНО! В конструктор давать информацию в последовательности: Тип, Вместимость, Количество, Процент

        //Конструктор, если известны все переменные
        public Calculating(string containerType, int containerCapacity, int numberOfcontainers, int alcoholProcent) {
            ContainerType = containerType;
            ContainerCapacity = containerCapacity;
            NumberOfContainers = numberOfcontainers;
            AlcoholProcent = alcoholProcent;
        }

        //Конструктор, если не известен обьем, но известен тип посуды
        public Calculating(string containerType, int numberOfcontainers, int alcoholProcent)
        {
            ContainerType = containerType;
            ContainerCapacity = capacityDictionary[containerType];
            NumberOfContainers = numberOfcontainers;
            AlcoholProcent = alcoholProcent;
        }

        //Конструктор, если известен обьем, но не известен тип посуды
        public Calculating(int containerCapacity, int numberOfcontainers, int alcoholProcent)
        {
            var matchingKey = capacityDictionary.FirstOrDefault(x => x.Value == containerCapacity).Key;
            if (matchingKey != null)
            {
                //значение matchigKey = containerCapacity
                ContainerType = matchingKey;
            }
            else
            {
                // Значение не найдено, код пробует найти близжайший вариант посуды с данной емкостью
                ContainerType = findContainerType(containerCapacity) + " (auto selected by the program)";
            }
            ContainerCapacity = containerCapacity;
            NumberOfContainers = numberOfcontainers;
            AlcoholProcent = alcoholProcent;
        }


        //Метод ищет близжайшее значение обьема из словаря и возвращает тип посуды
        private static string findContainerType(int givenCapacityValue)
        {
            string closestKey = null;
            int closestDifference = int.MaxValue;

            foreach (var kvp in capacityDictionary)
            {
                int difference = Math.Abs(kvp.Value - givenCapacityValue);

                if (difference < closestDifference)
                {
                    closestKey = kvp.Key;
                    closestDifference = difference;
                }
            }

            if (closestKey != null)
            {
                // closestKey теперь содержит ключ, соответствующий ближайшему значению к targetValue
                // closestDifference содержит разницу между ближайшим значением и givenCapacityValue

                return closestKey;
            }
            else
            {
                // Словарь пуст ПОЛНАЯ ПИЗДА
                return "ERROR 69";
            }
        }

        //Метод возвращает строку с количеством выпитого спирта
        public static string calculateAlcohol(string containerType, int containerCapacity, int numberOfContainers, int alcoholProcent)
        {
            return "Container type: " + containerType + "\nCapacity: " + containerCapacity + "\nQuantity: " + numberOfContainers + "\nAlcohol procent: " + alcoholProcent;
        }    
    }
}

