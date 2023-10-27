using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CalculatorAlcohol
{
    class Calculating
    {
        private static Dictionary<string, int> capacityDictionary = new Dictionary<string, int>
        {
            { "Shot glass", 50 },
            { "Wine glass", 200 },
            { "Glass", 250 },
            { "Goblet", 300 },
            { "Mug", 400},
            { "Beer mug", 500 },
            { "Bottle", 1000 },
            { "Pitcher", 2000 }
        };

        public static void fillComboBoxFromDictionary(ComboBox comboBox)
        {
            comboBox.Items.Clear();
            foreach (var kvp in capacityDictionary)
            {
                comboBox.Items.Add(kvp.Key);
            }
        }

        public static int findContainerTypeInDictinory(string givenContainerType)
        {
            foreach (var kvp in capacityDictionary)
            {
                if (kvp.Key == givenContainerType)
                {
                    return kvp.Value;
                }
            }
            return -1;
        }

        public string ContainerType { get; set; } //Тип посуды
        public int ContainerCapacity { get; set; } //Вместимость посуды
        public int NumberOfContainers { get; set; } //Количество посуды
        public float AlcoholProcent { get; set; } //Процент алкоголя

        //ВАЖНО! В конструктор давать информацию в последовательности: Тип, Вместимость, Количество, Процент

        //Конструктор, если известны все переменные
        public Calculating(string containerType, int containerCapacity, int numberOfcontainers, float alcoholProcent) {
            ContainerType = containerType;
            ContainerCapacity = containerCapacity;
            NumberOfContainers = numberOfcontainers;
            AlcoholProcent = alcoholProcent;
        }

        //Конструктор, если не известен обьем, но известен тип посуды
        public Calculating(string containerType, int numberOfcontainers, float alcoholProcent)
        {
            ContainerType = containerType;
            ContainerCapacity = capacityDictionary[containerType];
            NumberOfContainers = numberOfcontainers;
            AlcoholProcent = alcoholProcent;
        }

        //Конструктор, если известен обьем, но не известен тип посуды
        public Calculating(int containerCapacity, int numberOfcontainers, float alcoholProcent)
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
                ContainerType = findContainerType(containerCapacity) + " (auto sel.)";
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
                // Словарь пуст
                return "ERROR";
            }
        }

        //Метод возвращает строку с количеством выпитого спирта
        public static string calculateAlcohol(Calculating test)
        {
            return "Container type: " + test.ContainerType + "\nCapacity: " + test.ContainerCapacity + "\nQuantity: " + test.NumberOfContainers + "\nAlcohol procent: " + test.AlcoholProcent + "\nRESULT: " + test.ContainerCapacity * test.NumberOfContainers * (test.AlcoholProcent / 100) + "ml. of alcohol";
        }
        
        //public static bool 
    }
}

